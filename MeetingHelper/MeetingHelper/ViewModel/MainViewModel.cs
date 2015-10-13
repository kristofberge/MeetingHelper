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
using PropertyChanged;

namespace MeetingHelper.ViewModel
{
    [ImplementPropertyChanged]
    public class MainViewModel : ViewModelBase
    {
        public IImageHelper ImageHelper { get; set; }
        public ITimeHelper Timer { get; set; }
        public RelayCommand ImageClicked { get; private set; }
        public RelayCommand TimerClicked { get; private set; }

        public MainViewModel()
        {
            ImageHelper = MyFactory.GetImageHelper(); ;
            Timer = MyFactory.GetTimeHelper(Constants.TimeHelperType.STOPWATCH);
            ImageClicked = new RelayCommand(f => { ImageClickedCommand(); }, f => true);
            TimerClicked = new RelayCommand(f => { TimerClickedCommand(); }, f => true);
        }

        private ImageSource _chosenImage;
        public virtual ImageSource ChosenImage
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
                    _chosenImage = value;
            }
        }

        protected virtual void ImageClickedCommand()
        {
            ImageHelper.RefreshChosenImageFromUserChoice();
            ChosenImage = ImageHelper.ChosenImage;
        }

        protected virtual void TimerClickedCommand()
        {
            Timer.TimerClicked();
        }
    }
}