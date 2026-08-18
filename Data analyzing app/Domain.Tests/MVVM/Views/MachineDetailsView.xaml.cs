using Domain.Tests.MVVM.Models;
using Domain.Tests.MVVM.ViewModels;

namespace Domain.Tests.MVVM.Views;

public partial class MachineDetailsView : ContentPage
{
	public MachineDetailsView(APIConnectionManager APIConnectionManager, Machine machine)
	{
		BindingContext = new MachineDetailsViewModel(APIConnectionManager, machine);
		InitializeComponent();
	}
}