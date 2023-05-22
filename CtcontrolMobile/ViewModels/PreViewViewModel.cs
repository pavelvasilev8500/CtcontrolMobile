using CtcontrolAPIService.Models;
using CtcontrolAPIService.Services;
using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.EventAgregator;
using CtcontrolMobile.Models;
using CtcontrolMobile.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading;

namespace CtcontrolMobile.ViewModels
{
    class PreViewViewModel : BindableBase, INavigatedAware
    {

        IEventAggregator _eventAggregator;

        private string currentStatus = "";
        private string status = "";
        private string _cpuTemperature;
        private string _gpuTemerature;
        private string _workTime;
        private int position;
        private bool _canControl = true;

        private ObservableCollection<PreViewModel> carouselData = new ObservableCollection<PreViewModel>();
        private StatusDataModel _statusData = new StatusDataModel { Id = "", PCType = "", Status = "" };
        private ClientDataModel _clientData = new ClientDataModel { Id = "", Batary = 0, CpuTemperature = 0, DateMonth = "", DateNumber = 0, DateYear = 0, Day = "", GpuTemperature = 0, Time = "", WorktimeDay = 0, WorktimeHour = 0, WorktimeMinute = 0, WorktimeSecond = 0 };

        private string _id { get; set; }
        public bool CanControl
        {
            get { return _canControl; }
            set { SetProperty(ref _canControl, value); }
        }
        public StatusDataModel StatusData
        {
            get { return _statusData; }
            set { SetProperty(ref _statusData, value); }
        }
        public ClientDataModel ClientData
        {
            get { return _clientData; }
            set { SetProperty(ref _clientData, value); }
        }
        public string CurrrentStatus
        {
            get { return currentStatus; }
            set { SetProperty(ref currentStatus, value); }
        }
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }
        public string CPUTemperature
        {
            get { return _cpuTemperature; }
            set { SetProperty(ref _cpuTemperature, value); }
        }
        public string GPUTemperature
        {
            get { return _gpuTemerature; }
            set { SetProperty(ref _gpuTemerature, value); }
        }
        public string WorkTime
        {
            get { return _workTime; }
            set { SetProperty(ref _workTime, value); }
        }
        public int Position
        {
            get { return position; } 
            set 
            {
                SetProperty(ref position, value);
                switch (Position)
                {
                    case 0:
                        if (CurrrentStatus == "active")
                            Status = AppResources.Working_now;
                        else if (CurrrentStatus == "")
                            Status = "";
                        else
                            Status = AppResources.Not_Working;
                        break;
                    case 1:
                        if (CurrrentStatus == "shutdown")
                            Status = AppResources.Shutdown_Now;
                        else if (CurrrentStatus == "")
                            Status = "";
                        else
                            Status = AppResources.Click_to_shutdown;
                        break;
                    case 2:
                        if (CurrrentStatus == "reset")
                            Status = AppResources.Reset_Now;
                        else if (CurrrentStatus == "")
                            Status = "";
                        else
                            Status = AppResources.Click_to_reset;
                        break;
                    case 3:
                        //if (Position == 0)
                        //    Status = "Hello";
                        if (CurrrentStatus == "sleep")
                            Status = AppResources.Sleep_Now;
                        else if (CurrrentStatus == "")
                            Status = "";
                        else
                            Status = AppResources.Clisck_to_sleep;
                        break;
                }
            }
        }
        public ObservableCollection<PreViewModel> CarouselData
        {
            get { return carouselData; }
            set { SetProperty(ref carouselData, value); }
        }


        CancellationTokenSource _cts = new CancellationTokenSource();
        private StatusDataService _statusDataService = new StatusDataService();
        private ClientDataService _clientDataService = new ClientDataService();

