﻿using DynamicDataDisplay.Charts.Markers;
using DynamicDataDisplay.Charts.Shapes;
using System.Linq;
using System.Windows;

namespace DynamicDataDisplay.Charts.Selectors
{
	public class SinglePointSelector : PointSelector
	{
		#region Properties

		#region AllowedRect property

		public DataRect AllowedRegion
		{
			get { return (DataRect)GetValue(AllowedRegionProperty); }
			set { SetValue(AllowedRegionProperty, value); }
		}

		public static readonly DependencyProperty AllowedRegionProperty = DependencyProperty.Register(
		  "AllowedRegion",
		  typeof(DataRect),
		  typeof(SinglePointSelector),
		  new FrameworkPropertyMetadata(DataRect.Infinite, OnAllowedRegionChanged));

		private static void OnAllowedRegionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SinglePointSelector owner = (SinglePointSelector)d;
			owner.OnAllowedRegionChanged();
		}

		private void OnAllowedRegionChanged()
		{
			// removing points that are outside of allowed region
			var region = AllowedRegion;
			var pointsToRemove = Points.Where(p => !region.Contains(p)).ToArray();

			foreach (var point in pointsToRemove)
			{
				Points.Remove(point);
			}

			addPointCommand.RaiseCanExecuteChanged();
		}

		#endregion // end of AllowedRect property

		#endregion // end of Properties

		#region Commands

		protected override bool AddPointExecute(object parameter)
		{
			if (Points.Count == 1)
			{
				Points.RemoveAt(0);
			}
			return base.AddPointExecute(parameter);
		}

		protected override bool AddPointCanExecute(object parameter)
		{
			var result = base.AddPointCanExecute(parameter);

			if (parameter is Point)
			{
				Point pointToAdd = (Point)parameter;
				// point should be inside of allowed region to be allowed to be added
				result &= AllowedRegion.Contains(pointToAdd);
			}

			return result;
		}

		#endregion // end of Commands

		protected override void OnPoint_PositionChanged(object sender, PositionChangedEventArgs e)
		{
			DraggablePoint marker = (DraggablePoint)sender;

			// adjusting position to fit inside allowed region
			var region = AllowedRegion;
			var position = e.Position;
			if (position.X < region.XMin)
				position.X = region.XMin;
			if (position.Y < region.YMin)
				position.Y = region.YMin;
			if (position.X > region.XMax)
				position.X = region.XMax;
			if (position.Y > region.YMax)
				position.Y = region.YMax;

			marker.Position = position;

			var index = DevMarkerChart.GetIndex(marker);
			if (0 <= index && index < Points.Count && !ProtectedPoints.Changing)
			{
				Points[index] = position;
			}
		}
	}
}
