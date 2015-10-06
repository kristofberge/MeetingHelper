using MeetingHelper.Exceptions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MeetingHelper.Helpers.Image
{
    public class DialogHandler : IDialogHandler
    {
        private OpenFileDialog _chooseImageDialog;

        public Uri GetImageUriFromUser()
        {
            if (UserChoosesImage() == true)
                return GetUriFromDialog();
            else
                throw new UserCancelledDialogException();
        }
        protected virtual bool? UserChoosesImage()
        {
            SetupChooseImageDialog();
            return _chooseImageDialog.ShowDialog();
        }

        protected virtual Uri GetUriFromDialog()
        {
            return new Uri(_chooseImageDialog.FileName);
        }

        private void SetupChooseImageDialog()
        {
            _chooseImageDialog = new OpenFileDialog();
            _chooseImageDialog.Filter =
                "Image files (*.jpg, *.jpeg, *.bmp, *.gif, *.png)|*.jpg;*.jpeg;*.bmp;*.gif;*.png|"
                + "All files (*.*)|*.*";
        }
    }
}
