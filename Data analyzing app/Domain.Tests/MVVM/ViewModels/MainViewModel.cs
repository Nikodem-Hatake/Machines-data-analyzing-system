using Domain.Tests.MVVM.Models;
using Domain.Tests.MVVM.Views;
using PropertyChanged;
using System.Text.Json;

namespace Domain.Tests.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private APIConnectionManager _APIConnectionManager;
        public List <Machine> Machines { get; private set; }
        public Command RefreshCommand { get; }
        public Command ShowDetailsCommand { get; }

        public MainViewModel(APIConnectionManager APIConnectionManager)
        {
            _APIConnectionManager = APIConnectionManager;
            Machines = new List<Machine>();
            GetMachinesAsync();

            RefreshCommand = new Command(GetMachinesAsync);
            ShowDetailsCommand = new Command((obj) =>
            {
                if(obj is Machine machine)
                {
                    App.Current?.Windows[0].Page?.Navigation.PushAsync
                    (new MachineDetailsView(_APIConnectionManager, machine));
                }
            });
        }

        public async void GetMachinesAsync()
        {
            try
            {
                List<Machine>? machines = JsonSerializer.Deserialize<List<Machine>> 
                    (await _APIConnectionManager.Get("/machines"),
                    new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                if(machines != null)
                {
                    Machines = machines;
                }
            }
            catch(Exception e)
            {
                ExceptionsHandler.LogExceptionToAlertAsync
                    ($"Error occured when tried to load Machines: {e.Message}");
            }
        }
    }
}