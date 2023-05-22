using CtcontrolAPIService.Services;
using Prism.Events;

namespace CtcontrolMobile.EventAgregator
{
   class IClientDataEvent : PubSubEvent<ClientDataModel>{}
}
