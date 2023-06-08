using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace TryOCR2.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ReadImageButtonCommand ReadImageButtonCommand { get; }

        public MainWindowViewModel()
        {
            ReadImageButtonCommand = new ReadImageButtonCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string property_name = null)
        {
            PropertyChanged?.Invoke(this, new(property_name));
        }

        private string _ImageFilePath;
        public string ImageFilePath
        {
            get => _ImageFilePath;
            set
            {
                if(value != _ImageFilePath)
                {
                    _ImageFilePath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ImageSource _TargetImageSource;
        public ImageSource TargetImageSource
        {
            get => _TargetImageSource;
            set
            {
                if(value != _TargetImageSource)
                {
                    _TargetImageSource = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
