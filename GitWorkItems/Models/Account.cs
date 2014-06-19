using Microsoft.VisualStudio.Shell;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public ICollection<Query> Queries { get; set; }

		public ICollection<Query> Dashboards { get; set; }

		public Query SelectedQuery { get; set; }

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
			Dashboards = new List<Query>() 
			{
				new Query { Id = Guid.NewGuid(), Title = "Dashboard One", Total = 5, UnreadCount = 2, WorkItems = new List<WorkItem> { new WorkItem() { Title = "mine" } }},
				new Query { Id = Guid.NewGuid(), Title = "Dashboard Two", Total = 5, UnreadCount = 2 },
				new Query { Id = Guid.NewGuid(), Title = "Dashboard Three", Total = 5, UnreadCount = 2 }
			};

			Queries = new List<Query>() 
			{
				new Query { Id = Guid.NewGuid(), Title = "Query One", Total = 5, UnreadCount = 2 },
				new Query { Id = Guid.NewGuid(), Title = "Query Two", Total = 5, UnreadCount = 2 },
				new Query { Id = Guid.NewGuid(), Title = "Query Three", Total = 5, UnreadCount = 2 }
			};
		}

	}
}
