using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleVisitor.Models.Infrastructure
{
	public interface ISettingProvider
	{
		Setting Setting { get; }
		void SaveSettings(Setting setting);
		void SetAsExecuted();
		void UnsetAsExecuted();
	}
}
