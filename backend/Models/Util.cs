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
        static string criptografar(string value)
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
    }
}