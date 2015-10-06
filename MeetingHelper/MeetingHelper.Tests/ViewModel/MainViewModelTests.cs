using System;


using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Windows.Media;
using MeetingHelper.Helpers;
using System.ComponentModel;
using MeetingHelper.Helpers.Time;
using MeetingHelper.Helpers.Image;

namespace MeetingHelper.ViewModel.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
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
