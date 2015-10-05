using System;
using System.Web;

using NUnit.Framework;
using MeetingHelper.Helpers;
using Moq;
using Moq.Protected;

using MeetingHelper.Tests.Testables;
using MeetingHelper.ViewModel;
using System.Windows.Media;

namespace MeetingHelper.Tests.Helpers
{
    [TestFixture]
    public class ImageHelperTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            if (!UriParser.IsKnownScheme("pack"))
                new System.Windows.Application();
        }

        [Test]
        public void UserCancelsDialog_ImageNotRefreshed()
        {
            //Arrange
            Mock<ImageHelper> imageHelper = new Mock<ImageHelper>();
            imageHelper.Protected().Setup<bool?>("UserChoosesImage").Returns(false);
            imageHelper.SetupSet(ih => ih.ChosenImage = It.IsAny<ImageSource>()).Verifiable();
            var imageHelperObj = imageHelper.Object as ImageHelper;

            //Act
            imageHelperObj.RefreshChosenImageFromUserChoice();

            //Assert
            imageHelper.VerifySet(ih => ih.ChosenImage = It.IsAny<ImageSource>(), Times.Never());
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void UserChoosesNonImageFile_NotSupportedExceptionThrown()
        {
            //Arrange
            Mock<ImageHelper> imageHelper = new Mock<ImageHelper>();
            Uri nonImageUri = new Uri("pack://application:,,,/Files/NotAnImage.txt");
            imageHelper.Protected().Setup<Uri>("GetUriFromDialog").Returns(nonImageUri);

            //Act
            imageHelper.Object.RefreshChosenImageFromUserChoice();
        }
    }
}
