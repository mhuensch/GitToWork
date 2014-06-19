using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.GitWorkItems.Models
{
	[ImplementPropertyChanged]
	public class Query
	{
		public string Title { get; set; }
		
		public int Count { get; set; }
		
		public int UnreadCount { get; set; }

		public ICollection<WorkItem> WorkItems { get; set;}

		public Query()
		{
			WorkItems = new List<WorkItem>();
			{
				new WorkItem { Title = "WorkItem1", Unread = true };
			}
		}
	}
}
