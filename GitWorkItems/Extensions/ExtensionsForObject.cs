using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Run00.GitWorkItems
{
	internal static class ExtensionsForObject
	{
		public static T GetPropertyValue<T>(this object obj, string name)
		{
			if (obj == null)
				return default(T);

			var prop = obj.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (prop == null)
				return default(T);

			return (T)prop.GetValue(obj);
		}

		public static object AddEventHandler(this object obj, string name, Delegate function)
		{
			if (obj == null)
				return null;

			var eventInfo = obj.GetType().GetEvent(name);
			if (eventInfo == null)
				return null;

			var removeHandler = eventInfo.GetRemoveMethod();
			removeHandler.Invoke(obj, new object[] { function });

			var addHandler = eventInfo.GetAddMethod();
			addHandler.Invoke(obj, new object[] { function });
			
			return obj;
		}
	}
}
