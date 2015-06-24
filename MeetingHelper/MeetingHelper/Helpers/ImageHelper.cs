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
    public class ImageHelper
    {
        public ImageSourceConverter Converter;

        private OpenFileDialog _chooseImageDialog;

        public ImageHelper()
        {
            Converter = new ImageSourceConverter();
        }

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
            if (UserMadeCorrectChoice() == true)
            {
                try
                {
                    ChosenImage = new BitmapImage(new Uri(_chooseImageDialog.FileName));
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show("Incorrect filetype.\nPlease Select an image file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        protected virtual bool UserMadeCorrectChoice()
        {
            SetupChooseImageDialog();
            return _chooseImageDialog.ShowDialog() == true;
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
