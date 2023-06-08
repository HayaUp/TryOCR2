﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TryOCR2.ViewModel
{
    public class ReadImageButtonCommand : ICommand
    {
        private MainWindowViewModel MainWindowViewModel;

        public event EventHandler CanExecuteChanged;

        public ReadImageButtonCommand(MainWindowViewModel view_model)
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
