using Microsoft.VisualStudio.Shell.Interop;
using Run00.GitWorkItems.Controls;
using Run00.GitWorkItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run00.GitWorkItems
{
	internal static class ExtensionsForIServiceProvider
	{
		public static T GetService<T>(this IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
				return default(T);

			return (T)serviceProvider.GetService(typeof(T));
		}

		public static void OpenNewTabWindow(this IServiceProvider serviceProvider, string controlGuid, IModel model)
		{
			var shell = serviceProvider.GetService<IVsUIShell>();

			//NOTE: if the guid is empty, id will be 0 and a new window will be created
			//if the guid is not empty, a new window will be created if one doesnt already exist
			var id = (uint)model.GetHashCode();
			var guid = Guid.Parse(controlGuid);

			IVsWindowFrame winFrame;
			if (shell.FindToolWindowEx(0x80000, ref guid, id, out winFrame) < 0 || winFrame == null)
				return;

			winFrame.SetProperty((int)__VSFPROPID.VSFPROPID_Caption, model.Title);

			object obj;
			winFrame.GetProperty((int)__VSFPROPID.VSFPROPID_DocView, out obj);

			if (obj is IModelControl)
				((IModelControl)obj).InitalizeModel(model);

			winFrame.Show();
		}

	}
}
