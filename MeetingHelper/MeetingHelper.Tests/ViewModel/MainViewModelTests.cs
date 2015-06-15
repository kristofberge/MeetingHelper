using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Media;
using MeetingHelper.Helpers;
using System.ComponentModel;

namespace MeetingHelper.ViewModel.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void SetDefaulImage_WhenNoneIsChosen()
        {
            Mock<ImageHelper> imageHelperMock = new Mock<ImageHelper>();
            imageHelperMock.Setup(x => x.GetDefaultImageSource()).Returns(new Object() as ImageSource);

            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.ImageHelper = imageHelperMock.Object as ImageHelper;

            var ImageSourceProperty = mainViewModel.ChosenImageSource;
            imageHelperMock.Verify(x => x.GetDefaultImageSource(), Times.Exactly(1));
        }

        [TestMethod]
        public void DoNotSetChosenImage_WhenUserCancelsDialog()
        {
            Mock<ImageHelper> imageHelperMock = new Mock<ImageHelper>();
            imageHelperMock.Setup(x => x.UserMadeCorrectChoice()).Returns(false);
            imageHelperMock.SetupSet(x => x.ChosenImage = It.IsAny<ImageSource>()).Verifiable();

            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.ImageHelper = imageHelperMock.Object as ImageHelper;

            var imageHelper = imageHelperMock.Object as ImageHelper;
            imageHelper.RefreshChosenImageFromUserChoice();

            imageHelperMock.VerifySet(x => x.ChosenImage = It.IsAny<ImageSource>(), Times.Never());
        }
    }
}
