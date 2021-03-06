﻿using System.Windows.Input;

namespace DynamicDataDisplay
{
	// TODO: probably optimize memory usage by replacing implicit creation of 
	// all commands on first usage of this class - 
	// create each command on first access directly to it.

	// To add new commands, create a stub here like the other commands
	// Then add the command implementation in d3\Charts\Navigation\KeyboardNavigation.cs 
	// (again use other commands as an example).  Also setup the command bindings
	// in the KeyboardNavigation.InitCommands() to link the RoutedUICommand defined here
	// to the implementation details in KeyboardNavigation

	/// <summary>Common chart plotter commands</summary>
	public static class ChartCommands
	{

		#region Auxiliary code for creation of commands

		private static RoutedUICommand CreateCommand(string name)
		{
			return new RoutedUICommand(name, name, typeof(ChartCommands));
		}

		private static RoutedUICommand CreateCommand(string name, params Key[] keys)
		{
			InputGestureCollection gestures = new InputGestureCollection();
			foreach (var key in keys)
			{
				gestures.Add(new KeyGesture(key));
			}
			return new RoutedUICommand(name, name, typeof(ChartCommands), gestures);
		}

		private static RoutedUICommand CreateCommand(string name, MouseAction mouseAction)
		{
			return new RoutedUICommand(name, name, typeof(ChartCommands), new InputGestureCollection { new MouseGesture(mouseAction) });
		}

		private static RoutedUICommand CreateCommand(string name, params InputGesture[] gestures)
		{
			return new RoutedUICommand(name, name, typeof(ChartCommands), new InputGestureCollection(gestures));
		}

		#endregion

		private static readonly RoutedUICommand zoomOutToMouse = CreateCommand("ZoomOutToMouse", MouseAction.RightDoubleClick);
		/// <summary>
		/// Gets the value that represents the Zoom Out To Mouse command.
		/// </summary>
		public static RoutedUICommand ZoomOutToMouse
		{
			get { return ChartCommands.zoomOutToMouse; }
		}

		private static readonly RoutedUICommand zoomInToMouse = CreateCommand("ZoomInToMouse", MouseAction.LeftDoubleClick);
		/// <summary>
		/// Gets the value that represents the zoom in to mouse command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ZoomInToMouse
		{
			get { return ChartCommands.zoomInToMouse; }
		}

		private static readonly RoutedUICommand zoomWithParam = CreateCommand("ZoomWithParam");
		/// <summary>
		/// Gets the value that represents the zoom with parameter command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ZoomWithParameter
		{
			get { return zoomWithParam; }
		}

		private static readonly RoutedUICommand zoomIn = CreateCommand("ZoomIn", Key.OemPlus);
		/// <summary>
		/// Gets the value that represents the zoom in command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ZoomIn
		{
			get { return zoomIn; }
		}

		private static readonly RoutedUICommand zoomOut = CreateCommand("ZoomOut", Key.OemMinus);
		/// <summary>
		/// Gets the value that represents the zoom out command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ZoomOut
		{
			get { return zoomOut; }
		}

		private static readonly RoutedUICommand lockZoomX = CreateCommand("LockZoomX");
		public static RoutedUICommand LockZoomX
		{
			get
			{
				return lockZoomX;
			}
		}

		private static readonly RoutedUICommand lockZoomY = CreateCommand("LockZoomY");
		public static RoutedUICommand LockZoomY
		{
			get
			{
				return lockZoomY;
			}
		}

		private static readonly RoutedUICommand editScreenshotParameters = CreateCommand("EditScreenshotParameters");
		public static RoutedUICommand EditScreenshotParameters
		{
			get
			{
				return editScreenshotParameters;
			}
		}

		private static readonly RoutedUICommand fitToView = CreateCommand("FitToView", Key.Home);
		/// <summary>
		/// Gets the value that represents the fit to view command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand FitToView
		{
			get { return ChartCommands.fitToView; }
		}

		private static readonly RoutedUICommand scrollLeft = CreateCommand("ScrollLeft", Key.Left);
		/// <summary>
		/// Gets the value that represents the scroll left command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ScrollLeft
		{
			get { return ChartCommands.scrollLeft; }
		}

		private static readonly RoutedUICommand scrollRight = CreateCommand("ScrollRight", Key.Right);
		/// <summary>
		/// Gets the value that represents the scroll right command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ScrollRight
		{
			get { return ChartCommands.scrollRight; }
		}

		private static readonly RoutedUICommand scrollUp = CreateCommand("ScrollUp", Key.Up);
		/// <summary>
		/// Gets the value that represents the scroll up command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ScrollUp
		{
			get { return ChartCommands.scrollUp; }
		}

		private static readonly RoutedUICommand scrollDown = CreateCommand("ScrollDown", Key.Down);
		/// <summary>
		/// Gets the value that represents the scroll down command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ScrollDown
		{
			get { return ChartCommands.scrollDown; }
		}

		private static readonly RoutedUICommand saveScreenshot = CreateCommand("SaveScreenshot", new KeyGesture(Key.S, ModifierKeys.Control));
		/// <summary>
		/// Gets the value that represents the save screenshot command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand SaveScreenshot
		{
			get { return ChartCommands.saveScreenshot; }
		}

		private static readonly RoutedUICommand highResScreenshot = CreateCommand("HighResScreenshot");
		/// <summary>
		/// Gets the value that represents the save screenshot command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand HighResScreenshot
		{
			get { return ChartCommands.highResScreenshot; }
		}


		private static readonly RoutedUICommand copyScreenshot = CreateCommand("CopyScreenshot", Key.F11);
		/// <summary>
		/// Gets the value that represents the copy screenshot command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand CopyScreenshot
		{
			get { return ChartCommands.copyScreenshot; }
		}

		private static readonly RoutedUICommand showHelp = CreateCommand("ShowHelp", Key.F1);
		/// <summary>
		/// Gets the value that represents the show help command.
		/// </summary>
		/// <value></value>
		public static RoutedUICommand ShowHelp
		{
			get { return ChartCommands.showHelp; }
		}
	}
}
