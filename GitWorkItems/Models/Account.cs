using Microsoft.VisualStudio.Shell;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Run00.GitWorkItems.Models 
{
	[ImplementPropertyChanged]
	public class Account : IModel
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public string RepositoryPath { get; set; }

		public Uri RepositoryUrl { get; set; }

		public string AccountName { get; set; }

		public string RepositoryName { get; set; }

		public ObservableCollection<Query> Queries { get; set; }

		public ObservableCollection<Query> Dashboards { get; set; }

		public bool MissingDashboards { get { return Dashboards.Count() == 0; } }

		public bool MissingQueries { get { return Queries.Count() == 0; } }

		public bool IsConnected()
		{
			return
				string.IsNullOrWhiteSpace(AccountName) == false &&
				string.IsNullOrWhiteSpace(RepositoryName) == false;
		}

		public Account()
		{
			if (Debugger.IsAttached)
			{
				MessageBox.Show("ha!");
			}
			Dashboards = new ObservableCollection<Query>() 
			{
				new Query { Id = Guid.NewGuid(), Title = "Dashboard One", Total = 5, UnreadCount = 2, WorkItems = new ObservableCollection<WorkItem> { new WorkItem() { Title = "mine" } }},
				new Query { Id = Guid.NewGuid(), Title = "Dashboard Two", Total = 5, UnreadCount = 2 },
				new Query { Id = Guid.NewGuid(), Title = "Dashboard Three", Total = 5, UnreadCount = 2 }
			};

			Queries = new ObservableCollection<Query>() 
			{
				new Query { Id = Guid.NewGuid(), Title = "Query One", Total = 5, UnreadCount = 2, Assignee="Michael", State="Created", SortBy="Comments" },
				new Query { Id = Guid.NewGuid(), Title = "Query Two", Total = 5, UnreadCount = 2 },
				new Query { Id = Guid.NewGuid(), Title = "Query Three", Total = 5, UnreadCount = 2 }
			};
		}

	}
}
