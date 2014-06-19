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
	public class Account
	{
		public string RepositoryPath { get; set; }

		public Uri RepositoryUrl { get; set; }

		public string AccountName { get; set; }

		public string RepositoryName { get; set; }

		public ICollection<Query> Queries { get; set; }

		public ICollection<Query> Dashboards { get; set; }

		public Query SelectedQuery { get; set; }

		public bool MissingDashboards { get { return Dashboards.Count() == 0; } }

		public bool MissingQueries { get { return Queries.Count() == 0; } }

		public Account()
		{
			Dashboards = new List<Query>() 
			{
				new Query { Title = "Dashboard One" },
				new Query { Title = "Dashboard Two" },
				new Query { Title = "Dashboard Three" }
			};

			Queries = new List<Query>() 
			{
				new Query { Title = "Query One" },
				new Query { Title = "Query Two" },
				new Query { Title = "Query Three" }
			};
		}
	}
}
