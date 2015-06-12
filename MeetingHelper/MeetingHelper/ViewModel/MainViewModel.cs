using GalaSoft.MvvmLight;
using MeetingHelper.Helpers;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MeetingHelper.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 

        OpenFileDialog openImageDialog;
        public ImageSourceHelper ImageHelper;

        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            ImageHelper = new ImageSourceHelper();
        }


        private ImageSource _chosenImageSource;
        public ImageSource ChosenImageSource
        {
            get { return ImageHelper.ChosenImageSource; }
            set { _chosenImageSource = value; }
        }

        

        public void ImageClicked(object sender, EventArgs args)
        {
            if (GetReponseFromUser() == true)
            {
                try
                {
                    ChosenImageSource = new ImageSourceConverter().ConvertFromString(openImageDialog.FileName) as ImageSource;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Incorrect filetype.\nPlease Select an image file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool? GetReponseFromUser()
        {
            openImageDialog = new OpenFileDialog();
            openImageDialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.gif, *.png)|*.jpg;*.jpeg;*.bmp;*.gif;*.png|"
                + "All files (*.*)|*.*";

            return openImageDialog.ShowDialog();
        }
        

    }
}