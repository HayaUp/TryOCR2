using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace TryOCR2.ViewModel
{
    public class CopyToClipboardCommand : ICommand
    {
        private MainWindowViewModel MainWindowViewModel;

        public event EventHandler CanExecuteChanged;

        public CopyToClipboardCommand(MainWindowViewModel view_model)
        {
            MainWindowViewModel = view_model;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            Clipboard.SetText(MainWindowViewModel.OCRResultText);
        }
    }
}
