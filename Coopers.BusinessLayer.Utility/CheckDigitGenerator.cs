using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Utility
{
    public class CheckDigitGenerator
    {
        public static bool IsSensorIDNumeric(string sensorID)
        {
            int result;
            return int.TryParse(sensorID.Replace("-", ""), out result);
        }
        public static string FormatSensorID(string sensorID)
        {
            string sIDRet = "";
            if (IsSensorIDNumeric(sensorID))
            {
                string[] sID = sensorID.Split('-');
                for (int i = 0; i < sID.Length; i++)
                {
                    string sIDPart = sID[i];
                    if (sIDPart.Length < 3)
                    {
                        sID[i] = sIDPart.PadLeft(3, '0');
                    }
                    sIDRet += sID[i];
                }
            }
            else
            {
                sIDRet = "Invalid SensorID";
            }
            return sIDRet;
        }
        public static bool ValidateSensorID(string sensorID, string securityCode)
        {
            sensorID = sensorID.ToUpper();
            if (sensorID.Length + securityCode.Length < 7)
            {
                return false;
            }
            string sensorIDNoSecurity = sensorID.Substring(sensorID.Length - 6, 6);

            double fid = 0;
            string sensorSuffix = "";

            string[] sensorID_Array = stringToStringArray(sensorIDNoSecurity);



            if (sensorIDNoSecurity.Length == 16)
            {
                FixHex(ref sensorID_Array, 16);
            }
            else if (sensorIDNoSecurity.Length == 8)
            {
                FixHex(ref sensorID_Array, 8);
            }
            else if (sensorIDNoSecurity.Length == 6)
            {
                FixHex(ref sensorID_Array, 6);
            }

            fid = ValidateSensorIDStepOne(sensorID_Array);
            sensorSuffix = ValidateSensorIDStepTwo(fid, sensorID_Array);

            return String.Equals(securityCode, sensorSuffix);

        }

        public static string GenerateSecurityCode(string sensorID)
        {
            sensorID = sensorID.ToUpper();
            if (sensorID.Length < 6)
                return "";
            string sensorIDNoSecurity = sensorID.Substring(sensorID.Length - 6, 6);

            double fid = 0;
            string sensorSuffix = "";

            string[] sensorID_Array = stringToStringArray(sensorIDNoSecurity);



            if (sensorIDNoSecurity.Length == 16)
            {
                FixHex(ref sensorID_Array, 16);
            }
            else if (sensorIDNoSecurity.Length == 8)
            {
                FixHex(ref sensorID_Array, 8);
            }
            else if (sensorIDNoSecurity.Length == 6)
            {
                FixHex(ref sensorID_Array, 6);
            }

            fid = ValidateSensorIDStepOne(sensorID_Array);
            sensorSuffix = ValidateSensorIDStepTwo(fid, sensorID_Array);

            return sensorSuffix;
        }

        private static double ValidateSensorIDStepOne(string[] fid_Array)
        {
            //string output = "";
            string calc_on_fid_str = null;
            int calc_on_fid_orig = 0;
            double fid_calc = 0;
            double fid_holder = 0;
            double fid_temp = 0;

            for (int count = 0; count <= fid_Array.Length - 1; count++)
            {
                if (count % 2 == 0)
                {
                    calc_on_fid_str = fid_Array[count];
                    calc_on_fid_orig = Convert.ToInt32(calc_on_fid_str) + 7;

                    if (calc_on_fid_orig > 15)
                    {
                        calc_on_fid_orig = 25;
                    }
                    fid_temp = calc_on_fid_orig;
                }
                else
                {
                    calc_on_fid_str = fid_Array[count];
                    calc_on_fid_orig = (Convert.ToInt32(calc_on_fid_str) + 1);
                    if (calc_on_fid_orig > 15)
                    {
                        calc_on_fid_orig = 10;
                    }
                    fid_temp = calc_on_fid_orig;
                }

                if (count == 0)
                {
                    fid_holder = fid_temp;
                }

                if (count == 1)
                {
                    fid_calc = Convert.ToInt32(fid_temp) + Convert.ToInt32(fid_holder);
                }
                else
                {
                    if (count % 2 == 0 && count < 11)
                    {
                        fid_calc = fid_calc + Convert.ToInt32(fid_temp);
                    }
                    else
                    {
                        fid_calc = (fid_calc - Convert.ToInt32(fid_temp)) * 9;
                    }
                }
            }

            return fid_calc;
        }

        private static string ValidateSensorIDStepTwo(double fid_calc, string[] fid_Array)
        {
            double divisor_fid = 0;
            double countq = 0;
            double fid_calc_temp = 0;
            double fid_calc2 = 0;
            string fid_calc_str = "";
            string fid_calc_str1 = null;
            string fid_calc_str2 = null;
            string fid_calc_str3 = null;
            string fid_calc_str4 = null;
            int startPosition = 0;
            if (fid_Array.Length == 16)
            {
                startPosition = 2;
            }
            else if (fid_Array.Length == 8 || fid_Array.Length == 6)
            {
                startPosition = 3;
            }
            else
            {
                // this is an error
                return string.Empty;
            }
            divisor_fid = ((fid_calc) + 9) * 515;
            countq = ((divisor_fid) - 31);
            fid_calc = countq * 517;
            fid_calc_temp = ((((fid_calc / 3) * 1024) * 2) - 23);
            fid_calc = fid_calc_temp;

            if (fid_calc.ToString().Length < 10)
            {
                fid_calc2 = fid_calc * 1009;
                fid_calc_str = fid_calc2.ToString();
                fid_calc_str1 = fid_calc_str.Substring(startPosition, 1);
                fid_calc_str2 = fid_calc_str.Substring(startPosition + 2, 2);
                fid_calc_str3 = fid_calc_str.Substring(startPosition + 4, 2);
                fid_calc_str4 = fid_calc_str.Substring(startPosition + 6, 1);
                fid_calc_str = fid_calc_str1 + fid_calc_str2 + fid_calc_str3 + fid_calc_str4;
            }

            if (fid_calc.ToString().Length > 10)
            {
                fid_calc2 = fid_calc * 812;
                fid_calc_str = fid_calc2.ToString();
                fid_calc_str1 = fid_calc_str.Substring(startPosition, 1);
                fid_calc_str2 = fid_calc_str.Substring(startPosition + 2, 2);
                fid_calc_str3 = fid_calc_str.Substring(startPosition + 4, 2);
                fid_calc_str4 = fid_calc_str.Substring(startPosition + 6, 1);
                fid_calc_str = fid_calc_str1 + fid_calc_str2 + fid_calc_str3 + fid_calc_str4;
            }
            return fid_calc_str;
        }

        public static String[] stringToStringArray(String str)
        {
            String[] result = new String[str.Length];
            char[] charArr = str.ToCharArray();

            for (int i = 0; i <= charArr.Length - 1; i++)
            {
                // may need to set it to a string
                result[i] = charArr[i].ToString().ToUpper();
            }

            return result;
        }

        private static void FixHex(ref string[] fid_Array, int length)
        {
            for (int count = 0; count <= length - 1; count++)
            {
                if (fid_Array[count] == ("A"))
                {
                    fid_Array[count] = "10";
                }

                if (fid_Array[count] == ("B"))
                {
                    fid_Array[count] = "11";
                }

                if (fid_Array[count] == ("C"))
                {
                    fid_Array[count] = "12";
                }

                if (fid_Array[count] == ("D"))
                {
                    fid_Array[count] = "13";
                }

                if (fid_Array[count] == ("E"))
                {
                    fid_Array[count] = "14";
                }

                if (fid_Array[count] == ("F"))
                {
                    fid_Array[count] = "15";
                }
            }

        }
    }

}
