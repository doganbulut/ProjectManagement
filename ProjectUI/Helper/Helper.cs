using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectUI
{
    public class Helper
    {
        public static string GetPermission(int? PermID)
        {
            switch (PermID)
            {
                case 0: return "Yönetici";
                case 1: return "Alt Yönetici";
                case 2: return "Kullanıcı";
                default: return "Tanımsız";
            }
        }

        public static string GetProjectState(int Yuzde)
        {
            if (Yuzde == 0 )
                return "Planlandı";
            else if (Yuzde > 0 && Yuzde <= 99)
                return "Devam Ediyor";
            else if (Yuzde == 100)
                return "Tamamlandı";
            else
                return "Tanımsız";
        }

        public static int GetProjectStateID(int Yuzde)
        {
            if (Yuzde == 0)
                return 0;
            else if (Yuzde > 0 && Yuzde <= 99)
                return 1;
            else if (Yuzde == 100)
                return 2;
            else
                return -1;
        }


        public static Dictionary<string, string> GetCurrency()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("USD", "USD");
            result.Add("EUR", "EUR");
            result.Add("TL", "TL");
            return result;
        }


        public static Dictionary<string, string> GetInternalProject()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("USD", "USD");
            result.Add("EUR", "EUR");
            result.Add("TL", "TL");
            return result;
        }

    }
}