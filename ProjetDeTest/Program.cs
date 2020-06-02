using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jp_Tools;

namespace ProjetDeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Jp_Tools.AdjectifDll adj = new Jp_Tools.AdjectifDll()
            {
                furigana = "おいしい",
                kanji = "美味しい",
                romaji = "oishii",
                type = "I"
            };
            var o = JapaneseTools.ConjugAdjectif(adj, Jp_Tools.JapaneseTools.E_TmpsAdj.Provisional, false, true);
            Console.WriteLine(o);
        }
    }
}
