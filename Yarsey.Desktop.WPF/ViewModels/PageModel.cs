using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class PageModel : ViewModelBase
    {
        private string _title;
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

    }
}
