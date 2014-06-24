using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		public string State { get; set; }

		public string Assignee { get; set; }

		public string Creator { get; set; }

		public string Mentioning { get; set; }

		public string Milestone { get; set; }

		public DateTime Since { get; set; }

		public string SortBy { get; set; }

		public string Direction { get; set; }

		public ObservableCollection<string> Labels { get; set; }

		public ObservableCollection<WorkItem> WorkItems { get; set; }

		public Query()
		{
			Title = "New Query";
			Labels = new ObservableCollection<string>();
			WorkItems = new ObservableCollection<WorkItem>();
		}
	}
}
