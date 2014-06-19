using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.TeamFoundation.Controls;
using System.Windows;
using System.ComponentModel;
using Run00.GitWorkItems.Controls;
using Run00.GitWorkItems.Models;

namespace Run00.GitWorkItems
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the 
	/// IVsPackage interface and uses the registration attributes defined in the framework to 
	/// register itself and its components with the shell.
	/// </summary>
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
	// a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	// This attribute is needed to let the shell know that this package exposes some menus.
	[ProvideMenuResource("Menus.ctmenu", 1)]
	// This attribute registers a tool window exposed by this package.
	//[ProvideToolWindow(typeof(QueryListPane))]
	[ProvideToolWindow(typeof(QueryResultsPane))]
	[ProvideToolWindow(typeof(NewItemPane))]
	[ProvideToolWindow(typeof(NewQueryPane))]
	// Registering provider services to be located by this package.
	[ProvideService(typeof(GitControlProxy))]
	[Guid(GuidList.GitWorkItemsPkgStringId)]
	public sealed class GitWorkItemsPackage : Package
	{
		/// <summary>
		/// Default constructor of the package.
		/// Inside this method you can place any initialization code that does not require 
		/// any Visual Studio service because at this point the package object is created but 
		/// not sited yet inside Visual Studio environment. The place to do all the other 
		/// initialization is the Initialize method.
		/// </summary>
		public GitWorkItemsPackage()
		{
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));

			var serviceContainer = (IServiceContainer)this;
			serviceContainer.AddService(typeof(GitControlProxy), new ServiceCreatorCallback((c, s) => { return new GitControlProxy(this); }), true);
		}
	}
}
