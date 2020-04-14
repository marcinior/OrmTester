using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OrmTesterLib.IOService
{
    public class ChartService
    {
        public static void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var scale = visual.DesiredSize.Height / visual.DesiredSize.Width;
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)(visual.ActualWidth * scale), 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) 
            {
                encoder.Save(stream);
            }
        }
    }
}
