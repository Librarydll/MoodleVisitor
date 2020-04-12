using MoodleVisitor.Models.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
				MessageBox.Show(ex.Message);
			}
		}
		private void CreateInitialSettingFile()
		{
			Setting setting = new Setting()
			{
				StringTime = new TimeSpan(8, 0, 0).ToString(),
				User =new User { Login = "di313-17-n", Password = "Di313-17-n" },
				AutoRunEnable =true
			};

			serializer.Serialize(setting, FILE_NAME);
		}

		private Setting GetSettingFromFile()
		{
			return serializer.DeSerialize(FILE_NAME);
		}

		public void SaveSettings(Setting setting)
		{
			if (File.Exists(FILE_NAME))
				serializer.Serialize(setting, FILE_NAME);
			else
			{
				MessageBox.Show("Can not save setting ,file does not exist");
			}
		}

		public void SetAsExecuted()
		{
			Setting.AlreadyExecute = true;
			serializer.Serialize(Setting, FILE_NAME);
		}

		public void UnsetAsExecuted()
		{
			Setting.AlreadyExecute = false;
			serializer.Serialize(Setting, FILE_NAME);
		}
	}
}
