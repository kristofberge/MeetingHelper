using System;

using NUnit.Framework;
using MeetingHelper.Helpers;
using Moq;
using Moq.Protected;

namespace MeetingHelper.Tests.Helpers
{
    [TestFixture]
    public class ImageHelperTests
    {
        [Test]
        public void UserCancelsDialog_ImageNotRefreshed()
        {
            Mock<ImageHelper> imageHelper = new Mock<ImageHelper>();
            imageHelper.Protected().Setup<bool>("UserCancelledDialog").Returns(true);

            var imageHelperObj = imageHelper.Object as ImageHelper;
            imageHelperObj.RefreshChosenImageFromUserChoice();


        }
    }
}
