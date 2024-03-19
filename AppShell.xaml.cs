using TriviaAppClean.Models;
using TriviaAppClean.Views;

namespace TriviaAppClean;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
		this.BindingContext = this;
	}

	private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
    }

	public bool IsAdmin 
	{
		get
		{
			User currentUser = ((App)Application.Current).LoggedInUser;
			if (currentUser == null || currentUser.Rank < 2) 
			{
				return false;
			}
			return true;
		}
	}

}
