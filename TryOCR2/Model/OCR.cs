using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace TryOCR2.Model
{
    public class OCR
    {
        /// <summary>
        /// RunOcrで利用するSoftwareBitmapを作成する
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<SoftwareBitmap> ConvertSoftwareBitmap(Image image)
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

        /// <summary>
        /// 画像から文字を認識する
        /// </summary>
        /// <param name="software_bitmap"></param>
        /// <returns></returns>
        public async Task<OcrResult> Run(SoftwareBitmap software_bitmap)
        {
            //OCRを実行する
            OcrEngine engine = OcrEngine.TryCreateFromLanguage(new Windows.Globalization.Language("ja-JP"));
            var result = await engine.RecognizeAsync(software_bitmap);
            return result;
        }
    }
}
