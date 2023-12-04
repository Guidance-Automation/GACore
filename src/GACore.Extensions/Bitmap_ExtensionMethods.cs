using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Versioning;

namespace GACore.Extensions;

public static class BitmapExtensions
{
    [SupportedOSPlatform("windows")]
    /// <summary>
    /// Converts a bitmap to a generic sequence of bytes.
    /// </summary>
    /// <returns></returns>
    public static Stream ToStream(this Bitmap bitmap, ImageFormat imageFormat)
    {
        ArgumentNullException.ThrowIfNull(bitmap);
        ArgumentNullException.ThrowIfNull(imageFormat);

        MemoryStream stream = new();
        bitmap.Save(stream, imageFormat);
        stream.Position = 0;
        return stream;
    }
}