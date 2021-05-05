using SYL_Mobile.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SYL_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly Regex passwordRegex = new Regex(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$");

        public class Type
        {
            public string TypeName { get; set; }
            public int TypeInt { get; set; }
        }

        IList<Type> types;
        public IList<Type> Types { get { return types; } }

        public RegisterPage()
        {
            InitializeComponent();
            int i = 0;
            types = new List<Type>();

            foreach(var type in Enum.GetNames(typeof(UserTypeEnum)))
            {
                types.Add(new Type { TypeName = type, TypeInt = i });
                i++;
            }
            UserType.ItemsSource = (System.Collections.IList)types;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            if(name.Text == null || lastName.Text == null || name.Text == "" || lastName.Text == "")
            {
                await DisplayAlert("", "Please enter a name and last name", "Ok");
                
            }
            else
            {
                if(!IsEmailValid(email.Text))
                {
                    await DisplayAlert("", "The email you entered is invalid", "Ok");
                }
                else
                {
                    if(password.Text == null || repeatedPassword.Text == null)
                    {
                        await DisplayAlert("", "Please enter two passwords", "Ok");
                    }
                    else
                    {
                        if(!passwordRegex.IsMatch(password.Text) && password.Text != repeatedPassword.Text)
                        {
                            await DisplayAlert("", "The password must contain at least 1 uppercase, 1 lowercase character, 1 number and 1 special character", "Ok");
                        }
                        else
                        {
                            if (UserType.SelectedItem == null)
                            {
                                await DisplayAlert("", "Please choose a user type", "Ok");
                            }
                            else
                            {
                                if (await App._userService.AddNewUser(new NewUserDTO { userName = name.Text, userLastName = lastName.Text, userEmail = email.Text, userPassword = password.Text, userType = ((Type)UserType.SelectedItem).TypeInt }))
                                {
                                    await DisplayAlert("", "UserCreated", "Ok");
                                    App.Current.MainPage = new AppShell();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
        
        bool IsEmailValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}