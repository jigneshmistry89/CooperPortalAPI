using System.Collections.Generic;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class AccountDetails
    {
        public List<Account> AccountData { get; set; }

        public int NumGateways { get; set; }

        public int NumSensors { get; set; }

    }
}
