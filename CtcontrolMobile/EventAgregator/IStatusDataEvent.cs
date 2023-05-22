using CtcontrolAPIService.Models;
using Prism.Events;

namespace CtcontrolMobile.EventAgregator
{
    class IStatusDataEvent : PubSubEvent<StatusDataModel>{}
}
