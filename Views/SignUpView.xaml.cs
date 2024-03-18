using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class SignUpView : ContentPage
{
	public SignUpView(SignUpViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}