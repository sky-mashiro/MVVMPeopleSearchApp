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
    //Since iamge is stored as byte array in database, so we need a converter
    class ImageByteArray: IValueConverter
    {
        //Convert iamge path to byte array of image
        public byte[] ImagePathToByteArray(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return null;
            }
            Image coderPhoto = Image.FromFile(imagePath);

            MemoryStream ms = new MemoryStream();
            coderPhoto.Save(ms, coderPhoto.RawFormat);
            coderPhoto.Dispose();

            return ms.ToArray();
        }

        //Covert byte array to image
        public Image ByteArrayToImage(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream();
            Image returnImage = Image.FromStream(ms);

            return returnImage;
        }

        //Used as converter in xaml file to show the image
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

        //Not implemented
        object IValueConverter.ConvertBack(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