        public PreViewViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            ea.GetEvent<IStringEvent>().Subscribe(GetId);
        }

        private void GetId(string id)
        {
            _id = id;
            GetStatusData();
            Thread DataThread = new Thread(GetData);
            DataThread.Name = "DataThread";
            DataThread.Start();
        }

        private async void GetData()
        {
            do
            {
                StatusData = await _statusDataService.Get(_id);
                ClientData = await _clientDataService.Get(_id);
                GetCurrentStatus(StatusData);
                GetClientData(ClientData);
                Thread.Sleep(5000);
            }
            while (!_cts.IsCancellationRequested);
        }

        private void GetCurrentStatus(StatusDataModel statusData)
        {
            switch (statusData.Status)
            {
                case "active":
                    Status = AppResources.Working_now;
                    CurrrentStatus = statusData.Status;
                    CanControl = true;
                    Position = 0;
                    break;
                case "shutdown":
                    Status = AppResources.Shutdown_Now;
                    CurrrentStatus = statusData.Status;
                    CanControl = false;
                    Position = 1;
                    break;
                case "reset":
                    Status = AppResources.Reset_Now;
                    CurrrentStatus = statusData.Status;
                    CanControl = false;
                    Position = 2;
                    break;
                case "sleep":
                    Status = AppResources.Sleep_Now;
                    CurrrentStatus = statusData.Status;
                    CanControl = false;
                    Position = 3;
                    break;
            }

        }

        private void GetClientData(ClientDataModel data)
        {
            CPUTemperature = data.CpuTemperature.ToString();
            GPUTemperature = data.GpuTemperature.ToString();
            WorkTime = data.WorktimeDay.ToString() + " days " +
                       data.WorktimeHour.ToString() + " hours " +
                       data.WorktimeMinute.ToString() + " minutes " +
                       data.WorktimeSecond.ToString() + " seconds ";
        }

        private async void GetStatusData()
        {
            StatusDataModel data =  await _statusDataService.Get(_id);
            CurrrentStatus = data.Status;
            switch (data.PCType)
            {
                case "laptop":

                    CarouselData.Add(new PreViewModel() { Type = "laptopwork_white.svg", SelectCommand = new DelegateCommand(Select) });
                    CarouselData.Add(new PreViewModel() { Type = "laptoppoweroff_white.svg", SelectCommand = new DelegateCommand(Shutdown) });
                    CarouselData.Add(new PreViewModel() { Type = "laptoprestart_white.svg", SelectCommand = new DelegateCommand(Restart) });
                    CarouselData.Add(new PreViewModel() { Type = "laptopsleep_white.svg", SelectCommand = new DelegateCommand(Sleep) });
                    break;
                case "pc":
                    CarouselData.Add(new PreViewModel() { Type = "desktopwork_white.svg", SelectCommand = new DelegateCommand(Select) });
                    CarouselData.Add(new PreViewModel() { Type = "desktoppoweroff_white.svg", SelectCommand = new DelegateCommand(Shutdown) });
                    CarouselData.Add(new PreViewModel() { Type = "desktoprestart_white.svg", SelectCommand = new DelegateCommand(Restart) });
                    CarouselData.Add(new PreViewModel() { Type = "desktopsleep_white.svg", SelectCommand = new DelegateCommand(Sleep) });
                    break;
            }
            switch (CurrrentStatus)
            {
                case "active":
                    Position = 0;
                    break;
                case "shutdown":
                    Position = 1;
                    break;
                case "reset":
                    Position = 2;
                    break;
                case "sleep":
                    Position = 3;
                    break;
            }
        }

        private async void Shutdown()
        {
            CurrrentStatus = "shutdown";
            Status = AppResources.Shutdown_Now;
            var data = StatusData;
            data.Status = CurrrentStatus;
            await _statusDataService.Put(_id, data);
        }

        private async void Restart()
        {
            CurrrentStatus = "reset";
            Status = AppResources.Reset_Now;
            var data = StatusData;
            data.Status = CurrrentStatus;
            await _statusDataService.Put(_id, data);
        }

        private async void Sleep()
        {
            CurrrentStatus = "sleep";
            Status = AppResources.Sleep_Now;
            var data = StatusData;
            data.Status = CurrrentStatus;
            await _statusDataService.Put(_id, data);
        }

        private void Select()
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            _cts.Cancel();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
