using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Coopers.BusinessLayer.Database.Domain.Models
{
    [Table("Location")]
    public partial class Location
    {
        public long ID { get; set; }

        public string ProfileName { get; set; }

        public long AccountID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Title { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public virtual ICollection<LocationNetwork> LocationNetworks { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        [XmlIgnore]
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
    }
}
