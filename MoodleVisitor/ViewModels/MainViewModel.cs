using MoodleVisitor.Models;
using MoodleVisitor.Models.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleVisitor.ViewModels
{
	public class MainViewModel :BindableBase
	{
        private string _login = "di313-17-2";
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _password="Di313-17-2";
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private DelegateCommand _startProccessCommand;
        private readonly SeleniumHelper seleniumHelper;

        public DelegateCommand StartProccessCommand =>
            _startProccessCommand ?? (_startProccessCommand = new DelegateCommand(ExecuteStartProccess));

        public MainViewModel(SeleniumHelper seleniumHelper)
        {
            this.seleniumHelper = seleniumHelper;
        }

        async void ExecuteStartProccess()
        {    
            await seleniumHelper.Start(new User { Login = Login, Password = Password });
        }
    }
}
