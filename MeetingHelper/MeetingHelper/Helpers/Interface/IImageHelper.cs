using System;
using System.Windows.Media;
namespace MeetingHelper.Helpers
{
    public interface IImageHelper
    {
        ImageSource ChosenImage { get; set; }
        ImageSource GetDefaultImageSource();
        void RefreshChosenImageFromUserChoice();
    }
}
