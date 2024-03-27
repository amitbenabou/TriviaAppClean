using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class GameViewModel:ViewModelBase
    {
        TriviaWebAPIProxy triviaService;

        private List<string> answers;
        public List<string> Answers
        {
            get { return answers; }
            set
            {
                answers = value;
                OnPropertyChanged();
            }
        }
        public GameViewModel(TriviaWebAPIProxy service) 
        { 
            this.triviaService = service;
            this.NextQuestionCommand = new Command(GetQuestion);
            GetQuestion();

        }
        private AmericanQuestion question;
        public AmericanQuestion Question
        {
            get { return question; }
            set { question = value;
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

        public ICommand AnswerCommand1 { get; set; }

        public ICommand AnswerCommand2 { get; set; }

        public ICommand AnswerCommand3 { get; set; }

        public ICommand AnswerCommand4 { get; set; }
        public ICommand NextQuestionCommand { get; set; }
        


        private async void GetQuestion()
        {
            Question = await triviaService.GetRandomQuestion();
            ScrambleAnswers();
        }
        private void ScrambleAnswers()
        {
            this.Answers = new List<string>();
            this.Answers.Add(Question.CorrectAnswer);
            this.Answers.Add(Question.Bad1);
            this.Answers.Add(Question.Bad2);
            this.Answers.Add(Question.Bad3);
            

        }
        private void GetCorrectAnswer(string answer)
        {
          if  (question != null)
            {
                if (question.CorrectAnswer == answer)
                {
                    score = score + 10;
                    ButtonColor = Colors.Green;
                }
                else if(question.CorrectAnswer == AnswerCommand2.ToString())
                {
                    score = score + 10;
                    ButtonColor = Colors.Green;
                }
                else if (question.CorrectAnswer == AnswerCommand3.ToString())
                {
                    score = score + 10;
                    ButtonColor = Colors.Green;
                }
                else
                {
                    score = score + 10;
                    ButtonColor = Colors.Green;
                }

            }
        }

















    }
}
