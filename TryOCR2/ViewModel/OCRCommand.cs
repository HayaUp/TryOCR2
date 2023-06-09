using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

namespace TryOCR2.ViewModel
{
    public class OCRCommand : ICommand
    {
        private MainWindowViewModel MainWindowViewModel;

        public event EventHandler CanExecuteChanged;

        public OCRCommand(MainWindowViewModel view_model)
        {
            MainWindowViewModel = view_model;
        }

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter)
        {
            var ocr = new Model.OCR();
            var image = new Image()
            {
                Source = MainWindowViewModel.TargetImageSource
            };
            var software_bitmap = await ocr.ConvertSoftwareBitmap(image);
            var result = await ocr.Run(software_bitmap);
            var result_text = ocr.ResultMultilineText(result);

            MainWindowViewModel.OCRResultText = result_text;
        }
    }
}
