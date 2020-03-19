﻿using System.Windows.Shapes;

namespace DynamicDataDisplay.Markers
{
	public class RectangleMarker : ShapeMarker
	{
		protected override Shape CreateShape()
		{
			return new Rectangle();
		}
	}
}
