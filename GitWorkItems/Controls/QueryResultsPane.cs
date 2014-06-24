using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using Microsoft.VisualStudio.Shell.Interop;
using Run00.GitWorkItems.Views;
using Run00.GitWorkItems.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace Run00.GitWorkItems.Controls
{
	/// <summary>
	/// This class implements the tool window exposed by this package and hosts a user control.
	///
	/// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, 
	/// usually implemented by the package implementer.
	///
	/// This class derives from the ToolWindowPane class provided from the MPF in order to use its 
	/// implementation of the IVsUIElementPane interface.
	/// </summary>
	[Guid(GuidList.QueryResultsPaneId)]
	public class QueryResultsPane : ToolWindowPane, IModelControl
	{
		/// <summary>
		/// Standard constructor for the tool window.
		/// </summary>
		public QueryResultsPane() : base(null)
		{
			// Set the window title reading it from the resources.
			this.Caption = Resources.ToolWindowTitle;

			// Set the image that will appear on the tab of the window frame
			// when docked with an other window
			// The resource ID correspond to the one defined in the resx file
			// while the Index is the offset in the bitmap strip. Each image in
			// the strip being 16x16.
			this.BitmapResourceID = 301;
			this.BitmapIndex = 1;

			// This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
			// we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on 
			// the object returned by the Content property.
			_view = new QueryResults();
			_preview = new WorkItemEditor();
			_view.Refresh.MouseLeftButtonUp += OnRefreshLeftMouseButtonUp;
			_view.Edit.MouseLeftButtonUp += OnEditClicked;
			_view.Results.MouseDoubleClick += OnListItemDoubleClick;
			base.Content = _view;
		}

		public void InitalizeModel(object model)
		{
			this.Caption.ToString();
			_view.DataContext = model;
		}

		private void OnListItemDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var uiSender = sender as UIElement;
			if (uiSender == null)
				return;

			var item = uiSender.GetSelectedElement<ListBoxItem>(e);
			if (item == null)
				return;

			var workItem = item.Content as Models.WorkItem;
			if (workItem == null)
				return;

			this.OpenNewTabWindow(GuidList.NewItemPaneId, workItem);
		}

		private void OnEditClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (_view.Editor.Visibility == Visibility.Visible)
				_view.Editor.Visibility = Visibility.Collapsed;
			else
				_view.Editor.Visibility = Visibility.Visible;
		}

		private void OnRefreshLeftMouseButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var proxy = this.GetService<GitControlProxy>();
			var query = _view.DataContext as Query;
			if (query == null)
				return;

			proxy.UpdateQuery(query);
		}

		private void listItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.OpenNewTabWindow(GuidList.NewItemPaneId, new Models.WorkItem());
		}

		private void listItem_Selected(object sender, RoutedEventArgs e)
		{
			//if (_view.Preview.Children.Count == 0)
			//	return;

			//var preivewContent = _view.Preview.Children[0] as WorkItemEditor;
			//if (preivewContent == null)
			//{
			//	_view.Preview.Children.Clear();
			//	_view.Preview.Children.Add(_preview);
			//}
		}

		private readonly WorkItemEditor _preview;
		private readonly QueryResults _view;
	}
}
