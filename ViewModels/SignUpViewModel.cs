using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;


namespace TriviaAppClean.ViewModels
{
  
    public class SignUpViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
      
        public SignUpViewModel(TriviaWebAPIProxy service) 
        {
            this.triviaService = service;
            this.NameError = "זהו שדה חובה";
            this.PasswordError = "זהו שדה חובה ";
            this.EmailError = "מייל לא תקין ";
            this.SignUpCommand = new Command(OnSignUp);

        }
   
        #region שם
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion

        #region סיסמא
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
        }
        #endregion


        #region מייל
        private bool showEmailError;

        public bool ShowEmailError//ShowEmailError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            string email = Email;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            this.ShowEmailError = !match.Success;
        }
        #endregion
        private bool ValidateForm()
        {
            ValidateEmail();
            ValidatePassword();
            ValidateName();
            if (showEmailError || showNameError || ShowPasswordError)
            {
                return false;   
            }
            return true;

        }

        public ICommand SignUpCommand { get; set; }
        private async void OnSignUp()
        {
            if(ValidateForm())
            {
                User user =new User()
                {
                    Name=this.name,
                    Email=this.email,
                    Password=this.password
                };
                bool succes=await triviaService.RegisterUser(user);
                await App.Current.MainPage.DisplayAlert("Sign Up", $"Sign Up Succeed! ", "ok");
                await App.Current.MainPage.Navigation.PopAsync(); 
                //App.Current.MainPage = new AppShell();

                if (!succes)
                {
                    await App.Current.MainPage.DisplayAlert("Sign up", "Sign up Faild!", "ok");
                }
                else
                {
                    await App.Current.MainPage.Navigation.PopAsync();   
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Sign up", "Sign up Faild!", "ok");
            }
        }


    }
}
