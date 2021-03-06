﻿using DynamicDataDisplay.Charts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NewMarkersSample.Pages
{
	/// <summary>
	/// Interaction logic for AcceptableValuePage.xaml
	/// </summary>
	public partial class AcceptableValuePage : Page
	{
		public AcceptableValuePage()
		{
			InitializeComponent();
		}

		Segment[] segments = Segment.LoadSegments(40);
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			var newLegend = plotter.Children.OfType<NewLegend>().FirstOrDefault();
			plotter.Children.Remove(newLegend);
			DataContext = segments;
		}
	}
}
