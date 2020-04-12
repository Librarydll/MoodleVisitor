using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleVisitor.Services
{
	public class AutoRunManager
	{
		private static readonly RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private static bool IsStartupItem()
        {
            if (rkApp.GetValue("My app's name") == null)
                return false;
            else
                return true;
        }

        public static void SetAutoRun(string applicationName)
        {
            if (!IsStartupItem())
                rkApp.SetValue(applicationName, System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static void RemoveAutoRun(string applicationName)
        {
            if (!IsStartupItem())
                rkApp.DeleteValue(applicationName, false);
        }

    }
	
}
