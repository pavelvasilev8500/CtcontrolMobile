using MongoDB.Bson;
using Prism.Commands;
using Realms;
using Xamarin.Forms;

namespace CtcontrolMobile.Models
{
    public class HistoryDataModel : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [MapTo("pctype")]
        public string PcType { get; set; }

        [MapTo("pcname")]
        public string PcName { get; set; }

        public Color color { get; set; } = Color.White;

        public DelegateCommand<string> SelectLastCommand { get; set; }
    }
}
