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
            Mock<ImageSourceConverter> converterMock = new Mock<ImageSourceConverter>();
            converterMock.Setup(c => c.ConvertFromString(It.IsAny<string>())).Returns<object>(null);

            Mock<ImageSourceHelper> imageHelperMock = new Mock<ImageSourceHelper>();
            imageHelperMock.Setup(x => x.GetDefaultImageSource()).Returns(new Object() as ImageSource);

            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.ImageHelper = imageHelperMock.Object as ImageSourceHelper;

            var ImageSourceProperty = mainViewModel.ChosenImageSource;
            imageHelperMock.Verify(x => x.GetDefaultImageSource(), Times.Exactly(1));
        }

        [TestMethod]
        public void SetDefaulImageWhenNoneIsChosen2()
        {
            Mock<ImageSourceConverter> converterMock = new Mock<ImageSourceConverter>();
            converterMock.Setup(c => c.ConvertFromString(It.IsAny<string>())).Returns(new object() as ImageSource);

            ImageSourceHelper imageHelper = new ImageSourceHelper();
            imageHelper.Converter = converterMock.Object as ImageSourceConverter;

            var ImageSourceProperty = imageHelper.ImageSource;
            converterMock.Verify(c => c.ConvertFromString(It.IsAny<string>()), Times.Exactly(1));
        }
    }
}
