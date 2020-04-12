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
		public static IWebElement FindElementByXpath(this IWebDriver driver, string xpath)
		{
			return driver.FindElement(By.XPath(xpath));
		}
		public static IReadOnlyCollection<IWebElement> FindElementsByXpath(this IWebDriver driver, string xpath)
		{
			return driver.FindElements(By.XPath(xpath));
		}

		public static IEnumerable<string> CreateLinkFromElements(this IEnumerable<IWebElement> webElements)
		{
			return webElements.Select(x => x.GetAttribute("href"));
		}
		public static string CreateLinkFromElement(this IWebElement webElement)
		{
			return webElement.GetAttribute("href");
		}
		public static IEnumerable<IWebElement> CreatePageNumbersFromElements(this IEnumerable<IWebElement> webElements)
		{
			return webElements
				.Where(x => !string.IsNullOrEmpty(x.Text))
				.Where(x => int.TryParse(x.Text, out int result));
		}
	}
}
