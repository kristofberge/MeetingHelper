using System;
using System.Windows.Media;
namespace MeetingHelper.ViewModel
{
    public interface IMainViewModel
    {
        ImageSource ChosenImageSource { get; set; }
        ImageSource GetDefaultImageSource();
        void ImageClicked(object sender, EventArgs args);
    }
}
