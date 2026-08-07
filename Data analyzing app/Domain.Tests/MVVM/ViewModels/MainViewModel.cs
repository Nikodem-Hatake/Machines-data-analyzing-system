using Domain.Tests.MVVM.Models;
using Domain.Tests.MVVM.Views;
using Microsoft.EntityFrameworkCore;
using PropertyChanged;

namespace Domain.Tests.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private DataBaseContext _dataBaseContext;
        public List <Machine> Machines { get; private set; }
        public Command RefreshCommand { get; }
        public Command ShowDetailsCommand { get; }

        public async void GetMachinesAsync()
        {
            try
            {
                this.Machines = await this._dataBaseContext.Machine.ToListAsync();
            }
            catch(Exception e)
            {
                ExceptionsHandler.LogExceptionToAlertAsync($"Data base error occured when tried to load Machines: {e.Message}");
            }
        }

        public MainViewModel(DataBaseContext dataBaseContext)
        {
            this._dataBaseContext = dataBaseContext;
            this.Machines = new List<Machine>();
            this.GetMachinesAsync();

            this.RefreshCommand = new Command(this.GetMachinesAsync);
            this.ShowDetailsCommand = new Command((obj) =>
            {
                if(obj is Machine machine)
                {
                    App.Current?.Windows[0].Page?.Navigation.PushAsync
                    (new MachineDetailsView(this._dataBaseContext, machine));
                }
            });
        }
    }
}