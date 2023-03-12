namespace MyWorkoutPal;
using MyWorkoutPal.Pages;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ManageExercices), typeof(ManageExercices));
	}
}
