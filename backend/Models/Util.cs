using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace backend.Models
{
    public class Util
    {
        // Configuração de paginação - Itens por página
        public const int ITENS_POR_PAGINA = 5;

        public static string criptografar(string value)
        {
            var UE = new UnicodeEncoding();
            byte[] HashValue, MessagesBytes = UE.GetBytes(value);
            var SHhash = new SHA256Managed();
            string strhex = "";

            HashValue = SHhash.ComputeHash(MessagesBytes);
            foreach (byte b in HashValue)
            {
                strhex += String.Format("{0:x2}", b);
            }
            return strhex;
        }

        public static string dateAgo(DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "Agora" : ts.Seconds + " segundos atrás";
            }
            if (delta < 2 * MINUTE)
            {
                return "Um minuto atrás";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " minutos atrás";
            }
            if (delta < 90 * MINUTE)
            {
                return "Uma hora atrás";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " horas atrás";
            }
            if (delta < 48 * HOUR)
            {
                return "Ontem";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " dias atrás";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "Um mês atrás" : months + " meses atrás";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "Um ano atrás" : years + " anos atrás";
            }

        }
    }
}