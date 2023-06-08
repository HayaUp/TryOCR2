using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using System.IO;

namespace TryOCR2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ファイルダイアログで画像を選択する
        /// 選択した画像のファイルパスと画像を画面に表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var file_dialog = new Microsoft.Win32.OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultExt = ".png",
                Filter = "PNG (.png)|*.png",
            };

            if(file_dialog.ShowDialog() == true)
            {
                var image_file_path = file_dialog.FileName;
                ImageFilePathTextBlock.Text = image_file_path;

                TargetImage.Source = System.Windows.Media.Imaging.BitmapFrame.Create(new Uri(image_file_path));
            }
        }

        private async void CharacterRecognitionButton_Click(object sender, RoutedEventArgs e)
        {
            var ocr = new Model.OCR();
            var software_bitmap = await ocr.ConvertSoftwareBitmap(TargetImage);
            var result = await ocr.Run(software_bitmap);
            var result_text = ocr.ResultMultilineText(result);

            CharacterRecognitionResultTextBlock.Text = result_text;
        }

        private void CopyToClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(CharacterRecognitionResultTextBlock.Text);
        }
    }
}
