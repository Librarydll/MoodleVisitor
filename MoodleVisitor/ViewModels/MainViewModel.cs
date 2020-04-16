using MoodleVisitor.Models;
using MoodleVisitor.Models.Infrastructure;
using MoodleVisitor.Services;
using MoodleVisitor.UIControls.Infrastructure;
using MoodleVisitor.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.ComponentModel;
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
        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user , value); }
        }
        private Visibility _windowVisibility = Visibility.Visible;

        public Visibility WindowVisibility
        {
            get { return _windowVisibility; }
            set { SetProperty(ref _windowVisibility, value); }
        }

        private bool _showInTaskbar = true;

        public bool ShowInTaskbar
        {
            get { return _showInTaskbar; }
            set { SetProperty(ref _showInTaskbar, value); }
        }
      
        private readonly SeleniumHelper _seleniumHelper;
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;
        private readonly ISettingProvider _settingProvider;
        private LoginView loginView;
        private SettingView settingView;
        private DispatcherTimer timer;

        private DelegateCommand _startProccessCommand;
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
            ShowInTaskbar = true;
        }

        private void OnWindowsClosing(CancelEventArgs e)
        {
            if (!_closeWindow)
            {
                e.Cancel = true;
                WindowVisibility = Visibility.Hidden;
                ShowInTaskbar = false;
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
           // InitializeTimer();
            User = _settingProvider.Setting.User;
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
            iconService = new TryIconService("main.ico");
            iconService.SetDoubleClickCommand(OpenWindowCommand);
            iconService.OnMenuItemClick += IconService_OnMenuItemClick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int settingHour= _settingProvider.Setting.Time.Hours;
            int nowHour = DateTime.Now.TimeOfDay.Hours;

            if (!_settingProvider.Setting.AlreadyExecute)
            {
                if (settingHour == nowHour||settingHour+1==nowHour)
                {
                    ExecuteStartProccess();
                    _settingProvider.SetAsExecuted();
                }
            }

            if (new TimeSpan(settingHour + 12,0,0).Hours == nowHour)
            {
                _settingProvider.UnsetAsExecuted();

            }

        }

        async void ExecuteStartProccess()
        {
            _settingProvider.Setting.User = User;
            _settingProvider.SaveSettings(_settingProvider.Setting);
            await _seleniumHelper.Start(User);
         
        }
        private void InitializeView()
        {
          loginView =  _unityContainer.Resolve<LoginView>();
          settingView =  _unityContainer.Resolve<SettingView>();
        }

     
    }
}
