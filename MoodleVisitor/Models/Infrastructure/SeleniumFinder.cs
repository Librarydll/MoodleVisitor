using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleVisitor.Models.Infrastructure
{
	public static class SeleniumFinder
	{

		public static IWebElement FindElementById(this IWebDriver driver, string id)
		{
			return driver.FindElement(By.Id(id));
		}
		public static IWebElement FindElementByXpath(this IWebDriver driver, string id)
		{
			return driver.FindElement(By.XPath(id));
		}
		public static IReadOnlyCollection<IWebElement> FindElementsByXpath(this IWebDriver driver, string id)
		{
			return driver.FindElements(By.XPath(id));
		}

		public static IEnumerable<string> CreateLinkFromElements(this IEnumerable<IWebElement> webElements)
		{
			return webElements.Select(x => x.GetAttribute("href"));
		}
		public static string CreateLinkFromElements(this IWebElement webElements)
		{
			return webElements.GetAttribute("href");
		}
	}
}
