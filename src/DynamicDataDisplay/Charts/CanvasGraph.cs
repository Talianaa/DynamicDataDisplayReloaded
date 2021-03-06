﻿using DynamicDataDisplay.Common;
using System.Windows;
using System.Windows.Controls;

namespace DynamicDataDisplay.Charts
{
	public abstract class CanvasGraph : Canvas, IPlotterElement
	{
		static CanvasGraph()
		{
			EventManager.RegisterClassHandler(typeof(CanvasGraph), Plotter.PlotterChangedEvent, new PlotterChangedEventHandler(OnPlotterChanged));
		}

		private static void OnPlotterChanged(object sender, PlotterChangedEventArgs e)
		{
			CanvasGraph owner = (CanvasGraph)sender;
			owner.OnPlotterChanged(e);
		}

		private void OnPlotterChanged(PlotterChangedEventArgs e)
		{
			if (plotter == null && e.CurrentPlotter != null)
			{
				plotter = (Plotter2D)e.CurrentPlotter;
				plotter.Viewport.PropertyChanged += Viewport_PropertyChanged;
				OnPlotterAttached();
			}
			if (plotter != null && e.PreviousPlotter != null)
			{
				OnPlotterDetaching();
				plotter.Viewport.PropertyChanged -= Viewport_PropertyChanged;
				plotter = null;
			}
		}

		#region IPlotterElement Members

		private void Viewport_PropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
		{
			OnViewportPropertyChanged(e);
		}

		protected virtual void OnViewportPropertyChanged(ExtendedPropertyChangedEventArgs e) { }

		protected virtual Panel HostPanel
		{
			get { return plotter.CentralGrid; }
		}

		void IPlotterElement.OnPlotterAttached(Plotter plotter)
		{
			this.plotter = (Plotter2D)plotter;
			HostPanel.Children.Add(this);
			this.plotter.Viewport.PropertyChanged += Viewport_PropertyChanged;

			OnPlotterAttached();
		}

		protected virtual void OnPlotterAttached() { }

		void IPlotterElement.OnPlotterDetaching(Plotter plotter)
		{
			OnPlotterDetaching();

			this.plotter.Viewport.PropertyChanged -= Viewport_PropertyChanged;
			HostPanel.Children.Remove(this);
			this.plotter = null;
		}

		protected virtual void OnPlotterDetaching() { }

		private Plotter2D plotter;
		protected Plotter2D Plotter2D
		{
			get { return plotter; }
		}

		Plotter IPlotterElement.Plotter
		{
			get { return plotter; }
		}

		#endregion
	}
}
