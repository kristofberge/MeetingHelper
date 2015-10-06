using System;
using System.Windows.Media;
namespace MeetingHelper.Helpers.Image
{
    public interface IImageHelper
    {
        ImageSource ChosenImage { get; set; }
        void RefreshChosenImageFromUserChoice();
    }
}
