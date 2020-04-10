using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleVisitor.Models.Infrastructure
{
	public class MoodleSiteControlNames
	{
		public string UserNameInputId { get; } = "username";
		public string PasswordInputId { get; } = "password";
		public string LoginInButtonId { get; } = "loginbtn";
		public string TabContorlCourseXpath { get; } = "//ul/li[position()=2]/a";
		public string TabControlElementsALocation { get; set; } = "//div[contains(@class, 'in ')]//div[contains(@class, 'media-body')]/h4/a";
	}
}
