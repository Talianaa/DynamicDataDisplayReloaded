﻿using Microsoft.Research.DynamicDataDisplay.Charts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DynamicDataDisplay.Markers
{
	public class PieChartLegendItem : NewLegendItem
	{
		static PieChartLegendItem()
		{
			var thisType = typeof(PieChartLegendItem);
			DefaultStyleKeyProperty.OverrideMetadata(thisType, new FrameworkPropertyMetadata(thisType));
		}

		public PieChartLegendItem(PieChartItem pieChartItem, PieChart chart)
		{
			ResourceDictionary dict = (ResourceDictionary)Application.LoadComponent(new Uri("/DynamicDataDisplay.Markers;component/PieChart files/PieChartResources.xaml", UriKind.Relative));

			//NewLegend.SetChart(this, pieChartItem);
			DataContext = pieChartItem.DataContext;

			Style selfStyle = (Style)dict[GetType()];
			Style = selfStyle;

			Binding fillBinding = new Binding { Path = new PropertyPath("Background"), Source = pieChartItem };
			SetBinding(BackgroundProperty, fillBinding);

			if (chart.DependentValueBinding != null)
			{
				SetBinding(NewLegend.DescriptionProperty, chart.DependentValueBinding);
			}

			bool setTooltipBinding = !string.IsNullOrEmpty(chart.IndependentValuePath) && !string.IsNullOrEmpty(chart.DependentValuePath);
			if (setTooltipBinding)
			{
				MultiBinding tooltipBinding = new MultiBinding { Converter = new PieLegendItemTooltipConverter() };
				tooltipBinding.Bindings.Add(chart.DependentValueBinding);
				tooltipBinding.Bindings.Add(chart.IndependentValueBinding);
				SetBinding(ToolTipService.ToolTipProperty, tooltipBinding);
			}
		}
	}
}
