using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Run00.GitWorkItems.Providers
{
	public class WorkItem
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string AssignedTo { get; set; }
		public string Milestone { get; set; }
		public ICollection<string> Tags { get; set; }
		public bool Unread { get; set; }
	}
}
