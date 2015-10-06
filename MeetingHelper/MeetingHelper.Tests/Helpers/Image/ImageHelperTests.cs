using System;
using System.Web;

using NUnit.Framework;
using MeetingHelper.Helpers;
using Moq;
using Moq.Protected;

using MeetingHelper.Tests.Testables;
using MeetingHelper.ViewModel;
using System.Windows.Media;
using MeetingHelper.Helpers.Image;
using MeetingHelper.Exceptions;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;

namespace MeetingHelper.Tests.Helpers.Image
{
    [TestFixture]
    public class ImageHelperTests
    {
        private Mock<ImageHelper> _imageHelper;
        private Mock<IDialogHandler> _dialogHandler;
        private string _directoryPath;

        [SetUp]
        public void Setup()
        {
            _dialogHandler = new Mock<IDialogHandler>();
            _imageHelper = new Mock<ImageHelper>(_dialogHandler.Object);

            _directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [Test]
        public void ChosenImage_SetDefaultImage()
        {
            //Arrange
            string imagePath = Path.Combine(_directoryPath, @"Files\Test.jpg");
            var defaultImage = new BitmapImage(new Uri(imagePath));
            _imageHelper.Protected().Setup<ImageSource>("GetDefaultImage").Returns(defaultImage);
            _imageHelper.CallBase = true;

            //Act
            var returnedImage =_imageHelper.Object.ChosenImage;

            //Assert
            Assert.AreEqual(returnedImage, defaultImage);
        }

        [Test]
        [ExpectedException(typeof(NonImageFileChosenException))]
        public void RefreshChosenImageFromUserChoice_ThrowsNotSupportedExceptionThrown()
        {
            //Arrange
            string nonImagePath = Path.Combine(_directoryPath, @"Files\NotAnImage.txt");
            var nonImageUri = new Uri(nonImagePath);
            _dialogHandler.Setup(x => x.GetImageUriFromUser()).Returns(nonImageUri);

            //Act
            _imageHelper.Object.RefreshChosenImageFromUserChoice();
        }

        [Test]
        public void RefreshChosenImageFromUserChoice_ChosenImageRefreshed()
        {
            //Arrange
            string imagePath = Path.Combine(_directoryPath, @"Files\Test.jpg");
            var imageUri = new Uri(imagePath);
            _dialogHandler.Setup(x => x.GetImageUriFromUser()).Returns(imageUri);
            _imageHelper.SetupSet(x => x.ChosenImage = It.IsAny<ImageSource>()).Verifiable();

            //Act
            _imageHelper.Object.RefreshChosenImageFromUserChoice();

            //Assert
            _imageHelper.VerifySet(x => x.ChosenImage = It.IsAny<ImageSource>(), Times.Once());
        }

    }
}
