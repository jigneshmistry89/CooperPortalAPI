using System.Text;

namespace Coopers.BusinessLayer.Utility
{
    public class AppUtility
    {

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string PrepareAddress(string Address,string Address2,string State, string PostalCode, string Country)
        {
            string addr = "";

            addr =  !string.IsNullOrEmpty(Address) ? Address : "";

            addr += !string.IsNullOrEmpty(Address2) ? " " + Address2 : "";

            addr += !string.IsNullOrEmpty(State) ? " " + State : "";

            addr += !string.IsNullOrEmpty(PostalCode) ? " " + PostalCode : "";

            addr += !string.IsNullOrEmpty(Country) ? " " + Country : "";

            return addr;
        }

        public static void SpitNameIntoFNameAndLName(string Name,ref string FName,ref string LName)
        {
            var names = Name.Split(' ');

            if(names.Length > 0)
            {
                FName = names[0].Trim();
            }

            if(names.Length > 1)
            {
                if (names.Length == 3 && names[1] == " ")
                {
                    LName = names[2].Trim();
                }
                else
                {
                    LName = names[1].Trim();
                }
            }
        }

    }
}
