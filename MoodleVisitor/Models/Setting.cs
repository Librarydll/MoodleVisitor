using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoodleVisitor.Models
{
	[Serializable]
	public class Setting
	{
		[XmlElement(ElementName = "Time")]
		public string StringTime { get; set; }
		public bool AlreadyExecute { get; set; }

		[XmlIgnore]
		public TimeSpan Time => GetTimeFromString(StringTime);

		public bool AutoRunEnable { get; set; }
		public User User { get; set; }
		public TimeSpan GetTimeFromString(string s)
		{
			if (TimeSpan.TryParse(s, out TimeSpan result))
				return result;
			return new TimeSpan(8, 0, 0);
		}
	}
}
