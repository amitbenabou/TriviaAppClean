using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel:ViewModelBase
    {

        public ProfileViewModel()
        {
            User u = ((App)Application.Current).LoggedInUser;
            Email = u.Email;
            Password = u.Password;
            Name = u.Name;
            Id = u.Id;
            Score = u.Score;
            Rank = u.Rank;

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
            }
        }
        public int Id
        {

            get { return id; }
            set { id = value; }
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
            }
        }
        public string Password
        {
            get { return password; }
            set { this.password = value; }
        }
        public int Score
        {
            get
            {
                return score;
            }
            set { score = value; }
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
            }
        }

        private async void ReadUsers()
        {
            TriviaWebAPIProxy service = this.userService;
            List<User> list = await service.GetAllUsers();
            this.Users = new ObservableCollection<User>(list);

        }
















    }


}
