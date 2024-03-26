using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    public class GameViewModel:ViewModelBase
    {
        private AmericanQuestion question;
        public AmericanQuestion Question
        {
            get { return question; }
            set { question = value; }


        }
        public ICommand AnswerCommand1 { get; set; }

        public ICommand AnswerCommand2 { get; set; }

        public ICommand AnswerCommand3 { get; set; }

        public ICommand AnswerCommand4 { get; set; }
        public ICommand NextQuestionCommand { get; set; }
        



















    }
}
