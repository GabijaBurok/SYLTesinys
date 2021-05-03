using System;
using System.Collections.Generic;
using System.Text;

namespace SYL_Mobile.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
