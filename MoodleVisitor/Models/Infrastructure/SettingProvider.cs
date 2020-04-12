using MoodleVisitor.Models.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleVisitor.Models.Infrastructure
{
	public class SettingProvider: ISettingProvider
	{
		public Setting Setting { get; private set; }
		XmlSerializerHelper<Setting> serializer = new XmlSerializerHelper<Setting>();
		private const string FILE_NAME = "setting.xml";
		public SettingProvider()
		{
			InitializeSetting();
		}

		private void InitializeSetting()
		{
			try
			{
				if (!File.Exists(FILE_NAME))
					CreateInitialSettingFile();
				Setting = GetSettingFromFile();
			}
			catch (Exception ex)
			{

				throw;
			}
		}
		private void CreateInitialSettingFile()
		{
			Setting setting = new Setting()
			{
				StringTime = new TimeSpan(8, 0, 0).ToString()
			};

			serializer.Serialize(setting, FILE_NAME);
		}

		private Setting GetSettingFromFile()
		{
			return serializer.DeSerialize(FILE_NAME);
		}

	}
}
