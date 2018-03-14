using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coopers.BusinessLayer.Database.Domain.Models
{
    [Table("Account")]
    public partial class Account
    {
        [Key]
        public long AccountID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
