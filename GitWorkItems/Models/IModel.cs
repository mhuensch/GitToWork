using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.GitWorkItems.Models
{
	public interface IModel
	{
		Guid Id { get; }
		string Title { get; }
	}
}
