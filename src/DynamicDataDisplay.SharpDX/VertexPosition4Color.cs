﻿using SharpDX;

namespace Microsoft.Research.DynamicDataDisplay.SharpDX
{
	public struct VertexPosition4Color
	{
		public static int SizeInBytes
		{
			get { return 4 * sizeof(float) + sizeof(int); }
		}

		public Vector4 Position;
		public int Color;
	}
}
