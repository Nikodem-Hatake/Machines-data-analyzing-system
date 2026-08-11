using Domain.Tests.MVVM.Models;
using Domain.Tests.MVVM.ViewModels;

namespace Domain.Tests.MVVM.Views;

public partial class MachineDetailsView : ContentPage
{
	private MachineDetailsViewModel _viewModel;

    protected override void OnAppearing()
    {
		this._viewModel.GetDataOnLoad();
        base.OnAppearing();
    }
	public MachineDetailsView(DataBaseContext dataBaseContext, Machine machine)
	{
		this._viewModel = new MachineDetailsViewModel(dataBaseContext, machine);
		this.BindingContext = this._viewModel;
		InitializeComponent();
	}
}