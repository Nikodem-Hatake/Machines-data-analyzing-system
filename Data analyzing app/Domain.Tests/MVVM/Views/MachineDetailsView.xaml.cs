using Domain.Tests.MVVM.Models;
using Domain.Tests.MVVM.ViewModels;

namespace Domain.Tests.MVVM.Views;

public partial class MachineDetailsView : ContentPage
{
	public MachineDetailsView(DataBaseContext dataBaseContext, Machine machine)
	{
		this.BindingContext = new MachineDetailsViewModel(dataBaseContext, machine);
		InitializeComponent();
	}
}