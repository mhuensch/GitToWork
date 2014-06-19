//using Microsoft.VisualStudio.Shell;
//using System;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Linq;
//using Microsoft.VisualStudio.Shell.Interop;
//using Run00.GitWorkItems.Views;

//namespace Run00.GitWorkItems.Controls
//{
//	/// <summary>
//	/// This class implements the tool window exposed by this package and hosts a user control.
//	///
//	/// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, 
//	/// usually implemented by the package implementer.
//	///
//	/// This class derives from the ToolWindowPane class provided from the MPF in order to use its 
//	/// implementation of the IVsUIElementPane interface.
//	/// </summary>
//	[Guid(GuidList.QueryResultsPaneId)]
//	public class QueryListPane : ToolWindowPane
//	{
//		/// <summary>
//		/// Standard constructor for the tool window.
//		/// </summary>
//		public QueryListPane() : base(null)
//		{
//			// Set the window title reading it from the resources.
//			this.Caption = Resources.ToolWindowTitle;
//			// Set the image that will appear on the tab of the window frame
//			// when docked with an other window
//			// The resource ID correspond to the one defined in the resx file
//			// while the Index is the offset in the bitmap strip. Each image in
//			// the strip being 16x16.
//			this.BitmapResourceID = 301;
//			this.BitmapIndex = 1;

//			// This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
//			// we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on 
//			// the object returned by the Content property.
//			_view = new QueryList();
//			base.Content = _view;
//		}

//		private readonly QueryList _view;
//	}
//}
