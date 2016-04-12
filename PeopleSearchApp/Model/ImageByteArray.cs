using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PeopleSearchApp.Model
{
    class ImageByteArray: IValueConverter
    {
        public byte[] ImagePathToByteArray(string imagePath)
        {
            Image coderPhoto = Image.FromFile(imagePath);

            MemoryStream ms = new MemoryStream();
            coderPhoto.Save(ms, coderPhoto.RawFormat);
            coderPhoto.Dispose();

            return ms.ToArray();
        }

        public Image ByteArrayToImage(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream();
            Image returnImage = Image.FromStream(ms);

            return returnImage;
        }

        object IValueConverter.Convert(object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture)
        {
            if (value != null && value is byte[])
            {
                byte[] bytes = value as byte[];

                MemoryStream stream = new MemoryStream(bytes);

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();

                return image;
            }

            return null;
        }

        object IValueConverter.ConvertBack(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
