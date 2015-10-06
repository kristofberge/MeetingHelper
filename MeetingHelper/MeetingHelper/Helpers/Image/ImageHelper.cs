using MeetingHelper.Exceptions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeetingHelper.Helpers.Image
{
    public class ImageHelper : IImageHelper
    {
        private IDialogHandler _dialogHandler;

        public ImageHelper(IDialogHandler handler)
        {
            _dialogHandler = handler;
        }

        private ImageSource _chosenImage;
        public virtual ImageSource ChosenImage
        {
            get
            {
                if (_chosenImage == null)
                    _chosenImage = GetDefaultImage();

                return _chosenImage;
            }
            set { _chosenImage = value; }
        }

        protected virtual ImageSource GetDefaultImage()
        {
            return new BitmapImage(new Uri("pack://Application:,,,/Images/Default.jpg"));
        }

        public void RefreshChosenImageFromUserChoice()
        {
            try
            {
                ChosenImage = new BitmapImage(_dialogHandler.GetImageUriFromUser());
            }
            catch (NotSupportedException)
            {
                throw new NonImageFileChosenException();
            }
        }
    }
}
