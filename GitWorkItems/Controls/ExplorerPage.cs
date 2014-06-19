using System;
using System.ComponentModel;

using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Controls;
using Run00.GitWorkItems.Views;
using Run00.GitWorkItems.Models;
using System.Windows.Threading;
using System.Threading;

namespace Run00.GitWorkItems.Controls
{
	[TeamExplorerPage(GuidList.ExplorerPageId)]
	public class ExplorerPage : ITeamExplorerPage
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public bool IsBusy { get; set; }

		public string Title { get; set; }
		 
		public object PageContent { get; set; }

		void ITeamExplorerPage.Initialize(object sender, PageInitializeEventArgs e)
		{
			Title = "Work Items";

			_serviceProvider = e.ServiceProvider;
			_gitProxy = _serviceProvider.GetService<GitControlProxy>();
			_gitProxy.AccountNotifier.PropertyChanged += OnAccountInformationChanged;

			_account = _gitProxy.Account;

			_explorer = new Explorer();
			_explorer.DataContext = _account;			
			_explorer.NewQueryLink.RequestNavigate += OnNewItemQueryClicked;
			_explorer.NewItemLink.RequestNavigate += OnNewWorkItemClicked;
			_explorer.CreateQueryLink.RequestNavigate += OnCreateQueryClicked;
			_explorer.AddQueryLink.RequestNavigate += OnAddQueryClicked;
			_explorer.QuerySelected += OnQuerySelected;

			PageContent = _explorer;
		}

		void ITeamExplorerPage.Cancel()
		{
		}

		object ITeamExplorerPage.GetExtensibilityService(Type serviceType)
		{
			return null;
		}

		void ITeamExplorerPage.Loaded(object sender, PageLoadedEventArgs e)
		{
		}

		void ITeamExplorerPage.Refresh()
		{
		}

		void ITeamExplorerPage.SaveContext(object sender, PageSaveContextEventArgs e)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private void OnAccountInformationChanged(object sender, PropertyChangedEventArgs e)
		{
			switch(e.PropertyName)
			{
				case "SelectedQuery":
					_serviceProvider.OpenNewTabWindow(GuidList.QueryResultsPaneId, _account.SelectedQuery);
					break;
				default:
					return;
			}
		}

		void AddItem(object item)
		{
			_account.SelectedQuery.WorkItems.Add(new Models.WorkItem() { Title = "Blaha" });
		}

		private void OnNewWorkItemClicked(object sender, EventArgs e)
		{
			_serviceProvider.OpenNewTabWindow(GuidList.NewItemPaneId, new Models.WorkItem());
		}

		private void OnNewItemQueryClicked(object sender, EventArgs e)
		{
			_serviceProvider.OpenNewTabWindow(GuidList.NewQueryPaneId, new Models.Query());
		}

		private void OnCreateQueryClicked(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		{
			_serviceProvider.OpenNewTabWindow(GuidList.NewQueryPaneId, new Models.Query());
		}

		private void OnAddQueryClicked(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		{
			//throw new NotImplementedException();
		}

		private void OnQuerySelected(object sender, EventArgs e)
		{
			var query = sender as Query;
			if (query == null)
				return;

			_account.SelectedQuery = query;
		}

		private Account _account;
		private IServiceProvider _serviceProvider;
		private Explorer _explorer;
		private GitControlProxy _gitProxy;
	}
}