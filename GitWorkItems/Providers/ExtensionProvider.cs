using Microsoft.VisualStudio.Shell;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.GitWorkItems.Providers
{
	[ImplementPropertyChanged]
	public class ExtensionProvider
	{
		public ICollection<Query> Queries { get; set; }

		public ICollection<Query> Dashboards { get; set; }

		public bool MissingDashboards { get { return Dashboards.Count() == 0; } }

		public bool MissingQueries { get { return Queries.Count() == 0; } }

		public ExtensionProvider([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;

			Dashboards = new List<Query>(){
				new Query { Title = "Dashboard One" },
				new Query { Title = "Dashboard Two" },
				new Query { Title = "Dashboard Three" }
			};

			Queries = new List<Query>() {
				new Query { Title = "Query One" },
				new Query { Title = "Query Two" },
				new Query { Title = "Query Three" }
			};
		}

		private readonly IServiceProvider _serviceProvider;
	}
}
