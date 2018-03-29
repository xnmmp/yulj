using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonSha
{
    public class encrypt
    {
        public static string Sha256(string PPWd) {
            SHA256Managed shm = new SHA256Managed();
            byte[] ciptext = shm.ComputeHash(Encoding.Default.GetBytes(PPWd));
            return Convert.ToBase64String(ciptext);

        }
    }
}
