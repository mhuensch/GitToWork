using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using PropertyChanged;
using Run00.GitWorkItems.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Run00.GitWorkItems.Controls
{
	public class GitControlProxy
	{
		public event EventHandler AccountInformationChanged;

		public GitControlProxy([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
		{
			serviceProvider
				.GetService<ITeamExplorer>()
				.GetPropertyValue<object>("TeamExplorerManager")
				.GetPropertyValue<object>("ViewModel")
				.AddEventHandler("PropertyChanged", (PropertyChangedEventHandler)((s, e) =>
				{
					OnTeamExplorerManagerViewModelChanged(s, e);
				}));

			_account = new Account();
			((INotifyPropertyChanged)_account).PropertyChanged += OnAccountInfromationChanged;
		}

		public bool IsConnected()
		{
			return
				string.IsNullOrWhiteSpace(_account.AccountName) == false &&
				string.IsNullOrWhiteSpace(_account.RepositoryName) == false;
		}

		private void OnAccountInfromationChanged(object sender, PropertyChangedEventArgs e)
		{
			if (AccountInformationChanged == null)
				return;

			AccountInformationChanged.Invoke(this, new EventArgs());
		}

		private void OnTeamExplorerManagerViewModelChanged(object sender, PropertyChangedEventArgs e)
		{
			var statusSercice = sender
				.GetPropertyValue<object>("CurrentContextInfoProvider")
				.GetPropertyValue<object>("Instance")
				.GetPropertyValue<object>("StatusService")
				.AddEventHandler("RepositoryPathChanged", (EventHandler)((s, a) =>
				{
					OnRepositoryPathChanged(s, a);
				}));

			if (statusSercice == null)
				return;

			UpdateRepositoryInfo(statusSercice);
		}

		private void OnRepositoryPathChanged(object sender, EventArgs e)
		{
			UpdateRepositoryInfo(sender);
		}

		private void UpdateRepositoryInfo(object statusService)
		{
			_account.RepositoryPath = statusService.GetPropertyValue<string>("RepositoryPath");
			_account.RepositoryUrl = null;
			_account.AccountName = null;
			_account.RepositoryName = null;

			if (string.IsNullOrWhiteSpace(_account.RepositoryPath))
				return;

			var filePath = Path.Combine(_account.RepositoryPath, @".git\config");
			if (File.Exists(filePath) == false)
				return;

			var ini = ReadIni(filePath);
			var url = ini["remote \"origin\""]["url"];

			if (string.IsNullOrWhiteSpace(url))
				return;

			Uri uri;
			Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri);
			if (uri == null)
				return;

			_account.RepositoryUrl = uri;

			var account = uri.AbsolutePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			_account.AccountName = account.First();
			_account.RepositoryName = account.Skip(1).First();
		}

		private Dictionary<string, Dictionary<string, string>> ReadIni(string filePath)
		{
			string data = File.ReadAllText(filePath);
			string pattern = @"
				^                           # Beginning of the line
				((?:\[)                     # Section Start
						 (?<Section>[^\]]*)     # Actual Section text into Section Group
				 (?:\])                     # Section End then EOL/EOB
				 (?:[\r\n]{0,}|\Z))         # Match but don't capture the CRLF or EOB
				 (                          # Begin capture groups (Key Value Pairs)
					(?!\[)                    # Stop capture groups if a [ is found; new section
					(?<Key>[^=]*?)            # Any text before the =, matched few as possible
					(?:=)                     # Get the = now
					(?<Value>[^\r\n]*)        # Get everything that is not an Line Changes
					(?:[\r\n]{0,4})           # MBDC \r\n
					)+                        # End Capture groups";

			return (
				from Match m in Regex.Matches(data, pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
				select new
				{
					Section = m.Groups["Section"].Value.Trim(),

					kvps = (
						from cpKey in m.Groups["Key"].Captures.Cast<Capture>().Select((a, i) => new { Value=a.Value.Trim(), i })
						join cpValue in m.Groups["Value"].Captures.Cast<Capture>().Select((b, i) => new { b.Value, i }) on cpKey.i equals cpValue.i
						select new KeyValuePair<string, string>(cpKey.Value, cpValue.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)

				}).ToDictionary(itm => itm.Section, itm => itm.kvps);

			//InIFile["WindowSettings"]["Window Name"];
		}

		private readonly Account _account;

	}
}
