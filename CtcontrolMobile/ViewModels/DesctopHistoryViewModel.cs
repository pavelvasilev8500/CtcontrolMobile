using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.MasterPage;
using CtcontrolMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using System.Collections.ObjectModel;
using System.Linq;

namespace CtcontrolMobile.ViewModels
{
    public class DesctopHistoryViewModel : BindableBase
    {
        INavigationService _navigationService { get; }

        Realm _realm;
        private string _clear = AppResources.Clear_History;
        private ObservableCollection<HistoryDataModel> _historyDataModel = new ObservableCollection<HistoryDataModel>();

        public string Clear
        {
            get { return _clear; }
            set { SetProperty(ref _clear, value); }
        }
        public ObservableCollection<HistoryDataModel> HistoryData
        {
            get { return _historyDataModel; }
            set { SetProperty(ref _historyDataModel, value); }
        }

        public DelegateCommand ClearHistoryCommand { get; private set; }

        public DesctopHistoryViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _realm = Realm.GetInstance();
            var list = _realm.All<HistoryDataModel>().Where(d => d.PcType == "desktop_white.svg" || d.PcType == "desktop_red.svg").ToList();
            for (int i = 0; i < list.Count; i++)
            {
                _realm.Write(() =>
                {
                    list[i].PcType = "desktop_white.svg";
                    list[i].SelectLastCommand = new DelegateCommand<string>(SelectLast);
                });
            }
            HistoryData = new ObservableCollection<HistoryDataModel>(list);
            ClearHistoryCommand = new DelegateCommand(ClearHistory);
        }

        private void ClearHistory()
        {
            _realm = Realm.GetInstance();
            _realm.Write(() =>
            {
                var laptops = _realm.All<HistoryDataModel>().Where(d => d.PcType == "desktop_white.svg" || d.PcType == "desktop_red.svg");
                _realm.RemoveRange(laptops);
            });
            HistoryData.Clear();
        }

        private async void SelectLast(string id)
        {
            var data = new NavigationParameters
            {
                { "Id", id },
                { "From", "Load" }
            };
            await _navigationService.NavigateAsync("MainDataView", data);
            MasterList.Update("MainDataView");
        }
    }
}
