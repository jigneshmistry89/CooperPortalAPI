using System;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.Model.DTO
{

    public class UserPermission
    {
        public long CustomerID { get; set; }

        public List<int> PermissionList { get; set; }

        public List<int> NetworkList { get; set; }

    }

}
