using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class HighScoresView : ContentPage
{
	public HighScoresView(HighScoresViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}