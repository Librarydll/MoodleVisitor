using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoodleVisitor.Models
{
	[Serializable]
	public class Subject
	{
		[XmlElement(ElementName = "SubjectName")]
		public string SubjectName { get; set; }
		[XmlAttribute(AttributeName = "StartingTime")]
		public DateTime StartingTime { get; set; }
		[XmlAttribute(AttributeName = "DayOfWeek")]
		public DayOfWeek DayOfWeek { get; set; }

	}
}
