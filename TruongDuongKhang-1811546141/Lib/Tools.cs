using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TruongDuongKhang_1811546141.Lib
{
    // tham khảo từ thầy Nguyễn Mai Huy
    class Tools
    {
        /// <summary>
        /// Convert Image to String [Use to send image to Database varchar(max)]
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string imageToString(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //--- Step 1: Save image to memory stream and convert it to byte array ----
                img.Save(ms, getTypeOfImage(img));
                byte[] imgBytes = ms.ToArray();
                //--- Step 2: Convert from byte array to Base64String using Convert class -
                return Convert.ToBase64String(imgBytes);
            }
        }
        /// <summary>
        /// Convert string to Image [Get from Database and create image on C sharp]
        /// </summary>
        /// <param name="imgString"></param>
        /// <returns></returns>
        public static Image stringToImage(string imgString)
        {
            //--- Step 1: Convert from string to Byte array and store in memory stream ----
            Image result = Properties.Resources.noImage;
            try
            {
                byte[] imgBytes = Convert.FromBase64String(imgString);
                MemoryStream ms = new MemoryStream(imgBytes, 0, imgBytes.Length);
                //--- Step 2: Convert from Byte array to Image and resturn to the caller ------
                ms.Write(imgBytes, 0, imgBytes.Length);
                result = Image.FromStream(ms, true);
            }
            catch
            {
                //-- Do nothings
            }
            return result;
        }
        /// <summary>
        /// Get type of Image 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static ImageFormat getTypeOfImage(Image img)
        {
            ImageFormat result = ImageFormat.Emf;
            if (img.RawFormat.Equals(ImageFormat.Png))
                result = ImageFormat.Png;
            else if (img.RawFormat.Equals(ImageFormat.Jpeg))
                result = ImageFormat.Jpeg;
            else if (img.RawFormat.Equals(ImageFormat.Bmp))
                result = ImageFormat.Bmp;
            else if (img.RawFormat.Equals(ImageFormat.Icon))
                result = ImageFormat.Icon;
            else if (img.RawFormat.Equals(ImageFormat.Gif))
                result = ImageFormat.Gif;
            else if (img.RawFormat.Equals(ImageFormat.Tiff))
                result = ImageFormat.Tiff;
            else if (img.RawFormat.Equals(ImageFormat.Wmf))
                result = ImageFormat.Wmf;
            else
                result = ImageFormat.Exif;
            return result;
        }
    }
}
