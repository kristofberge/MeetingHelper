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
            _viewModel.Object.CallImageClickedCommand();

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
        #endregion
    }
}
