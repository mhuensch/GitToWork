using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Run00.GitWorkItems.Models
{
	public class WorkItem : IModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string AssignedTo { get; set; }
		public string Milestone { get; set; }
		public ObservableCollection<string> Tags { get; set; }
		public bool Unread { get; set; }

		public WorkItem()
		{
			Title = "New Work Item";
			Tags = new ObservableCollection<string>();
		}
	}
}
