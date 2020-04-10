﻿using MoodleVisitor.Models.Infrastructure;
using MoodleVisitor.Views;
using Prism.Ioc;
using Prism.Unity;
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
			IShceduleProvider scheduleProvider = new ShceduleProvider();
			scheduleProvider.CreateInitialSchedule("schedule.xml");
			containerRegistry.RegisterInstance(scheduleProvider);
			containerRegistry.Register<IShceduleProvider, ShceduleProvider>();
		}
		
	}
}
