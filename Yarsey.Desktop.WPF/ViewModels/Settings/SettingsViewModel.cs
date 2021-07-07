using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.Services;
using Syncfusion;
using Syncfusion.SfSkinManager;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class SettingsViewModel:ViewModelBase
    {
        private readonly MainWindow _mainWindow;
        private readonly IConfiguration _configuration;
        private readonly GeneralModalNavigationService _generalModalNavigationService;

        private bool _lightMode;

        public bool LightMode
        {
            get { return _lightMode; }
            set { SetProperty(ref _lightMode, value); SetTheme(); }
        }


        public SettingsViewModel(MainWindow mainWindow,IConfiguration configuration,GeneralModalNavigationService generalModalNavigationService)
        {
            this._mainWindow = mainWindow;
            this._configuration = configuration;
            this._generalModalNavigationService = generalModalNavigationService;
            InitConfigure();
        
        }

        public void InitConfigure()
        {
            var d = _configuration.GetSection("Theme").GetSection("LightMode").Value;
            bool result;
            if(bool.TryParse(d, out result))
            {
                LightMode = result;
            }
        }
        public void SetTheme()
        {
            if (LightMode == true)
            {
                _mainWindow.SetLightMode();
                
            }
            else
            {
                _mainWindow.SetDarkMode();
            }
            
            _configuration.GetSection("Theme").GetSection("LightMode").Value = LightMode.ToString().ToLower();
            var finaVal = _configuration.GetSection("Theme").GetSection("LightMode").Value;
        
        }

        #region Dispose
        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
