﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;
using Yarsey.Domain.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class BusinessSelectionPageModel: PageModel
    {

        public Action<Business> ChangeMainWindow;

        private ObservableCollection<Business> _businesses;

        public ObservableCollection<Business> Businesses { get { return _businesses; } set { SetProperty(ref _businesses, value); } }

        private Business _selectedBusiness;

        public Business SelectedBusiness { get { return _selectedBusiness; } 
            set { SetProperty(ref _selectedBusiness, value); if (value is not null && ChangeMainWindow is not null) ChangeMainWindow(value); } }


        private readonly IBusinessService _businessService;
        public BusinessSelectionPageModel(IBusinessService businessService)
        {
            this._businessService = businessService;
            this.GetBusiness();
        }

        void GetBusiness()
        {
            this.Businesses = new ObservableCollection<Business>(this._businessService.GetAll().Result.ToList());
        }
    }
}
