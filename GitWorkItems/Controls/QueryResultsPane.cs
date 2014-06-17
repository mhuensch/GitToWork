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
	public class QueryResultsPane : ToolWindowPane
	{
		/// <summary>
		/// Standard constructor for the tool window.
		/// </summary>
		public QueryResultsPane()
			: base(null)
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
			_view = new WorkItemList();
			
			var items = new List<object>() {
				new { Title = "Title One", Closed = false},
				new { Title = "Title Two", Closed = true},
				new { Title = "Title Three", Closed = false}
			};

			foreach(var eachItem in items)
			{
				var itemView = new WorkItem();
				itemView.Status.Source = "\uf12a".ToFontAwesomeIcon(Brushes.Red);
				//itemView.Status.Source = "\uf00c".ToFontAwesomeIcon();

				var checkbox = new CheckBox();

				var listItem = new ListBoxItem();
				listItem.Selected += listItem_Selected;
				listItem.MouseDoubleClick += listItem_MouseDoubleClick;

				var panel = new StackPanel();
				panel.Orientation = Orientation.Horizontal;
				panel.Children.Add(checkbox);
				panel.Children.Add(itemView);

				listItem.Content = panel;
				_view.WorkItems.Items.Add(listItem);

			}
			//view.Sample.Source = FontAwesome.GetIcon("\uf046");

			
			base.Content = _view;

			_preview = new WorkItemEditor();
		}

		void listItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			((IServiceProvider)this).OpenNewTabWindow(GuidList.NewItemPaneId, "Editing");
		}

		void listItem_Selected(object sender, RoutedEventArgs e)
		{
			if (_view.Preview.Children.Count == 0)
				return;

			var preivewContent = _view.Preview.Children[0] as WorkItemEditor;
			if (preivewContent == null)
			{
				_view.Preview.Children.Clear();
				_view.Preview.Children.Add(_preview);
			}
		}

		private readonly WorkItemEditor _preview;
		private readonly WorkItemList _view;
	}
}
