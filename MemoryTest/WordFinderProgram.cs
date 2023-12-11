using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest
{
    public static class WordFinderProgram
    {
        public static void Run()
        {
            string[] words = File.ReadAllLines("swe_wordlist.txt");
            List<string> a = words.Where(x => char.ToLower(x[0]) is 'm' && char.ToLower(x.GetConsonant(1)) is 'f').ToList();
            a.ForEach(x => Console.WriteLine(x));
            Console.ReadLine();
        }
    }
}
