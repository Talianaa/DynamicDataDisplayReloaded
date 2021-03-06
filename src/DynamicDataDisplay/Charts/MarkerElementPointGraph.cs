﻿using DynamicDataDisplay.DataSources;
using DynamicDataDisplay.PointMarkers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DynamicDataDisplay
{
	public class ElementMarkerPointsGraph : PointsGraphBase
	{
		/// <summary>List with created but unused markers</summary>
		private readonly List<UIElement> unused = new List<UIElement>();

		/// <summary>Initializes a new instance of the <see cref="MarkerPointsGraph"/> class.</summary>
		public ElementMarkerPointsGraph()
		{
			ManualTranslate = true; // We'll handle translation by ourselves
		}

		/// <summary>Initializes a new instance of the <see cref="MarkerPointsGraph"/> class.</summary>
		/// <param name="dataSource">The data source.</param>
		public ElementMarkerPointsGraph(IPointDataSource dataSource)
			: this()
		{
			DataSource = dataSource;
		}

		private Grid grid;
		private Canvas canvas;

		protected override void OnPlotterAttached(Plotter plotter)
		{
			base.OnPlotterAttached(plotter);

			grid = new Grid();
			canvas = new Canvas { ClipToBounds = true };
			grid.Children.Add(canvas);

			Plotter2D.CentralGrid.Children.Add(grid);
			Panel.SetZIndex(grid, base.ZIndex);
		}

		public override int ZIndex
		{
			get { return this.grid != null ? Panel.GetZIndex(this.grid) : base.ZIndex; }
			set
			{
				base.ZIndex = value;
				if (this.grid != null)
				{
					Panel.SetZIndex(this.grid, value);
				}
			}
		}

		protected override void OnPlotterDetaching(Plotter plotter)
		{
			Plotter2D.CentralGrid.Children.Remove(grid);
			grid = null;
			canvas = null;

			base.OnPlotterDetaching(plotter);
		}

		protected override void OnDataChanged()
		{
			//			if (canvas != null)
			//			{
			//                foreach(UIElement child in canvas.Children)
			//                    unused.Add(child);
			//				canvas.Children.Clear();
			//			}
			// todo почему так?
			base.OnDataChanged();
		}

		public ElementPointMarker Marker
		{
			get { return (ElementPointMarker)GetValue(MarkerProperty); }
			set { SetValue(MarkerProperty, value); }
		}

		public static readonly DependencyProperty MarkerProperty =
			DependencyProperty.Register(
				"Marker",
				typeof(ElementPointMarker),
				typeof(ElementMarkerPointsGraph),
				new FrameworkPropertyMetadata { DefaultValue = null, AffectsRender = true }
				);

		protected override void OnRenderCore(DrawingContext dc, RenderState state)
		{
			if (Marker == null)
				return;

			if (DataSource == null) // No data is specified
			{
				if (canvas != null)
				{
					foreach (UIElement child in canvas.Children)
						unused.Add(child);
					canvas.Children.Clear();
				}
			}
			else // There is some data
			{

				int index = 0;
				var transform = GetTransform();
				using (IPointEnumerator enumerator = DataSource.GetEnumerator(GetContext()))
				{
					Point point = new Point();

					DataRect bounds = DataRect.Empty;

					while (enumerator.MoveNext())
					{
						enumerator.GetCurrent(ref point);
						enumerator.ApplyMappings(Marker);

						if (index >= canvas.Children.Count)
						{
							UIElement newMarker;
							if (unused.Count > 0)
							{
								newMarker = unused[unused.Count - 1];
								unused.RemoveAt(unused.Count - 1);
							}
							else
								newMarker = Marker.CreateMarker();
							canvas.Children.Add(newMarker);
						}

						Marker.SetMarkerProperties(canvas.Children[index]);
						bounds.Union(point);
						Point screenPoint = point.DataToScreen(transform);
						Marker.SetPosition(canvas.Children[index], screenPoint);
						index++;
					}

					Viewport2D.SetContentBounds(this, bounds);

					while (index < canvas.Children.Count)
					{
						unused.Add(canvas.Children[index]);
						canvas.Children.RemoveAt(index);
					}
				}
			}
		}
	}
}