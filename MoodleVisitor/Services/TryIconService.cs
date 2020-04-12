using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoodleVisitor.Services
{
	public class TryIconService:IDisposable
	{
		public event Action<HeaderName> OnMenuItemClick;
		public TaskbarIcon TaskbarIcon { get; set; } = new TaskbarIcon();

		public TryIconService(string iconpath)
		{
			TaskbarIcon.Icon = new Icon(iconpath);
			TaskbarIcon.Visibility = Visibility.Visible;
			TaskbarIcon.PopupActivation = PopupActivationMode.RightClick;
			var c =new ContextMenu();
			var menu = new MenuItem() { Header = HeaderName.Quit };
			menu.Click += Menu_Click;
			c.Items.Add(menu);
			
			TaskbarIcon.ContextMenu = c;
		}

		private void Menu_Click(object sender, RoutedEventArgs e)
		{
			var menu = sender as MenuItem;
			OnMenuItemClick?.Invoke((HeaderName)menu.Header);
		}

		public void SetDoubleClickCommand(ICommand command)
		{
			TaskbarIcon.DoubleClickCommand = command;
		}

		public void Dispose()
		{
			if(TaskbarIcon!=null)
			TaskbarIcon.Visibility = Visibility.Hidden;
			TaskbarIcon = null;
		}
	}

	public enum HeaderName
	{
		Quit
	}
}
