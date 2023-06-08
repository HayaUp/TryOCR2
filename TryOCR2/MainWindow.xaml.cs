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
