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
        public void SetDefaulImageWhenNoneIsChosen()
        {
            Mock<ImageSourceHelper> imageHelperMock = new Mock<ImageSourceHelper>();
            imageHelperMock.Setup(x => x.GetDefaultImageSource()).Returns(new Object() as ImageSource);

            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.ImageHelper = imageHelperMock.Object as ImageSourceHelper;

            var ImageSourceProperty = mainViewModel.ChosenImageSource;
            imageHelperMock.Verify(x => x.GetDefaultImageSource(), Times.Exactly(1));
        }

        [TestMethod]
        public void ImageUnchangedIfUserCancelsDialog()
        {
            Mock<ImageSourceHelper> imageHelperMock = new Mock<ImageSourceHelper>();

            

            bool res2 = test1 == test2;
            bool res = test1.Equals(test2);
        }
    }
}
