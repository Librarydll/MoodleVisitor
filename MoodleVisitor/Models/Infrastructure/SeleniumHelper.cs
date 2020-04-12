﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MoodleVisitor.Models.Infrastructure
{
	public class SeleniumHelper : IDisposable
	{
		public readonly string url = "http://moodle.samtuit.uz/login/index.php";
		public readonly string chromeDriverPath = AppDomain.CurrentDomain.BaseDirectory;
		private IList<string> _navigateLink = new List<string>();
		private readonly IShceduleProvider _shceduleProvider;
		private MoodleSiteControlNames moodleSiteControlNames = new MoodleSiteControlNames();
		private IWebDriver _webDriver;
		public SeleniumHelper(IShceduleProvider shceduleProvider)
		{
			_shceduleProvider = shceduleProvider;
			_shceduleProvider.GetScheduleFromFile("schedule.xml");
		}

		public async Task Start(User user, CancellationToken token = default)
		{
			InitializeChromeDriver();

			await Task.Run(async () =>
		   {
			   _webDriver.Navigate().GoToUrl(url);

			   await LoginViewAction(user);
			   await CourseViewAction();

		   }, token);
		}

		private void InitializeChromeDriver()
		{
			try
			{
				if (!File.Exists("chromedriver.exe")) throw new FileNotFoundException("chromedriver.exe file not found");
				_webDriver = new ChromeDriver(chromeDriverPath);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public async Task LoginViewAction(User user)
		{
			await Task.Run(() =>
			{
				_webDriver.FindElementById(moodleSiteControlNames.UserNameInputId).SendKeys(user.Login);
				_webDriver.FindElementById(moodleSiteControlNames.PasswordInputId).SendKeys(user.Password);
				_webDriver.FindElementById(moodleSiteControlNames.LoginInButtonId).Click();
			});
		}

		public async Task CourseViewAction()
		{
			await Task.Run(async() =>
				{
					//Swtich to Курсы
					_webDriver.FindElementByXpath(moodleSiteControlNames.TabContorlCourseXpath).Click();
					var pages = _webDriver.FindElementsByXpath(moodleSiteControlNames.Pagging).CreatePageNumbersFromElements();
					foreach (var pageNumber in pages)
					{
						pageNumber.Click();
						var subjectNames = _webDriver.FindElementsByXpath(moodleSiteControlNames.TabControlElementsALocation)
											.Where(x => !string.IsNullOrWhiteSpace(x.Text));

						foreach (Subject subject in _shceduleProvider.Shcedule.GetTodaySubjects())
						{
							var link = subjectNames.Where(x => subject.SubjectName.Equals(x.Text)).CreateLinkFromElements().FirstOrDefault();
							if(!string.IsNullOrWhiteSpace(link))
							_navigateLink.Add(link);
						}
					 await	OpenCourses();
					}

				});

		}

		public async Task OpenCourses()
		{
			foreach (var link in _navigateLink)
			{
				((IJavaScriptExecutor)_webDriver).ExecuteScript("window.open();");
				_webDriver.SwitchTo().Window(_webDriver.WindowHandles.Last());
				_webDriver.Navigate().GoToUrl(link);
				await LectureClick().ContinueWith(x => _webDriver.SwitchTo().Window(_webDriver.WindowHandles.First()));
			}

		}

		public async Task LectureClick()
		{
			try
			{
				var ulLiList = _webDriver.FindElementsByXpath(moodleSiteControlNames.LectureList);

				foreach (var li in ulLiList)
				{
					if (li.GetAttribute("class").Contains("resource"))
					{
						var imglist = li.FindElements(By.TagName("img"));
						var imglast = imglist.Last();
						var imgfirst= imglist.First();
						if (imgfirst.GetAttribute("href").Contains("document-24"))
						{
							if (imglast.GetAttribute("alt").Contains(MoodleSiteControlNames.INCOMPLETED))
							{
								var a = li.FindElement(By.TagName("a"));
								a.Click();
								await Task.Delay(2000);
							}
						}
						
					}
				}
			}
			catch (StaleElementReferenceException)
			{
				MessageBox.Show("че та с сайтом не то");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		public void Dispose()
		{
			if (_webDriver != null)
			{
				_webDriver.Close();
				_webDriver.Quit();
				_webDriver = null;
			}
		}
	}
}
