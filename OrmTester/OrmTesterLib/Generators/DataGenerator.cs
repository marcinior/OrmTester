using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmTesterLib.Generators
{
    public class DataGenerator
    {
        public static string GenerateRandomString()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }

        public static string GeneratePesel()
        {
            var pesel = string.Empty;
            var randomGenerator = new Random();
            for(var i = 0; i<11; i++)
            {
                pesel += randomGenerator.Next(0,9).ToString();
            }
            return pesel;
        }
    }
}
