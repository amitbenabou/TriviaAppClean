using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        

        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            User u = ((App)Application.Current).LoggedInUser;
            Email = u.Email;
            Password = u.Password;
            Name = u.Name;
            Id = u.Id;
            Score = u.Score;
            Rank = u.Rank;
            this.UpdateCommand = new Command(onUpdate);
            triviaService = service;
            this.NameError = "זהו שדה חובה";
            this.PasswordError = "זהו שדה חובה ";
            this.EmailError = "מייל לא תקין ";

        }
        private string email;
        private string password;
        private string name;
        private int id;
        private int score;
        private int rank;
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            { return users; }
            set
            {
                this.users = value;
                OnPropertyChanged();
            }
        }
        private TriviaWebAPIProxy userService;
        public TriviaWebAPIProxy UserService
        {
            get { return userService; }
            set
            {
                this.userService = value;
                OnPropertyChanged();
            }
        }
        public int Id
        {

            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                this.email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }
        public string Password
        {
            get { return password; }
            set { this.password = value; ValidatePassword();
                OnPropertyChanged("Password"); }
        }
        public int Score
        {
            get
            {
                return score;
            }
            set { score = value; OnPropertyChanged(); }
        }
        public int Rank
        {
            get
            {
                return rank;
            }
            set
            {
                rank = value;
                OnPropertyChanged();
            }
        }

        private async void ReadUsers()
        {
            TriviaWebAPIProxy service = this.userService;
            List<User> list = await service.GetAllUsers();
            this.Users = new ObservableCollection<User>(list);

        }

        public ICommand UpdateCommand
        {
            get; set;
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

        private async void onUpdate()
        {
            if (ValidateForm())
            {
                User u = ((App)Application.Current).LoggedInUser;
                u.Name = Name;
                u.Email = Email;
                u.Password = Password;
                await Shell.Current.GoToAsync("connectingToServer");
                bool success = await triviaService.UpdateUser(u);
                await Shell.Current.Navigation.PopModalAsync();


            }

        }
        
















    }


}
