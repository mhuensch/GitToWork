using Microsoft.VisualStudio.Shell.Interop;
using Run00.GitWorkItems.Controls;
using Run00.GitWorkItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Run00.GitWorkItems
{
	internal static class ExtensionsForUiElement
	{
		public static T GetSelectedElement<T>(this UIElement element, MouseButtonEventArgs e)
		{
			var elem = (UIElement)element.InputHitTest(e.GetPosition(element));

			while (elem != null && elem != element)
			{
				if (elem is T)
					return (T)(object)elem;

				elem = VisualTreeHelper.GetParent(elem) as UIElement;
			}
			
			return default(T);
		}
	}
}
