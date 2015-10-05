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

namespace MeetingHelper.Helpers
{
    public class ImageHelper : IImageHelper
    {
        protected bool? userCancelledDialog;

        private OpenFileDialog _chooseImageDialog;

        private ImageSource _chosenImage;
        public virtual ImageSource ChosenImage
        {
            get
            {
                if (_chosenImage == null)
                {
                    _chosenImage = GetDefaultImageSource();
                }
                return _chosenImage;
            }
            set { _chosenImage = value; }
        }

        public virtual ImageSource GetDefaultImageSource()
        {
            return new BitmapImage(new Uri("pack://Application:,,,/Images/Default.jpg"));
        }

        public void RefreshChosenImageFromUserChoice()
        {
            if (UserChoosesImage() == false)
            {
                try
                {
                    ChosenImage = new BitmapImage(GetUriFromDialog());
                }
                catch (NotSupportedException)
                {
                    throw;
                }
            }
        }

        protected virtual bool? UserChoosesImage()
        {
            SetupChooseImageDialog();
            return ShowDialog();
        }

        protected virtual Uri GetUriFromDialog()
        {
            return new Uri(_chooseImageDialog.FileName);
        }

        protected virtual bool? ShowDialog()
        {
            return !_chooseImageDialog.ShowDialog();
        }

        private void SetupChooseImageDialog()
        {
            _chooseImageDialog = new OpenFileDialog();
            _chooseImageDialog.Filter = 
                "Image files (*.jpg, *.jpeg, *.bmp, *.gif, *.png)|*.jpg;*.jpeg;*.bmp;*.gif;*.png|"
                + "All files (*.*)|*.*";
        }
    }
}
