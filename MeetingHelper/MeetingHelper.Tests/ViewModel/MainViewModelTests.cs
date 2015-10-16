using System;


using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Windows.Media;
using MeetingHelper.Helpers;
using System.ComponentModel;
using MeetingHelper.Helpers.Time;
using MeetingHelper.Helpers.Image;
using MeetingHelper.Tests.Testables;
using MeetingHelper.Events;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace MeetingHelper.ViewModel.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        #region Setup
        private Mock<TestableMainViewModel> _viewModel;
        private Mock<IImageHelper> _imageHelper;
        private Mock<ITimeHelper> _timeHelper;

        [SetUp]
        public void Setup()
        {
            _viewModel = new Mock<TestableMainViewModel>() { CallBase = true };
            _imageHelper = new Mock<IImageHelper>();
            _timeHelper = new Mock<ITimeHelper>();

            _viewModel.Object.Timer = _timeHelper.Object;
            _viewModel.Object.ImageHelper = _imageHelper.Object;
        }
        #endregion

        #region Image
        [Test]
        public void ImageClicked_CallsImageRefreshedFromUserChoice()
        {
            //Arrange
            _imageHelper.Setup(x => x.RefreshChosenImageFromUserChoice()).Verifiable();

            //Act
            _viewModel.Object.ImageClicked.Execute(null);

            //Assert
            _imageHelper.Verify(x => x.RefreshChosenImageFromUserChoice(), Times.Once());
        }

        [Test]
        public void ImageClicked_SetsChosenImage()
        {
            //Arrange
            _viewModel.SetupSet(x => x.ChosenImage = It.IsAny<ImageSource>()).Verifiable();

            //Act
            _viewModel.Object.CallImageClickedCommand();

            //Assert
            _viewModel.VerifySet(x => x.ChosenImage = It.IsAny<ImageSource>(), Times.Once);
        }

        [Test]
        public void ChosenImage_DefaultValueSet()
        {
            //Arrange
            var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string imagePath = Path.Combine(directoryPath, @"Files\Test.jpg");
            var defaultImage = new BitmapImage(new Uri(imagePath));
            _imageHelper.SetupGet(x => x.ChosenImage).Returns(defaultImage);

            //Act
            var chosenImage = _viewModel.Object.ChosenImage;

            //Assert
            Assert.AreEqual(chosenImage, defaultImage);
        }
        #endregion

        #region Time
        [Test]
        public void TimerClicked_CallsTimeHelperTimerClicked()
        {
            //Arrange
            _timeHelper.Setup(x => x.TimerClicked()).Verifiable();

            //Act
            _viewModel.Object.TimerClicked.Execute(null);

            //Assert
            _timeHelper.Verify(x => x.TimerClicked(), Times.Once());
        }

        [Test]
        public void DisplayTime_DefaultValueSet()
        {
            //Act
            var defaultDisplayTime = _viewModel.Object.DisplayTime;

            //Assert
            Assert.AreEqual("00:00:00.00", defaultDisplayTime);
        }

        [Test]
        public void TimeUpdated_UpdatesDisplayTime()
        {
            //Arrange
            var newTime = new TimeSpan(0, 1, 20, 45, 95);
            var eventArgs = new TimeUpdatedEventArgs(newTime);
            var expected = _viewModel.Object.CallCreateDisplayTime(newTime);
            
            //Act
            _viewModel.Object.CallUpdateDisplayTime(new object(), eventArgs);

            //Assert
            Assert.AreEqual(expected, _viewModel.Object.DisplayTime);
        }
        #endregion
    }
}
