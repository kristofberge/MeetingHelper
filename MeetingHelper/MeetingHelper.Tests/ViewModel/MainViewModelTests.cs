using System;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Windows.Media;
using MeetingHelper.Helpers;
using System.ComponentModel;
using MeetingHelper.Helpers.TimeKeerpers;

namespace MeetingHelper.ViewModel.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        [Test]
        public void SetDefaulImage_WhenNoneIsChosen()
        {
            Mock<ImageHelper> imageHelperMock = new Mock<ImageHelper>() { CallBase = true };
            imageHelperMock.Setup(x => x.GetDefaultImageSource()).Returns(new Object() as ImageSource);

            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.ImageHelper = imageHelperMock.Object as ImageHelper;

            var ImageSourceProperty = mainViewModel.ChosenImage;
            imageHelperMock.Verify(x => x.GetDefaultImageSource(), Times.Exactly(1));
        }

        [Test]
        public void DoNotSetChosenImage_WhenUserCancelsDialog()
        {
            Mock<ImageHelper> imageHelperMock = new Mock<ImageHelper>();
            imageHelperMock.Protected().Setup<bool>("UserMadeCorrectChoice").Returns(false);
            imageHelperMock.SetupSet(x => x.ChosenImage = It.IsAny<ImageSource>()).Verifiable();

            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.ImageHelper = imageHelperMock.Object as ImageHelper;

            var imageHelper = imageHelperMock.Object as ImageHelper;
            imageHelper.RefreshChosenImageFromUserChoice();

            imageHelperMock.VerifySet(x => x.ChosenImage = It.IsAny<ImageSource>(), Times.Never());
        }
    }
}
