using Hardcodet.Wpf.TaskbarNotification;
using MoodleVisitor.Models;
using MoodleVisitor.Models.Infrastructure;
using MoodleVisitor.Properties;
using MoodleVisitor.Services;
using MoodleVisitor.UIControls;
using MoodleVisitor.UIControls.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Unity;

namespace MoodleVisitor.ViewModels
{
	public class MainViewModel :BindableBase
	{
        private bool _closeWindow = false;
        private TryIconService iconService =null ;
        private const string REGION_NAME = "MainRegion";
        private User _user =new User() { Login= "di313-17-2",Password= "Di313-17-2" };
        public User User
        {
            get { return _user =new User() { Login= "di313-17-2",Password= "Di313-17-2" }; }
            set { SetProperty(ref _user , value); }
        }
        private Visibility _windowVisibility = Visibility.Visible;

        public Visibility WindowVisibility
        {
            get { return _windowVisibility; }
            set { SetProperty(ref _windowVisibility, value); }
        }



        private DelegateCommand _startProccessCommand;
        private readonly SeleniumHelper _seleniumHelper;
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;
        private readonly ISettingProvider _settingProvider;
        private LoginView loginView;
        private SettingView settingView;
        private DispatcherTimer timer;
        public DelegateCommand StartProccessCommand =>
            _startProccessCommand ?? (_startProccessCommand = new DelegateCommand(ExecuteStartProccess));

        private DelegateCommand<string> _changeViewCommand;
        public DelegateCommand<string> ChangeViewCommand =>
            _changeViewCommand ?? (_changeViewCommand = new DelegateCommand<string>(ExecuteChangeView));

        private DelegateCommand<CancelEventArgs> _onWindowsClosingCommand;
        public DelegateCommand<CancelEventArgs> OnWindowsClosingCommand =>
            _onWindowsClosingCommand ?? (_onWindowsClosingCommand = new DelegateCommand<CancelEventArgs>(OnWindowsClosing));

        private DelegateCommand _openWindowCommand;
        public DelegateCommand OpenWindowCommand =>
            _openWindowCommand ?? (_openWindowCommand = new DelegateCommand(OpenWindow));

        private void OpenWindow()
        {
            WindowVisibility = Visibility.Visible;
        }

        private void OnWindowsClosing(CancelEventArgs e)
        {
            if (!_closeWindow)
            {
                e.Cancel = true;
                WindowVisibility = Visibility.Hidden;
            }
            else
            {
                _seleniumHelper.Dispose();
                iconService.Dispose();
                Application.Current.Shutdown();
            }

        }
        void ExecuteChangeView(string parameter)
        {
            var view =(ViewName)Enum.Parse(typeof(ViewName),parameter);
            switch (view)
            {
                case ViewName.LoginView:
                    _regionManager.Regions[REGION_NAME].RemoveAll();
                    _regionManager.Regions[REGION_NAME].Add(loginView);
                    break;
                case ViewName.SettingView:
                    _regionManager.Regions[REGION_NAME].RemoveAll();
                    _regionManager.Regions[REGION_NAME].Add(settingView);
                    break;
                default:
                    break;
            }
        }

        public MainViewModel(SeleniumHelper seleniumHelper,IRegionManager regionManager,IUnityContainer unityContainer,ISettingProvider settingProvider)
        {
            InitializeIconService();
             _seleniumHelper = seleniumHelper;
            _regionManager = regionManager;
            _unityContainer = unityContainer;
            _settingProvider = settingProvider;
            InitializeView();
            InitializeTimer();
        }

        private void IconService_OnMenuItemClick(HeaderName headerName)
        {
            switch (headerName)
            {
                case HeaderName.Quit:
                    _closeWindow = true;
                    OnWindowsClosing(null);
                    break;
                default:
                    break;
            }
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
        }
        private void InitializeIconService()
        {
            iconService = new TryIconService("a.ico");
            iconService.SetDoubleClickCommand(OpenWindowCommand);
            iconService.OnMenuItemClick += IconService_OnMenuItemClick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int settingHour= _settingProvider.Setting.Time.Hours;
            int nowHour = DateTime.Now.TimeOfDay.Hours;

            if (!_settingProvider.Setting.AlreadyExecute)
            {
                if (settingHour >= nowHour - 2)
                {
                    ExecuteStartProccess();
                    _settingProvider.Setting.AlreadyExecute = true;
                }
            }

            if (new TimeSpan(settingHour + 12,0,0).Hours == nowHour)
            {
                _settingProvider.Setting.AlreadyExecute = false;
            }
           
        }

        async void ExecuteStartProccess()
        {    
            await _seleniumHelper.Start(User);
        }
        private void InitializeView()
        {
          loginView =  _unityContainer.Resolve<LoginView>();
          settingView =  _unityContainer.Resolve<SettingView>();
        }

     
    }
}
