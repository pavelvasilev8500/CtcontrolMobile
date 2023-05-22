using CtcontrolAPIService.Services;
using CtcontrolMobile.EventAgregator;
using CtcontrolMobile.Service;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading;

namespace CtcontrolMobile.ViewModels
{
    class DataViewViewModel : BindableBase, INavigationAware
    {
        IEventAggregator _eventAggregator;

        private int _dateNimber;
        private string _dateMonth;
        private int _dateYear;
        private string _time;
        private string _workTimeDay;
        private string _workTimeHour;
        private string _workTimeMinut;
        private string _workTimeSecond;
        private string _day;
        private string _cpuTemperature; 
        private double _cpuLevel;
        private string _gpuTemperature;
        private double _gpuLevel;
        private string _battery;
        private double _batteryLevel;
        private bool _scroll = false;

        private string _id { get; set; }
        private ClientDataModel _clientData {  get; set; }
        public int DateNamber
        {
            get { return _dateNimber; }
            set { SetProperty(ref _dateNimber, value); }
        }
        public string DateMonth
        {
            get { return _dateMonth; }
            set { SetProperty(ref _dateMonth, value); }
        }
        public int DateYear
        {
            get { return _dateYear; }
            set { SetProperty(ref _dateYear, value); }
        }
        public string Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }
        public string WorkTimeDay
        {
            get { return _workTimeDay; }
            set { SetProperty(ref _workTimeDay, value); }
        }
        public string WorkTimeHour
        {
            get { return _workTimeHour; }
            set { SetProperty(ref _workTimeHour, value); }
        }
        public string WorkTimeMinut
        {
            get { return _workTimeMinut; }
            set { SetProperty(ref _workTimeMinut, value); }
        }
        public string WorkTimeSecond
        {
            get { return _workTimeSecond; }
            set { SetProperty(ref _workTimeSecond, value); }
        }
        public string Day
        {
            get { return _day; }
            set { SetProperty(ref _day, value); }
        }
        public string CPUTemperature
        {
            get { return _cpuTemperature; }
            set { SetProperty(ref _cpuTemperature, value); }
        }
        public double CPULevel
        {
            get { return _cpuLevel; }
            set { SetProperty(ref _cpuLevel, value); }
        }
        public string GPUTemperature
        {
            get { return _gpuTemperature; }
            set { SetProperty(ref _gpuTemperature, value); }
        }
        public double GPULevel
        {
            get { return _gpuLevel; }
            set { SetProperty(ref _gpuLevel, value); }
        }
        public string Battery
        {
            get { return _battery; }
            set { SetProperty(ref _battery, value); }
        }
        public double BatteryLevel
        {
            get { return _batteryLevel; }
            set { SetProperty(ref _batteryLevel, value); }
        }
        public bool Scroll
        {
            get { return _scroll; }
            set { SetProperty(ref _scroll, value); }
        }

        private ClientDataService _clientDataService = new ClientDataService();
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public DataViewViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            ea.GetEvent<IStringEvent>().Subscribe(GetId);
        }

        private void GetId(string id)
        {
            _id = id;
            Thread ClientDataThread = new Thread(GetClientData);
            ClientDataThread.Name = "ClientDataThread";
            ClientDataThread.Start();
        }

        private async void GetClientData()
        {
            do
            {
                _clientData = await _clientDataService.Get(_id);
                GetData(_clientData);
                Thread.Sleep(5000);
            }
            while(!_cts.IsCancellationRequested);
        }

        private void GetData(ClientDataModel data)
        {
            CPUTemperature = data.CpuTemperature.ToString();
            GPUTemperature = data.GpuTemperature.ToString();
            if (data.Batary != null)
            {
                Scroll = true;
                Battery = data.Batary.ToString() + " %";
                BattaryChargeLevel();
            }
            DateNamber = data.DateNumber;
            DateMonth = data.DateMonth;
            DateYear = data.DateYear;
            Time = data.Time;
            Day = data.Day;
            WorkTimeDay = data.WorktimeDay.ToString() + " days";
            WorkTimeHour = data.WorktimeHour.ToString() + " hours";
            WorkTimeMinut = data.WorktimeMinute.ToString() + " minuts";
            WorkTimeSecond = data.WorktimeSecond.ToString() + " seconds";
            CpuLevel();
            GpuLevel();
        }

        private void CpuLevel()
        {
            CPULevel = int.Parse(CPUTemperature) * 1.9;
        }

        private void GpuLevel()
        {
            GPULevel = int.Parse(GPUTemperature) * 1.9;
        }

        private void BattaryChargeLevel()
        {
            BatteryLevel = int.Parse(Battery.Trim(new char[] {' ', '%'})) * 1.9;
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
