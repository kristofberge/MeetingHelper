using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MeetingHelper.Helpers
{
    public class ImageSourceHelper
    {
        public ImageSourceConverter Converter;

        public ImageSourceHelper()
        {
            Converter = new ImageSourceConverter();
        }

        private ImageSource _chosenImageSource;
        public ImageSource ChosenImageSource
        {
            get
            {
                if (_chosenImageSource == null)
                {
                    _chosenImageSource = GetDefaultImageSource();
                }
                return _chosenImageSource;
            }
            set { _chosenImageSource = value; }
        }

        public virtual ImageSource GetDefaultImageSource()
        {
            return new ImageSourceConverter().ConvertFromString(@"..\Images\Default.jpg") as ImageSource;
        }

        public void RefreshImageSourceFromUserChoice()
        {
            this.ChosenImageSource = GetNewImageFromDialog();
        }

        protected virtual ImageSource GetNewImageFromDialog()
        {
            ImageSource newImage = null;
            return newImage;
        }
    }
}
