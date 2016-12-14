using System;
using System.CodeDom;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CodingExcerise
{
    class Program
    {
        static void Main(string[] args)
        {
            var cards = new CardDeck();

            $"--------before shuffle-------------------".Dump();
            cards.Dump();

            $"--------Shuffle-------------------".Dump();
            cards.Shuffle();

            $"--------after shuffle-------------------".Dump();
            cards.Dump();

            cards.Sort();
            $"-----------after sort ----------------".Dump();
            cards.Dump();

            
            Console.ReadKey();
        }
    }
}
