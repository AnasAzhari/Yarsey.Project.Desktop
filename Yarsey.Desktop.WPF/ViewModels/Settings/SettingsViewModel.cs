﻿using Microsoft.Extensions.Configuration;
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
        private readonly SettingsConfiguration _settingsConfiguration;
        private readonly GeneralModalNavigationService _generalModalNavigationService;

     

        public bool LightMode
        {
            get { return _settingsConfiguration.CurrentSettings.LightTheme; }
            set { 

                _settingsConfiguration.CurrentSettings.LightTheme = value; SetTheme(); 
            }
        }


        public string BackupLoc
        {
            get { return _settingsConfiguration.CurrentSettings.BackupLocation; }
            set
            {
                _settingsConfiguration.CurrentSettings.BackupLocation = value; SetBackupLocation();
            }
        }


        public SettingsViewModel(MainWindow mainWindow,SettingsConfiguration settingsConfiguration,GeneralModalNavigationService generalModalNavigationService)
        {
            this._mainWindow = mainWindow;
            this._settingsConfiguration = settingsConfiguration;
            this._generalModalNavigationService = generalModalNavigationService;
            InitConfigure();
        
        }

        public void InitConfigure()
        {
            //var d = _configuration.GetSection("Theme").GetSection("LightMode").Value;
            //bool result;
            //if(bool.TryParse(d, out result))
            //{
            //    LightMode = result;
            //}
            

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
            _settingsConfiguration.WriteSettingsToJson();

        }
        public void SetBackupLocation()
        {
            _settingsConfiguration.WriteSettingsToJson();
        }

        #region Dispose
        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
