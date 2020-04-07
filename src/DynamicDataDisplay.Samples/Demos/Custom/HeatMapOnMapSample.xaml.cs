﻿using System.Windows.Controls;

namespace DynamicDataDisplay.Samples.Demos.Custom
{
	/// <summary>
	/// Interaction logic for HeatMapOnMapSample.xaml
	/// </summary>
	public partial class HeatMapOnMapSample : Page
	{
		public HeatMapOnMapSample()
		{
			InitializeComponent();
			heatmap.CreateRandomMapData();
		}
	}
}
