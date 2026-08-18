using Domain.Tests.MVVM.ViewModels;

namespace Domain.Tests.MVVM.Views;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel mainViewModel)
	{
		BindingContext = mainViewModel;
		InitializeComponent();
	}
}