using Microsoft.Maui.ApplicationModel.Communication;
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
    public class HighScoresViewModel : ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;

        public HighScoresViewModel(TriviaWebAPIProxy service)
        {
            triviaService = service;
            this.SearchCommand = new Command(OnSearch);
            ReadUsers();
        }

        private string name;
        private int score;

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
        public int Score
        {
            get
            {
                return score;
            }
            set { score = value; }
        }
        private async void ReadUsers()
        {
            List<User> list = await triviaService.GetAllUsers();
            list = list.OrderByDescending(u => u.Score).ToList();
            this.Users = new ObservableCollection<User>(list);
        }

        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value ?? string.Empty;
                if (searchText == string.Empty) ReadUsers();
                OnPropertyChanged("SearchText");
            }
        }
        public ICommand SearchCommand { get; set; }

        private async void OnSearch()
        {
            List<User> list = await triviaService.GetAllUsers();
            List<User> listSearch = new List<User>();
            var searchQuery = searchText?.ToLower() ?? "";
            listSearch = list.Where(u => u.Name.Contains(searchQuery)).ToList();
            this.Users = new ObservableCollection<User>(listSearch);
        }
    }
}
