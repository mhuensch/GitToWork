using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.GitWorkItems.Models
{
	[ImplementPropertyChanged]
	public class Query : IModel
	{
		public Guid Id { get; set; }

		public string Title { get; set; }
		
		public int Total { get; set; }
		
		public int UnreadCount { get; set; }

		public ICollection<WorkItem> WorkItems { get; set; }

		public Query()
		{
			Title = "New Query";
			WorkItems = new List<WorkItem>();
		}
	}
}
