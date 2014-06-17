using Microsoft.VisualStudio.Shell.Interop;
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

		public static void OpenNewTabWindow(this IServiceProvider serviceProvider, string guid, string title, bool forceNew = false)
		{
			var shell = serviceProvider.GetService<IVsUIShell>();

			var guidNo = new Guid(guid);

			//TODO: Register windows by id so that random and title hash codes are not needed
			var id = uint.Parse((new Random()).Next().ToString());
			if (forceNew == false)
				id = uint.Parse(title.GetHashCode().ToString());

			IVsWindowFrame winFrame;
			if (shell.FindToolWindowEx(0x80000, ref guidNo, id, out winFrame) >= 0 && winFrame != null)
			{
				winFrame.SetProperty((int)__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_MdiChild);
				winFrame.SetProperty((int)__VSFPROPID.VSFPROPID_Caption, title);
				winFrame.Show();
			}
		}
	}
}
