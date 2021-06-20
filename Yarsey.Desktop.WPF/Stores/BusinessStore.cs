using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.Stores
{
    public class BusinessStore
    {
        public BusinessStore(BusinessDataService businessDataService)
        {
            this._businessDataService = businessDataService;
        }
        public event Action CurrentBusinessChanged;

        private Business _currentBusiness;
        private readonly BusinessDataService _businessDataService;

        public Business CurrentBusiness
        {
            get => _currentBusiness;
            set
            {
                _currentBusiness = value;
                OnCurrentBusinessChanged();
            }
        }

        public async void RefreshBusiness()
        {
            //OnCurrentBusinessChanged();
            var bizId = this.CurrentBusiness.Id;
            var biz = _businessDataService.Get(bizId);
            this.CurrentBusiness =await biz;

        }

        private void OnCurrentBusinessChanged()
        {
            var delegates=CurrentBusinessChanged?.GetInvocationList();

            CurrentBusinessChanged?.Invoke();
        }
        public void ClearEvents()
        {
            CurrentBusinessChanged=null;
        }
    }
}
