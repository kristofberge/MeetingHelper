using GalaSoft.MvvmLight;
using MeetingHelper.Command;
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

        
        public ImageHelper ImageHelper;
        public RelayCommand ImageClicked;

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
            ImageHelper = new ImageHelper();
            ImageClicked = new RelayCommand(f => { ImageClickedCmd(); }, f => true);
        }

        private ImageSource _chosenImageSource;
        public ImageSource ChosenImageSource
        {
            get { return ImageHelper.ChosenImage; }
            set { _chosenImageSource = value; }
        }

        public void ImageClickedCmd()
        {
            
        }

        private bool? GetReponseFromUser()
        {
            

            return openImageDialog.ShowDialog();
        }
        

    }
}