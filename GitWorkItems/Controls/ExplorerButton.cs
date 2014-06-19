using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.TeamFoundation.Controls.WPF.TeamExplorer.Framework;
using PropertyChanged;
using System.IO;
using Run00.GitWorkItems.Models;

namespace Run00.GitWorkItems.Controls
{
	[ImplementPropertyChanged]
	[TeamExplorerNavigationItem(GuidList.ExplorerButtonId, 100)]
	public class ExplorerButton : ITeamExplorerNavigationItem
	{
		public event PropertyChangedEventHandler PropertyChanged;
		
		public bool IsVisible { get; set; }

		public Image Image { get; set; }
	
		public string Text { get; set; }

		[ImportingConstructor]
		public ExplorerButton([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
		{
			Image = Resources.WorkItemIcon;
			Text = "Work Items";

			_serviceProvider = serviceProvider;

			var gitProxy = _serviceProvider.GetService<GitControlProxy>();
			if (gitProxy == null)
				return;

			gitProxy.AccountNotifier.PropertyChanged += OnAccountChanged;
		}
		
		void ITeamExplorerNavigationItem.Execute()
		{
			var teamExplorer = _serviceProvider.GetService<ITeamExplorer>();
			if (teamExplorer == null)
				return;

			teamExplorer.NavigateToPage(new Guid(GuidList.ExplorerPageId), null);
		}

		void ITeamExplorerNavigationItem.Invalidate()
		{
		}

		void IDisposable.Dispose()
		{
		}

		private void OnAccountChanged(object sender, EventArgs e)
		{
			var account = sender as Account;
			if (account == null)
				return;

			IsVisible = account.IsConnected();
		}

		private readonly IServiceProvider _serviceProvider;
		
	}
}