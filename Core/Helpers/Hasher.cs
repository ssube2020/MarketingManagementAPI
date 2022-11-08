using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class Hasher
    {
        public static string GetHash()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string GetRandomCode()
        {
            var allChar = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(allChar, 8).Select(token => token[random.Next(token.Length)]).ToArray()).ToString();
        }
    }
}
