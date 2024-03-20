using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;

	}
}