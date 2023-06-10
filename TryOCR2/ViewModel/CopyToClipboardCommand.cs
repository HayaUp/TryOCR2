using System;
using System.Windows;
using System.Windows.Input;

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
