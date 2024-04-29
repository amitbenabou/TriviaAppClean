using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        TriviaWebAPIProxy triviaService;

        private ObservableCollection<string> answers;
        public ObservableCollection<string> Answers
        {
            get { return answers; }
            set
            {
                answers = value;
                OnPropertyChanged();
            }
        }
        public ICommand AnswerCommand1 { get; set; }

        public ICommand AnswerCommand2 { get; set; }

        public ICommand AnswerCommand3 { get; set; }

        public ICommand AnswerCommand4 { get; set; }
        public ICommand NextQuestionCommand { get; set; }
        private Color answer1Color;
        public Color Answer1Color
        {
            get { return this.answer1Color; }
            set
            {
                this.answer1Color = value;
                OnPropertyChanged();
            }
        }
        private Color answer2Color;
        public Color Answer2Color
        {
            get { return this.answer2Color; }
            set
            {
                this.answer2Color = value;
                OnPropertyChanged();
            }
        }
        private Color answer3Color;
        public Color Answer3Color
        {
            get { return this.answer3Color; }
            set
            {
                this.answer3Color = value;
                OnPropertyChanged();
            }
        }
        private Color answer4Color;
        public Color Answer4Color
        {
            get { return this.answer4Color; }
            set
            {
                this.answer4Color = value;
                OnPropertyChanged();
            }
        }

        public GameViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            User u = ((App)Application.Current).LoggedInUser;
            Name = u.Name;
            this.NextQuestionCommand = new Command(GetQuestion);
            this.AnswerCommand1 = new Command(GetCorrectAnswer1);
            this.AnswerCommand2 = new Command(GetCorrectAnswer2);
            this.AnswerCommand3 = new Command(GetCorrectAnswer3);
            this.AnswerCommand4 = new Command(GetCorrectAnswer4);
            this.Answer1Color = new Color();
            this.Answer2Color = new Color();
            this.Answer3Color = new Color();
            this.Answer4Color = new Color();
            Answers = new ObservableCollection<string>();
         
            GetQuestion();

        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
              
                OnPropertyChanged("Name");
            }
        }
        private AmericanQuestion question;
        public AmericanQuestion Question
        {
            get { return question; }
            set
            {
                question = value;
                OnPropertyChanged();
            }


        }
        private int score;
        public int Score
        {
            get { return this.score; }
            set
            {
                score = value;
                OnPropertyChanged();
            }
        }
        private Color buttonColor;
        public Color ButtonColor
        {
            get { return this.buttonColor; }
            set
            {
                this.buttonColor = value;
                OnPropertyChanged();
            }
        }





        private async void GetQuestion()
        {
            Question = await triviaService.GetRandomQuestion();
            ScrambleAnswers();

        }
        private void ScrambleAnswers()
        {
            this.Answers.Clear();
            this.Answers.Add(Question.CorrectAnswer);
            this.Answers.Add(Question.Bad1);
            this.Answers.Add(Question.Bad2);
            this.Answers.Add(Question.Bad3);
            this.Answer1Color = new Color();
            this.Answer2Color = new Color();
            this.Answer3Color = new Color();
            this.Answer4Color = new Color();
            OnPropertyChanged("Answers");


        }


        private void GetCorrectAnswer1()
        {
            string answer = Answers[0];
            if (question != null)
            {
                if (question.CorrectAnswer == answer)
                {
                    Score = Score + 10;
                    Answer1Color = Colors.Green;
                }
                else { Answer1Color = Colors.Red; }
               

            }
        }
        private void GetCorrectAnswer2()
        {
            string answer = Answers[1];
            if (question != null)
            {
                if (question.CorrectAnswer == answer)
                {
                    Score = Score + 10;
                    Answer2Color = Colors.Green;
                }
                else { Answer2Color = Colors.Red; }
              

            }
        }
        private void GetCorrectAnswer3()
        {
            string answer = Answers[2];
            if (question != null)
            {
                if (question.CorrectAnswer == answer)
                {
                    Score = Score + 10;
                    Answer3Color = Colors.Green;
                }
                else { Answer3Color = Colors.Red; }
                

            }
        }
        private void GetCorrectAnswer4()
        {
            string answer = Answers[3];
            if (question != null)
            {
                if (question.CorrectAnswer == answer)
                {
                    Score = Score + 10;
                    Answer4Color = Colors.Green;
                }
                else { Answer4Color = Colors.Red; }
              

            }
        }
















    }
}
