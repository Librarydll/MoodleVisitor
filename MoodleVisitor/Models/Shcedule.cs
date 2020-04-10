using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoodleVisitor.Models
{
	[Serializable]

	public class Shcedule
	{
		[XmlElement(ElementName ="Subject")]
		public Subject[] Subjects { get; set; }

		public  IEnumerable<Subject> GetTodaySubjects()
		{
			return Subjects.Where(x => x.DayOfWeek == DateTime.Now.DayOfWeek);
		}
	}
}
