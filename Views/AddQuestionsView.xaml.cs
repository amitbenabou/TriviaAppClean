using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class AddQuestionsView : ContentPage
{
	public AddQuestionsView(AddQuestionViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}