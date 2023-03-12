namespace MyWorkoutPal;
using MyWorkoutPal.Services;
using MyWorkoutPal.Pages;
using MyWorkoutPal.Models;

public partial class MainPage : ContentPage
{
	private readonly IRestDataService _dataService;

	public MainPage(IRestDataService dataService)
	{
		InitializeComponent();
		_dataService = dataService;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		collectionView.ItemsSource = await _dataService.GetAllExercicesAsync();
	}

	async void OnAddExerciceClicked(object sender, EventArgs e)
	{
		var navigationParameter = new Dictionary<string, object>
		{
			{ nameof(Exercice), new Exercice() }
		};

		await Shell.Current.GoToAsync(nameof(ManageExercices), navigationParameter);
	}

	async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(Exercice), e.CurrentSelection.FirstOrDefault() as Exercice }
        };
        await Shell.Current.GoToAsync(nameof(ManageExercices), navigationParameter);
    }
}


