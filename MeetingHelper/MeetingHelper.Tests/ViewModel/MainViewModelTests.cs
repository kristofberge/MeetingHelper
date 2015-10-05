using System;


using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Windows.Media;
using MeetingHelper.Helpers;
using System.ComponentModel;
using MeetingHelper.Helpers.Time;

namespace MeetingHelper.ViewModel.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        [Test]
        public void SetDefaulImage_WhenNoneIsChosen()
        {
            //Arrange
            Mock<ImageHelper> imageHelper = new Mock<ImageHelper>() { CallBase = true };
            imageHelper.Setup(x => x.GetDefaultImageSource()).Returns(new Object() as ImageSource);

            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.ImageHelper = imageHelper.Object as ImageHelper;

            //Act
            var ImageSourceProperty = mainViewModel.ChosenImage;

            //Assert
            imageHelper.Verify(x => x.GetDefaultImageSource(), Times.Exactly(1));
        }

        [Test]
        public void DoNotSetChosenImage_WhenUserCancelsDialog()
        {
            //Arrange
            Mock<ImageHelper> imageHelper = new Mock<ImageHelper>();
            imageHelper.Protected().Setup<bool>("ShowDialog").Returns(false);
            imageHelper.SetupSet(x => x.ChosenImage = It.IsAny<ImageSource>()).Verifiable();

            //Act
            imageHelper.Object.RefreshChosenImageFromUserChoice();

            //Assert
            imageHelper.VerifySet(x => x.ChosenImage = It.IsAny<ImageSource>(), Times.Never());
        }
    }
}
