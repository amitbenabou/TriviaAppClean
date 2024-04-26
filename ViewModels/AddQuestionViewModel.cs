using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class AddQuestionViewModel : ViewModelBase
    {
        private string ifInValid;
        public string IfInValid
        {
            get
            {
                return ifInValid;
            }
            set
            {
                ifInValid = value;
                OnPropertyChanged();
            }
        }

        private bool showIfInValid;
        public bool ShowIfInValid
        {
            get
            {
                return showIfInValid;
            }
            set
            {
                showIfInValid = value;
                OnPropertyChanged();
            }
        }

        private bool canAddQuestion;//אם אפשרי להוסיף את השאלה
        public bool CanAddQuestion
        {
            get
            {
                return canAddQuestion;
            }
            set
            {
                canAddQuestion = value;
                OnPropertyChanged();
            }
        }

        private bool IsAccessApprove()//בודק האם השחקן בדרגה שיכולה להוסיף שאלות או שיש לו מספיק נקודות
        {
            if (((App)Application.Current).LoggedInUser.Rank == 2 || ((App)Application.Current).LoggedInUser.Score >= 100)
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        private string questionText;
        public string QuestionText
        {
            get
            {
                return questionText;
            }
            set
            {
                questionText = value;
                OnPropertyChanged();
            }
        }

        private string correctAnswer;
        public string CorrectAnswer
        {
            get
            {
                return correctAnswer;
            }
            set
            {
                correctAnswer = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer1;
        public string WrongAnswer1
        {
            get
            {
                return wrongAnswer1;
            }
            set
            {
                wrongAnswer1 = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer2;
        public string WrongAnswer2
        {
            get
            {
                return wrongAnswer2;
            }
            set
            {
                wrongAnswer2 = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer3;
        public string WrongAnswer3
        {
            get
            {
                return wrongAnswer3;
            }
            set
            {
                wrongAnswer3 = value;
                OnPropertyChanged();
            }
        }

        private TriviaWebAPIProxy triviaService;
       // private ConnectingToServerView connectingToServerView;
        public AddQuestionViewModel(TriviaWebAPIProxy service/*, ConnectingToServerView connect*/)
        {
            this.triviaService = service;
            //this.connectingToServerView = connect;
            this.IfInValid = "יש למלא כל שדה כדי להוסיף שאלה";
            this.CanAddQuestion = IsAccessApprove();
            this.AddingCommand = new Command(OnAddQuestion);
        }

        private bool ValidateForm()
        {
            //Validate all fields first
            if (string.IsNullOrEmpty(questionText)==false && string.IsNullOrEmpty(CorrectAnswer)==false && string.IsNullOrEmpty(wrongAnswer1)==false && string.IsNullOrEmpty(wrongAnswer2)==false && string.IsNullOrEmpty(wrongAnswer3)==false)
            {
                ShowIfInValid = false;
            }
            else
            {
                ShowIfInValid = true;
            }

            //check if any validation failed
            if (ShowIfInValid)
                return false;
            return true;
        }
        public ICommand AddingCommand { get; set; }

        private async void OnAddQuestion()
        {
            if (ValidateForm())
            {
                AmericanQuestion q = new AmericanQuestion();
                q.QText = questionText;
                q.CorrectAnswer = correctAnswer;
                q.Bad1 = wrongAnswer1;
                q.Bad2 = wrongAnswer2;
                q.Bad3 = wrongAnswer3;
                q.UserId = ((App)Application.Current).LoggedInUser.Id;
                if (((App)Application.Current).LoggedInUser.Rank == 2) { q.Status = 1; }
                else
                {
                    q.Status = 0;
                }
                if (IsAccessApprove())
                {
                    //await Shell.Current.Navigation.PushModalAsync(connectingToServerView);
                    bool a = await this.triviaService.PostNewQuestion(q);
                    await Shell.Current.Navigation.PopModalAsync();

                    if (a == true)
                    {
                        await Shell.Current.DisplayAlert("Add Qustion", "Question add to the game was successfull!", "ok");
                        this.ShowIfInValid = IsAccessApprove();
                        this.canAddQuestion = IsAccessApprove();
                        this.QuestionText = "";
                        this.CorrectAnswer = "";
                        this.WrongAnswer1 = "";
                        this.WrongAnswer2 = "";
                        this.WrongAnswer3 = "";
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Add Qustion", "Question add failed", "ok");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Add Qustion", "Cannot add question yet", "ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Add Question", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);
            }

        }
    }
}

