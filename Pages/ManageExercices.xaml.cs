using System.Diagnostics;
using MyWorkoutPal.Services;
using MyWorkoutPal.Models;

namespace MyWorkoutPal.Pages;

[QueryProperty(nameof(Exercice), "Exercice")]
public partial class ManageExercices : ContentPage
{
	private readonly IRestDataService _dataService;
	Exercice _exercice;
	bool _isNew;
	public ManageExercices(IRestDataService dataService)
	{
		InitializeComponent();
		_dataService = dataService;
		BindingContext = this;
	}

	public Exercice Exercice 
	{
		get => _exercice;
		set
		{
			_isNew = IsNew(value);
			_exercice = value;
			OnPropertyChanged();
		}
	}
	bool IsNew(Exercice exercice)
	{
		if( exercice.Id == 0 )
		return true;
		return false;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if (_isNew)
		{
			await _dataService.AddExerciceAsync(Exercice);
		}
		else
		{
            await _dataService.UpdateExerciceAsync(Exercice);
        }
        await Shell.Current.GoToAsync("..");
    }
	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _dataService.DeleteExerciceAsync(Exercice.Id);
        await Shell.Current.GoToAsync("..");
    }

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}