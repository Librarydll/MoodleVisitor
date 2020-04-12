using MoodleVisitor.Models;
using MoodleVisitor.Models.Infrastructure;
using MoodleVisitor.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleVisitor.ViewModels
{
	public class SettingViewModel :BindableBase
	{
        private Setting _setting;
        private readonly ISettingProvider _settingProvider;

        private DelegateCommand _saveSettingCommand;
        public DelegateCommand SaveSettingCommand =>
            _saveSettingCommand ?? (_saveSettingCommand = new DelegateCommand(SaveSettings));


        public Setting Setting
        {
            get { return _setting; }
            set { SetProperty(ref _setting, value); }
        }

        public SettingViewModel(ISettingProvider settingProvider)
        {
            _settingProvider = settingProvider;
            InitializeSettings();
        }

        public void InitializeSettings()
        {
            Setting = _settingProvider.Setting;
        }

        private void SaveSettings()
        {
            _settingProvider.SaveSettings(Setting);
            if (!Setting.AutoRunEnable)
            {
                AutoRunManager.RemoveAutoRun("MoodleVisitor");
            }
        }
    }
}
