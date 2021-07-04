using ModernWpf;
using SkiaSharp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace STNMI
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            ThemeManager.SetRequestedTheme(this, (ElementTheme)Parametres.Default.Theme);
            Version.Content = Version.Content.ToString().Replace("(Version)", AppData.GetAppVersion());
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }


        private void TintedImageView_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs args)
        {

            var image = SKImage.FromEncodedData(AppDomain.CurrentDomain.BaseDirectory + "STNMIPNG.png");
            SKBitmap bitmap = SKBitmap.FromImage(image);
            SKBitmap bitmapMask = SKBitmap.FromImage(image); 

            var blueBananaBitmap = new SKBitmap(bitmapMask.Width, bitmapMask.Height);
            using (SKCanvas canva = new SKCanvas(blueBananaBitmap))
            {
                SKPaint pai = new SKPaint();
                pai.IsAntialias = true;
                canva.Clear();
                canva.DrawBitmap(bitmapMask, new SKPoint(0, 0), pai);

                using (SKPaint paint = new SKPaint())
                {
                    paint.IsAntialias = false;
                    paint.Color = SKColor.Parse(ThemeManager.Current.ActualAccentColor.ToString());
                    paint.BlendMode = SKBlendMode.SrcIn;
                    canva.DrawPaint(paint);
                }
            }

            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            SKPaint pain = new SKPaint();
            pain.IsAntialias = true;
            canvas.DrawBitmap(bitmap, info.Rect, pain);

            using (SKPaint paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.BlendMode = SKBlendMode.Hue;
                canvas.DrawBitmap(blueBananaBitmap,
                                  info.Rect,
                                  paint: paint);
            }
        }

    }
}
