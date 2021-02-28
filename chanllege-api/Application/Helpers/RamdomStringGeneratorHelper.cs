using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Helpers
{
    public class RamdomStringGeneratorHelper : IRandomStringGenerator
    {
        private static Random _random;

        public RamdomStringGeneratorHelper()
        {
            _random = new Random();
        }

        public string RamdomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}