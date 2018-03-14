using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Coopers.BusinessLayer.Database.Domain.Models
{
    [Table("LocationNetwork")]
    public partial class LocationNetwork
    {
        [Key]
        public long ID { get; set; }

        public long LocationID { get; set; }

        public long NetworkID { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        [XmlIgnore]
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

    }
}
