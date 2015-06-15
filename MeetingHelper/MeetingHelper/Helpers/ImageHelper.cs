using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
        public ImageSource ChosenImage
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
            return new ImageSourceConverter().ConvertFromString(@"..\Images\Default.jpg") as ImageSource;
        }

        public void RefreshChosenImageFromUserChoice()
        {
            ChosenImage = GetUserChoiceFromDialog();
        }

        protected ImageSource GetUserChoiceFromDialog()
        {
            ImageSource newImage = this.ChosenImage;

            if (UserMadeCorrectChoice() == true)
            {
                try
                {
                    newImage = new ImageSourceConverter().ConvertFromString(_chooseImageDialog.FileName) as ImageSource;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Incorrect filetype.\nPlease Select an image file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return newImage;
        }

        protected virtual bool? UserMadeCorrectChoice()
        {
            SetupChooseImageDialog();
            return _chooseImageDialog.ShowDialog();
        }

        private void SetupChooseImageDialog()
        {
            _chooseImageDialog = new OpenFileDialog();
            _chooseImageDialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.gif, *.png)|*.jpg;*.jpeg;*.bmp;*.gif;*.png|"
                + "All files (*.*)|*.*";
        }
    }
}
