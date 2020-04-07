﻿using System.Windows;
using System.Windows.Data;

namespace DynamicDataDisplay.Common.Auxiliary
{
	public static class BindingHelper
	{
		public static Binding CreateAttachedPropertyBinding(DependencyProperty attachedProperty)
		{
			return new Binding { Path = new PropertyPath("(0)", attachedProperty) };
		}
	}
}
