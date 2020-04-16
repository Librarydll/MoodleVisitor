using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleVisitor.Models.Infrastructure
{
	public class MoodleSiteControlNames
	{
		public const  string COMPLETED = "Выполнено";
		public const  string INCOMPLETED = "Не выполнено";


		public string UserNameInputId { get; } = "username";
		public string PasswordInputId { get; } = "password";
		public string LoginInButtonId { get; } = "loginbtn";
		public string TabContorlCourseXpath { get; } = "//ul/li[position()=2]/a";
		public string TabControlElementsALocation { get; set; } = "//div[contains(@class, 'in')]//div[contains(@class, 'media-body')]/h4/a";
		public string Pagging { get; set; } = "//a[contains(@class,'page-link')]";
		public string IsCompletedList { get; set; } = "//ul[contains(@class,'topics')]/li[position()>1]//form//button//img";
		public string LectureList { get; set; } = "//ul[contains(@class,'topics')]/li[position()>1]//ul/li";
		public string ApplyButtonXPath { get; set; } = "//ul[contains(@class,'topics')]/li//form//button";
	}
}
