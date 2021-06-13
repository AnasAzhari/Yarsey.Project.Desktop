using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Yarsey.Desktop.WPF.Stores;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class HomeViewModel:ViewModelBase
    {
        private readonly BusinessStore _businessstore;

        public string ContentHome { get; set; }


        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }


        private string _bizname;

        public string BizName
        {
            get { return _bizname; }
            set { SetProperty(ref _bizname, value); }
        }

        public HomeViewModel(BusinessStore businessstore)
        {
            this._businessstore = businessstore;
            this._businessstore.CurrentBusinessChanged += OnBusinessChanged;
        }

        public void OnBusinessChanged()
        {
            BizName = this._businessstore.CurrentBusiness.BusinessName;
            byte[] imgBlob = this._businessstore.CurrentBusiness.Image;
            Image = Helper.Helper.BlobToImage(imgBlob);

        }
    }
}
