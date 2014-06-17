using PropertyChanged;
using Run00.GitWorkItems.TeamExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Run00.GitWorkItems.WorkItem
{
	[ImplementPropertyChanged]
	public class GitHubUrlProvider
	{
		
		public Uri Current { get; set; }

		public GitHubUrlProvider(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			_accountProvider = _serviceProvider.GetService<WorkItemAccountProvider>();

			SetCurrent(_accountProvider.RepositoryUrl);

			((INotifyPropertyChanged)_accountProvider).PropertyChanged += GitHubUrlProvider_PropertyChanged;
		}

		void GitHubUrlProvider_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			SetCurrent(_accountProvider.RepositoryUrl);
		}

		private void SetCurrent(Uri uri)
		{
			try
			{
				((HttpWebRequest)WebRequest.Create(uri)).GetResponse();
			}
			catch (WebException ex)
			{
				var status = ((HttpWebResponse)ex.Response).StatusCode;
				uri = new Uri("https://github.com/login");
			}

			if (uri == null)
				Current = new Uri("http://www.google.com/");
			else
				Current = uri;
		}

		private WorkItemAccountProvider _accountProvider;
		private IServiceProvider _serviceProvider;
	}
}
