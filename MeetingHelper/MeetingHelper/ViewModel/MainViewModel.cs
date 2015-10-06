using GalaSoft.MvvmLight;
using MeetingHelper.Helpers;
using MeetingHelper.Helpers.Image;
using MeetingHelper.Helpers.Time;
using MeetingHelper.Shared;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
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

        
        public IImageHelper ImageHelper;
        public ITimeHelper Timer;
        public RelayCommand ImageClicked { get; private set; }
        public RelayCommand TimerClicked { get; private set; }

        public MainViewModel()
        {
            ImageHelper = MyFactory.GetImageHelper(); ;
            Timer = MyFactory.GetTimeHelper(Common.TimeHelperType.STOPWATCH);
            ImageClicked = new RelayCommand(f => { ImageClickedCmd(); }, f => true);
            TimerClicked = new RelayCommand(f => { TimerClickedCmd(); }, f => true);
        }

        private ImageSource _chosenImage;
        public ImageSource ChosenImage
        {
            get
            {
                if (_chosenImage == null)
                    _chosenImage = ImageHelper.ChosenImage;
                return _chosenImage;
            }
            set
            {
                if (_chosenImage != value)
                {
                    _chosenImage = value;
                    RaisePropertyChanged();
                }
            }
        }

        protected virtual void ImageClickedCmd()
        {
            ImageHelper.RefreshChosenImageFromUserChoice();
            ChosenImage = ImageHelper.ChosenImage;
        }

        protected virtual void TimerClickedCmd()
        {
            Timer.TimerClicked();
        }
    }
}