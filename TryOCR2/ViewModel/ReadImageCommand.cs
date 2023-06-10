using System;
using System.Windows.Input;

namespace TryOCR2.ViewModel
{
    public class ReadImageCommand : ICommand
    {
        private MainWindowViewModel MainWindowViewModel;

        public event EventHandler CanExecuteChanged;

        public ReadImageCommand(MainWindowViewModel view_model)
        {
            MainWindowViewModel = view_model;
        }

        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// ファイルダイアログで画像を選択する
        /// 選択した画像のファイルパスと画像を画面に表示する
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            var file_dialog = new Microsoft.Win32.OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultExt = ".png",
                Filter = "PNG (.png)|*.png",
            };

            if(file_dialog.ShowDialog() == true)
            {
                MainWindowViewModel.ImageFilePath = file_dialog.FileName;
                MainWindowViewModel.TargetImageSource = System.Windows.Media.Imaging.BitmapFrame.Create(new Uri(file_dialog.FileName));
            }
        }
    }
}
