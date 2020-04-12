using MoodleVisitor.Models.Infrastructure;
using MoodleVisitor.Services;
using MoodleVisitor.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Windows;
using Unity;
namespace MoodleVisitor
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			return (Window)Container.Resolve(typeof(MainView));
		}
		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register<IShceduleProvider, ShceduleProvider>();
			containerRegistry.Register<ISettingProvider, SettingProvider>();
		}

		protected override void OnInitialized()
		{
			AutoRunManager.SetAutoRun("MoodleVisitor");
			base.OnInitialized();
		}

	}
}
