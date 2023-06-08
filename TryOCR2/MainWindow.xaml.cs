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
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
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

        private async Task<SoftwareBitmap> ConvertSoftwareBitmap(Image image)
        {
            SoftwareBitmap sbitmap = null;

            using(MemoryStream stream = new MemoryStream())
            {
                //BmpBitmapEncoderに画像を書きこむ
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add((System.Windows.Media.Imaging.BitmapFrame)image.Source);
                encoder.Save(stream);

                //メモリストリームを変換
                var irstream = WindowsRuntimeStreamExtensions.AsRandomAccessStream(stream);

                //画像データをSoftwareBitmapに変換
                var decorder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(irstream);
                sbitmap = await decorder.GetSoftwareBitmapAsync();
            }

            return sbitmap;
        }

        private async Task<OcrResult> RunOcr(SoftwareBitmap sbitmap)
        {
            //OCRを実行する
            OcrEngine engine = OcrEngine.TryCreateFromLanguage(new Windows.Globalization.Language("ja-JP"));
            var result = await engine.RecognizeAsync(sbitmap);
            return result;
        }

        private async void CharacterRecognitionButton_Click(object sender, RoutedEventArgs e)
        {
            var software_bitmap = await ConvertSoftwareBitmap(TargetImage);
            var result = await RunOcr(software_bitmap);

            CharacterRecognitionResultTextBlock.Text = result.Text;
        }
    }
}
