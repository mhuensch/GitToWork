using Run00.GitWorkItems.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Run00.GitWorkItems.Views
{
	/// <summary>
	/// Interaction logic for Explorer.xaml
	/// </summary>
	public partial class Explorer : UserControl
	{
		//TODO: Create EventHandler for query
		public event EventHandler QuerySelected;
		public event EventHandler QueryDeleteSelected;

		public Explorer()
		{
			InitializeComponent();
		}
		
		private void OnQuerySelected(object sender, EventArgs args)
		{
			if (QuerySelected == null)
				return;

			var listBox = sender as ListBoxItem;
			if (listBox == null)
				return;

			QuerySelected.Invoke(listBox.Content, new EventArgs());
		}

		private void DeleteEvent(object sender, EventArgs args)
		{
			var query = ((System.Windows.FrameworkElement)(sender)).DataContext;

			if (QueryDeleteSelected == null)
				return;

			QueryDeleteSelected.Invoke(query, new EventArgs());
		}
	}
}
