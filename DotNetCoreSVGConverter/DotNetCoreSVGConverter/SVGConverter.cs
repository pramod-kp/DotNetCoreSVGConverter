using ImageMagick;
using System;
using System.IO;

namespace DotNetCoreSVGConverter
{
    public static class SVGConverter
    {
        public static Stream Base64ToImageStream(string base64String, string logoType)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                using (var msOut = new MemoryStream())
                {
                    MagickReadSettings readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Svg,
                        Width = 60,
                        Height = 40,
                        BackgroundColor = MagickColors.Transparent
                    };

                    using (MagickImage image = new MagickImage(imageBytes, readSettings))
                    {
                        image.Format = MagickFormat.Png; // Specify the format you need
                        image.Write(msOut);
                        byte[] data = image.ToByteArray();
                        var pngBase64 = Convert.ToBase64String(data);
                        byte[] imgByte = Convert.FromBase64String(pngBase64);
                        var pngStream = new MemoryStream(imgByte, 0, imgByte.Length);
                        return pngStream;
                    }
                }
            }            
        }
    }
}
