using Domain.Tests.MVVM.Models;
using Microsoft.EntityFrameworkCore;
using PropertyChanged;

namespace Domain.Tests.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MachineDetailsViewModel
    {
        private const int MAX_AMOUNT_OF_MACHINE_DETAILS_ON_PAGE = 10;

        private DataBaseContext _dataBaseContext;
        private int _howManyMachineDatasToSkip;
        public Machine Machine { get; private set; }
        public List<MachineDatas> MachineDatas { get; private set; }
        public MachineDetails MachineDetails { get; private set; }
        public Command NextPaginationMachineDetailCommand { get; }
        public Command PreviousPaginationMachineDetailCommand { get; }
        public Command RefreshCommand { get; }

        private async Task<List<MachineDatas>>GetMachineDatas()
        {
            try
            {
                return await this._dataBaseContext.MachineDatas
                    .Where(x => x.MachineId == this.Machine.Id)
                    .Skip(this._howManyMachineDatasToSkip)
                    .Take(MAX_AMOUNT_OF_MACHINE_DETAILS_ON_PAGE)
                    .ToListAsync();
            }
            catch(Exception e)
            {
                ExceptionsHandler.LogExceptionToAlertAsync
                ($"Error occured when tried to get machine datas: {e.Message}");
            }
            return new List<MachineDatas>();
        }

        private async void GetMachineDatasAsync()
        {
            List<MachineDatas> machineDatas = await this.GetMachineDatas();
            if(machineDatas.Count == 0)
            {
                this._howManyMachineDatasToSkip = Math.Max(this._howManyMachineDatasToSkip
                - MAX_AMOUNT_OF_MACHINE_DETAILS_ON_PAGE, 0);
                machineDatas = await this.GetMachineDatas();
            }
            else if(machineDatas.Count < MAX_AMOUNT_OF_MACHINE_DETAILS_ON_PAGE)
            {
                this._howManyMachineDatasToSkip -= this._howManyMachineDatasToSkip % MAX_AMOUNT_OF_MACHINE_DETAILS_ON_PAGE;
            }

            this.MachineDatas = machineDatas;
        }

        private async Task <MachineDetails> GetMachineDetails()
        {
            try
            {
                IQueryable<MachineDatas> machineDatasForCurrentMachine = this._dataBaseContext.MachineDatas
                    .Where(x => x.MachineId == this.Machine.Id);
                MachineDetails machineDetails = new MachineDetails()
                {
                    AverageTemperature = machineDatasForCurrentMachine.Average(x => x.Temperature),
                    AverageTimeProcessingRecources = machineDatasForCurrentMachine
                        .Sum(x => x.SecondsInWhichResourcesWasProcessed),
                    LastUpdateDateTime = machineDatasForCurrentMachine.Max(x => x.UpdateDataDate),
                    TotalResourcesProcessed = machineDatasForCurrentMachine
                        .Sum(x => x.NumberOfProcessedResourcesSinceGettingData)
                };

                if(machineDetails.TotalResourcesProcessed != 0)
                {
                    machineDetails.AverageTimeProcessingRecources /= machineDetails.TotalResourcesProcessed;
                }
                return machineDetails;
            }
            catch(Exception e)
            {
                ExceptionsHandler.LogExceptionToAlertAsync
                ($"Error occured when tried to get machine details: {e.Message}");
            }

            return new MachineDetails();
        }

        private async void GetMachineDetailsAsync() => this.MachineDetails = await this.GetMachineDetails();

        public MachineDetailsViewModel(DataBaseContext dataBaseContext, Machine machine)
        {
            this._dataBaseContext = dataBaseContext;
            this.Machine = machine;
            this.MachineDatas = new List<MachineDatas>();
            this.MachineDetails = new MachineDetails();
            this.GetMachineDetailsAsync();
            this.GetMachineDatasAsync();

            this.RefreshCommand = new Command(() =>
            {
                this._howManyMachineDatasToSkip = 0;
                this.GetMachineDetailsAsync();
                this.GetMachineDatasAsync();
            });

            this.NextPaginationMachineDetailCommand = new Command(() =>
            {
                this._howManyMachineDatasToSkip += MAX_AMOUNT_OF_MACHINE_DETAILS_ON_PAGE;
                this.GetMachineDatasAsync();
            });

            this.PreviousPaginationMachineDetailCommand = new Command(() =>
            {
                if(this._howManyMachineDatasToSkip == 0)
                {
                    return;
                }

                this._howManyMachineDatasToSkip -= MAX_AMOUNT_OF_MACHINE_DETAILS_ON_PAGE;
                this.GetMachineDatasAsync();
            });
        }
    }
}
