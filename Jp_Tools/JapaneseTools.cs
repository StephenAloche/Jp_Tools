using HiraKana;
using System;
using System.Collections.Generic;
using System.Linq;
using static Jp_Tools.JapaneseTools;

namespace Jp_Tools
{
    public static class JapaneseTools
    {
        //private JpDB db { get; } = new JpDB(Properties.Settings.Default.GCG);

        #region adjectif
        public static JpString ConjugAdjectif(AdjectifDll adj, E_TmpsAdj temps, bool isPolite, bool isNegatif)
        {
            JpString adjConjug = (JpString)adj;
            bool isIAdj = adj.type == "I";
            string terminaison = "";
            string newTerminaison = "";

            if (isIAdj)
            {
                terminaison = adj.furigana.Last().ToString();
            }

            switch (temps)
            {
                case E_TmpsAdj.Present:
                    newTerminaison = ToPresentAdj(terminaison, isIAdj, isPolite, isNegatif);
                    break;
                case E_TmpsAdj.Past:
                    newTerminaison = ToPastAdj(terminaison, isIAdj, isPolite, isNegatif);
                    break;
                case E_TmpsAdj.NegatifFormel:
                    newTerminaison = ToNegatifFormel(terminaison, isIAdj, false);
                    break;
                case E_TmpsAdj.Epithete:
                    newTerminaison = ToEpithete(terminaison, isIAdj, isPolite, isNegatif);
                    break;
                case E_TmpsAdj.Adverbial:
                    newTerminaison = ToAdverbe(terminaison, isIAdj, isPolite, isNegatif);
                    break;
                case E_TmpsAdj.Conditionnel: //TODO conjugaison manquante
                    if (!isIAdj)
                    {
                        newTerminaison = ToConditional(terminaison, isIAdj, isPolite, isNegatif);
                    }
                    break;
                case E_TmpsAdj.Provisional: //TODO conjugaison manquante
                    if (!isIAdj)
                    {
                        newTerminaison = ToProvisional(terminaison, isIAdj, isPolite, isNegatif);
                    }
                    break;
                default:
                    break;
            }


            if (isIAdj)
            {
                adjConjug.kanji = adjConjug.kanji.Remove(adjConjug.kanji.Count()-1,1) + newTerminaison;// Replace(adjConjug.kanji.Last().ToString(), newTerminaison);
                adjConjug.kana = adjConjug.kana.Remove(adjConjug.kana.Count() - 1, 1) + newTerminaison;//.Replace(adjConjug.kana.Last().ToString(), newTerminaison);


                adjConjug.romaji = KanaTools.ToRomaji(adjConjug.kana);
            }
            else
            {
                adjConjug.kanji += newTerminaison;
                adjConjug.kana += newTerminaison;

                adjConjug.romaji = KanaTools.ToRomaji(adjConjug.kana);

            }

            return adjConjug;
        }

        private static string ToPresentAdj(string terminaison, bool isIAdj, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (!isPolite && !isNegatif)
            {
                if (isIAdj)
                {
                    return terminaison;
                }
                else
                {
                    newTerminaison = terminaison + "だ";
                }
            }
            else if (isPolite && !isNegatif)
            {
                newTerminaison = terminaison + "です";
            }
            else if (!isPolite && isNegatif)
            {
                if (isIAdj)
                {
                    newTerminaison = terminaison.Replace("い", "くない");
                }
                else
                {
                    newTerminaison = terminaison + "じゃない";
                }
            }
            else if (isPolite && isNegatif)
            {
                if (isIAdj)
                {
                    newTerminaison = terminaison.Replace("い", "くありません");
                }
                else
                {
                    newTerminaison = terminaison + "ではありません";
                }
            }
            return newTerminaison;
        }
        private static string ToPastAdj(string terminaison, bool isIAdj, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (!isPolite && !isNegatif)
            {
                if (isIAdj)
                {
                    newTerminaison = terminaison.Replace("い", "かった");
                }
                else
                {
                    newTerminaison = terminaison + "だった";
                }
            }
            else if (isPolite && !isNegatif)
            {
                if (isIAdj)
                {
                    newTerminaison = terminaison.Replace("い", "かったです");
                }
                else
                {
                    newTerminaison = terminaison + "でした";
                }
            }
            else if (!isPolite && isNegatif)
            {
                if (isIAdj)
                {
                    newTerminaison = terminaison.Replace("い", "くなかった");
                }
                else
                {
                    newTerminaison = terminaison + "じゃなかた";
                }
            }
            else if (isPolite && isNegatif)
            {
                if (isIAdj)
                {
                    newTerminaison = terminaison.Replace("い", "くありませんでした");
                }
                else
                {
                    newTerminaison = terminaison + "ではありませんでした";
                }
            }
            return newTerminaison;
        }
        private static string ToNegatifFormel(string terminaison, bool isIAdj, bool isPresent)
        {
            string newTerminaison = "";
            if (isPresent)
            {
                if (isIAdj)
                {
                    newTerminaison = terminaison.Replace("い", "くないです");
                }
                else
                {
                    newTerminaison = terminaison + "じゃないです";
                }
            }
            else
            {
                if (isIAdj)
                {
                    newTerminaison = terminaison.Replace("い", "くなかったです");
                }
                else
                {
                    newTerminaison = terminaison + "じゃなかったです";
                }
            }
            return newTerminaison;
        }
        private static string ToEpithete(string terminaison, bool isIAdj, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (isIAdj)
            {
                newTerminaison = terminaison;
            }
            else
            {
                newTerminaison = terminaison + "な";
            }
            return newTerminaison;
        }
        private static string ToAdverbe(string terminaison, bool isIAdj, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (isIAdj)
            {
                newTerminaison = terminaison.Replace("い", "く");
            }
            else
            {
                newTerminaison = terminaison + "な";
            }
            return newTerminaison;
        }
        private static string ToConditional(string terminaison, bool isIAdj, bool isPolite, bool isNegatif)
        {
            string newTerminaison = terminaison.Replace("い", "かったら");
            return newTerminaison;
        }
        private static string ToProvisional(string terminaison, bool isIAdj, bool isPolite, bool isNegatif)
        {
            string newTerminaison = terminaison.Replace("い", "ければ");
            return newTerminaison;
        }

        public enum E_TmpsAdj
        {
            Present = 1,
            Past = 2,
            NegatifFormel = 3,
            Epithete = 4,
            Adverbial = 5,
            Conditionnel = 6,
            Provisional = 7,
        }
        #endregion

        #region Verbes
        public static JpString ConjugVerb(this VerbeDll verbe, E_TmpsVerb temps, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string newTerminaison = "";
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            switch (temps)
            {
                case E_TmpsVerb.Present:
                    newTerminaison = ToPresent(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Past:
                    newTerminaison = ToPast(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.JustPolite:
                    newTerminaison = ToJustPolite(terminaison, isIchidan, isNegatif);
                    break;
                case E_TmpsVerb.JustNegatif:
                    newTerminaison = ToJustNegatif(terminaison, isIchidan, isPolite);
                    break;
                case E_TmpsVerb.Te:
                    newTerminaison = ToTe(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Potential:
                    newTerminaison = ToPotential(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Imperatif:
                    newTerminaison = ToImperatif(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Causatif:
                    newTerminaison = ToCausatif(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Passif:
                    newTerminaison = ToPassif(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Causatif_Passif:
                    newTerminaison = ToCausatifPassif(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Volitional:
                    newTerminaison = ToVolitional(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Conditionnel_Provisionnel:
                    newTerminaison = ToConditionnel(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Conditionnel_Tara:
                    newTerminaison = ToConditionnelTara(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                case E_TmpsVerb.Progressif:
                    newTerminaison = ToProgressif(terminaison, isIchidan, isPolite, isNegatif); //te iru
                    break;
                case E_TmpsVerb.Request:
                    newTerminaison = ToRequest(terminaison, isIchidan, isPolite, isNegatif);
                    break;
                default:
                    break;
            }
            verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
            verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);


            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }

        private static JpString ToPresent(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan; //Godan // IKU // KURU  //TSURU

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.IKU || verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                if (isPolite || isNegatif) //si les 2 sont faux le verbe reste a l'infinitif
                {
                    (string newTerm, string newTermKanji) verbIrr = ToPresentIrr(verbe, isPolite, isNegatif);
                    verbConjug.kana = verbIrr.newTerm;
                    verbConjug.kanji = verbIrr.newTermKanji;
                }
            }
            else
            {
                newTerminaison = ToPresent(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }

            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToPresent(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (!isPolite && !isNegatif)
            {
                return terminaison;
            }
            else if (isPolite && !isNegatif)
            {
                newTerminaison = ToJustPolite(terminaison, isIchidan, false);
            }
            else if (!isPolite && isNegatif)
            {
                newTerminaison = ToJustNegatif(terminaison, isIchidan, false);
            }
            else if (isPolite && isNegatif)
            {
                newTerminaison = ToPoliteNegatif(terminaison, isIchidan);
            }
            return newTerminaison;
        }
        private static (string, string) ToPresentIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            //TODO gerer : iku,  irassharu, aru
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.IKU:
                    break;
                case E_TypeVerb.KURU:
                    if (isPolite && !isNegatif)
                    {
                        newTerm = "きます";
                        newTermKanji = "来ます";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "こない";
                        newTermKanji = "来ない";
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = "きません";
                        newTermKanji = "来ません";
                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "します");
                        newTermKanji = verbe.furigana.Replace("する", "します");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しない");
                        newTermKanji = verbe.furigana.Replace("する", "しない");
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しません");
                        newTermKanji = verbe.furigana.Replace("する", "しません");
                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static JpString ToPast(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.IKU || verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToPastIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                newTerminaison = ToPast(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }

            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToPast(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";


            if (!isPolite && !isNegatif)
            {
                if (isIchidan)
                {
                    newTerminaison = terminaison.Replace("る", "た");
                }
                else
                {
                    if (terminaison.EndsWith("す"))
                    {
                        newTerminaison = terminaison.Replace("す", "した");
                    }
                    if (terminaison.EndsWith("く"))
                    {
                        newTerminaison = terminaison.Replace("く", "いた");
                    }
                    if (terminaison.EndsWith("ぐ"))
                    {
                        newTerminaison = terminaison.Replace("ぐ", "いだ");
                    }
                    if (terminaison.EndsWith("む") || terminaison.EndsWith("ぬ") || terminaison.EndsWith("ぶ"))
                    {
                        newTerminaison = terminaison.Replace(terminaison.Last().ToString(), "んだ");
                    }
                    if (terminaison.EndsWith("る") || terminaison.EndsWith("つ") || terminaison.EndsWith("う"))
                    {
                        newTerminaison = terminaison.Replace(terminaison.Last().ToString(), "った");
                    }
                }
            }
            else if (isPolite && !isNegatif)
            {
                if (isIchidan)
                {
                    newTerminaison = terminaison.Replace("る", "");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en I
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));
                }
                newTerminaison += "ました";
            }
            else if (!isPolite && isNegatif)
            {
                newTerminaison = ToJustNegatif(terminaison, isIchidan, false);
                newTerminaison = newTerminaison.Replace("い", "かった");
            }
            else if (isPolite && isNegatif)
            {
                if (isIchidan)
                {
                    newTerminaison = terminaison.Replace("る", "");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en I
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));
                }
                newTerminaison += "ませんでした";
            }
            return newTerminaison;
        }
        private static (string newTerm, string newTermKanji) ToPastIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            //TODO gerer : iku,  irassharu, aru
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.IKU:
                    break;
                case E_TypeVerb.KURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = "きた";
                        newTermKanji = "来た";
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = "きました";
                        newTermKanji = "来ました";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "こなかった";
                        newTermKanji = "来なかった";
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = "きませんでした";
                        newTermKanji = "来ませんでした";
                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "した");
                        newTermKanji = verbe.furigana.Replace("する", "した");
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しました");
                        newTermKanji = verbe.furigana.Replace("する", "しました");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しなかった");
                        newTermKanji = verbe.furigana.Replace("する", "しなかった");
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しませんでした");
                        newTermKanji = verbe.furigana.Replace("する", "しませんでした");
                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static JpString ToTe(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.IKU || verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToTeIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                newTerminaison = ToTe(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }

            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);
            return verbConjug;
        }
        private static string ToTe(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (!isPolite && !isNegatif)
            {
                newTerminaison = ToPast(terminaison, isIchidan, isPolite, isNegatif);

                if (newTerminaison.Last() == 'だ')
                {
                    newTerminaison = newTerminaison.Replace("だ", "で");
                }
                else
                {
                    newTerminaison = newTerminaison.Replace("た", "て");
                }
            }
            else if (isPolite && !isNegatif)
            {
                newTerminaison = ToPast(terminaison, isIchidan, isPolite, isNegatif);
                if (newTerminaison.Last() == 'だ')
                {
                    newTerminaison = newTerminaison.Replace("だ", "で");
                }
                else
                {
                    newTerminaison = newTerminaison.Replace("た", "て");
                }
            }
            else if (!isPolite && isNegatif)
            {
                //na ide 
                //newTerminaison = ToJustNegatif(terminaison, isIchidan, false);
                //newTerminaison += "で";

                //- nakutte
                newTerminaison = ToJustNegatif(terminaison, isIchidan, false);
                newTerminaison = newTerminaison.Replace("い", "くて");
            }
            else if (isPolite && isNegatif)
            {
                if (isIchidan)
                {
                    newTerminaison = terminaison.Replace("る", "");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en I
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));
                }
                newTerminaison += "ませんで";
            }
            return newTerminaison;

        }
        private static (string newTerm, string newTermKanji) ToTeIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            //TODO gerer : iku,  irassharu
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.IKU:
                    break;
                case E_TypeVerb.KURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = "きて";
                        newTermKanji = "来て";
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = "きまして";
                        newTermKanji = "来まして";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "こなくて";
                        newTermKanji = "来なくて";
                    }
                    else if (isPolite && isNegatif)
                    {
                        //TODO trouver la terminaison de ce temps
                        newTerm = "???";
                        newTermKanji = "???";

                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "して");
                        newTermKanji = verbe.furigana.Replace("する", "して");
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しまして");
                        newTermKanji = verbe.furigana.Replace("する", "しまして");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しなくて");
                        newTermKanji = verbe.furigana.Replace("する", "しなくて");
                    }
                    else if (isPolite && isNegatif)
                    {
                        //TODO trouver la terminaison de ce temps
                        newTerm = verbe.furigana.Replace("する", "???");
                        newTermKanji = verbe.furigana.Replace("する", "???");
                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static JpString ToProgressif(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.IKU || verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                //Todo a tester
                (string newTerm, string newTermKanji) verbIrr = ToTeIrr(verbe, isPolite, isNegatif);
                newTerminaison = ToPresent("いる", true, isPolite, isNegatif);
                verbConjug.kanji = verbIrr.newTerm += $" {newTerminaison}";
                verbConjug.kana = verbIrr.newTermKanji += $" {newTerminaison}";
            }
            else
            {
                newTerminaison = ToProgressif(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }


            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToProgressif(string terminaison, bool isIchidan, bool isPolite, bool isNegatif) //te iru
        {
            string newTerminaison = "";
            if (!isPolite && !isNegatif)
            {
                newTerminaison = ToPast(terminaison, isIchidan, isPolite, isNegatif);
            }
            else
            {
                newTerminaison = ToPast(terminaison, isIchidan, false, false);
            }

            if (newTerminaison.Last() == 'だ')
            {
                newTerminaison = newTerminaison.Replace("だ", "で");
            }
            else
            {
                newTerminaison = newTerminaison.Replace("た", "て");
            }
            newTerminaison += " いる";
            newTerminaison = ToPresent(newTerminaison, true, isPolite, isNegatif); //todo tester si bien te inai au lieu de naka te
            return newTerminaison;
        }


        private static JpString ToVolitional(this VerbeDll verbe, bool isPolite, bool isNegatif)//conjectural //volontaire
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToVolitionalIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                newTerminaison = ToVolitional(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }

            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToVolitional(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (!isPolite && !isNegatif)
            {
                if (isIchidan)
                {
                    //on remplace le ru par you
                    newTerminaison = terminaison.Replace("る", "よう");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en O et on ajoute U
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'o'));

                    newTerminaison += "う";
                }
            }
            else if (isPolite && !isNegatif)
            {
                if (isIchidan)
                {
                    //on remplace le ru par mashou
                    newTerminaison = terminaison.Replace("る", "ましょう");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en I et on ajoute mashou
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));

                    newTerminaison += "ましょう";
                }
            }
            else if (!isPolite && isNegatif)
            {
                if (isIchidan)
                {
                    //on enleve la terminaison
                    newTerminaison = terminaison.Replace("る", "");
                }
                else
                {
                    newTerminaison = terminaison;
                }
                newTerminaison += "まい";
            }
            else if (isPolite && isNegatif)
            {
                if (isIchidan)
                {
                    newTerminaison = ToPresent(newTerminaison, true, isPolite, false);
                }
                else
                {
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));
                    newTerminaison = ToPresent(newTerminaison, true, isPolite, false);
                }
                newTerminaison += "まい";
            }
            return newTerminaison;
        }
        private static (string newTerm, string newTermKanji) ToVolitionalIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.IKU:
                    break;
                case E_TypeVerb.KURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = "こよう";
                        newTermKanji = "来よう";
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = "きましょう";
                        newTermKanji = "来ましょう";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "???";
                        newTermKanji = "???";
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = "???";
                        newTermKanji = "???";
                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しよう");
                        newTermKanji = verbe.furigana.Replace("する", "しよう");
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しましょう");
                        newTermKanji = verbe.furigana.Replace("する", "しましょう");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "???");
                        newTermKanji = verbe.furigana.Replace("する", "???");
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "???");
                        newTermKanji = verbe.furigana.Replace("する", "???");
                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static JpString ToImperatif(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                //TODO trouver la vrai conjugaison
                (string newTerm, string newTermKanji) verbIrr = ToVolitionalIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                newTerminaison = ToImperatif(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }

            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToImperatif(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            //todo gerer l'exception kureru

            string newTerminaison = "";
            if (!isPolite && !isNegatif)
            {
                if (isIchidan)
                {
                    //on remplace le ru par ro
                    newTerminaison = terminaison.Replace("る", "ろ");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en E
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'e'));
                }
            }
            else if (isPolite && !isNegatif)
            {
                if (isIchidan)
                {
                    //on ajoute nasai
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en I
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));
                }
                newTerminaison += "なさい";
            }
            else if (!isPolite && isNegatif)
            {
                //on ajoute na
                newTerminaison = terminaison + "な";
            }
            else if (isPolite && isNegatif)
            {
                if (isIchidan)
                {
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en I
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));
                }
                newTerminaison += "なさるな";
            }
            return newTerminaison;
        }

        private static JpString ToRequest(this VerbeDll verbe, bool isPolite, bool isNegatif)//te kudasai
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;


            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToTeIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm += " ください";
                verbConjug.kanji = verbIrr.newTermKanji += " ください";
            }
            else
            {
                newTerminaison = ToRequest(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }


            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToRequest(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (!isNegatif)
            {
                newTerminaison = ToPast(terminaison, isIchidan, isPolite, isNegatif);
                if (newTerminaison.Last() == 'だ')
                {
                    newTerminaison = newTerminaison.Replace("だ", "で");
                }
                else
                {
                    newTerminaison = newTerminaison.Replace("た", "て");
                }
            }
            else if (isNegatif)
            {
                //- nakutte
                newTerminaison = ToJustNegatif(terminaison, isIchidan, false);
                newTerminaison += "で";
            }
            return newTerminaison += " ください";
        }

        //conditionnel aussi appelé provisional
        private static JpString ToConditionnel(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;


            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToConditionnelIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                newTerminaison = ToConditionnel(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }


            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToConditionnel(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";

            if (!isNegatif)
            {
                //on remplace la terminaison par sont equivalent en e
                newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'e'));
                //et on ajoute ba
                newTerminaison += "ば";
            }
            else if (isNegatif)
            {


                if (isIchidan)
                {
                    newTerminaison = ToJustNegatif(terminaison, isIchidan, false);
                    newTerminaison = newTerminaison.Replace("い", "ければ");
                }
                else
                {
                    //si u on remplace par wa
                    if (terminaison.Last() == 'う')
                    {
                        newTerminaison = terminaison.Replace("う", "わ");
                    }
                    else
                    {
                        //on remplace la terminaison par sont equivalent en A
                        newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'a'));
                    }
                    newTerminaison = ToJustNegatif(terminaison, isIchidan, false);
                    newTerminaison = newTerminaison.Replace("い", "ければ");
                }
            }

            return newTerminaison;
        }
        private static (string newTerm, string newTermKanji) ToConditionnelIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.IKU:
                    break;
                case E_TypeVerb.KURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = "くれば";
                        newTermKanji = "来れば";
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = "くれば";
                        newTermKanji = "来れば";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "こなければ";
                        newTermKanji = "来なければ";
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = "こなければ";
                        newTermKanji = "来なければ";
                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "すれば");
                        newTermKanji = verbe.furigana.Replace("する", "すれば");
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "すれば");
                        newTermKanji = verbe.furigana.Replace("する", "すれば");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しなければ");
                        newTermKanji = verbe.furigana.Replace("する", "しなければ");
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "しなければ");
                        newTermKanji = verbe.furigana.Replace("する", "しなければ");

                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static JpString ToConditionnelTara(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = ToConditionnelTara(terminaison, isIchidan, isPolite, isNegatif);

            verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
            verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);



            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToConditionnelTara(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = ToPast(terminaison, isIchidan, isPolite, isNegatif);
            return newTerminaison += "ら";
        }


        private static JpString ToPotential(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            if (verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToPotentialIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                string newTerminaison = ToPotential(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }

            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        /// <summary>
        /// Les Verbes a la forme potentiel devient des Verbes ichidan
        /// </summary>
        /// <param name="terminaison"></param>
        /// <param name="isIchidan"></param>
        /// <param name="isPolite"></param>
        /// <param name="isNegatif"></param>
        /// <returns></returns>
        private static string ToPotential(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";

            if (isIchidan)
            {
                //on remplace le ru par rareru
                newTerminaison = terminaison.Replace("る", "られる");
            }
            else
            {
                //on remplace la terminaison par sont equivalent en E et on ajoute ru
                newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'e'));
                newTerminaison += "る";
            }

            newTerminaison = ToPresent(newTerminaison, true, isPolite, isNegatif);

            return newTerminaison;
        }
        private static (string newTerm, string newTermKanji) ToPotentialIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            //todo gere la forme aru (se produire)
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.IKU:
                    break;
                case E_TypeVerb.KURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = "こられる";
                        newTermKanji = "来られる";
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = "こられます";
                        newTermKanji = "来られます";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "こられない";
                        newTermKanji = "来られない";
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = "こられません";
                        newTermKanji = "来られません";
                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "できる");
                        newTermKanji = verbe.furigana.Replace("する", "出来る");
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "できます");
                        newTermKanji = verbe.furigana.Replace("する", "出来ます");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "できない");
                        newTermKanji = verbe.furigana.Replace("する", "出来ない");
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "できません");
                        newTermKanji = verbe.furigana.Replace("する", "出来ません");

                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static JpString ToPassif(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToPassifIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                newTerminaison = ToPassif(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }

            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToPassif(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (isIchidan)
            {
                //on remplace le ru par rareru
                newTerminaison = terminaison.Replace("る", "られる");
            }
            else
            {
                //si u on remplace par wa
                if (terminaison.Last() == 'う')
                {
                    newTerminaison = terminaison.Replace("う", "わ");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en A et on ajoute reru
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'a'));
                }
                newTerminaison += "れる";
            }
            newTerminaison = ToPresent(newTerminaison, true, isPolite, isNegatif);
            return newTerminaison;
        }
        private static (string newTerm, string newTermKanji) ToPassifIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.KURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = "こられる";
                        newTermKanji = "来られる";
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = "こられます";
                        newTermKanji = "来られます";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "こられない";
                        newTermKanji = "来られない";
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = "こられません";
                        newTermKanji = "来られません";
                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "される");
                        newTermKanji = verbe.furigana.Replace("する", "される");
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "されます");
                        newTermKanji = verbe.furigana.Replace("する", "されます");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "されない");
                        newTermKanji = verbe.furigana.Replace("する", "されない");
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "されません");
                        newTermKanji = verbe.furigana.Replace("する", "されません");

                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static JpString ToCausatif(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToCausatifIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                newTerminaison = ToCausatif(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }


            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        /// <summary>
        /// Les Verbes a la forme potentiel deviennt des Verbes ichidan
        /// </summary>
        /// <param name="terminaison"></param>
        /// <param name="isIchidan"></param>
        /// <param name="isPolite"></param>
        /// <param name="isNegatif"></param>
        /// <returns></returns>
        private static string ToCausatif(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            string newTerminaison = "";
            if (isIchidan)
            {
                //on remplace le ru par saseru
                newTerminaison = terminaison.Replace("る", "させる");
            }
            else
            {
                //si u on remplace par wa
                if (terminaison.Last() == 'う')
                {
                    newTerminaison = terminaison.Replace("う", "わ");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en A et on ajoute seru
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'a'));
                }
                newTerminaison += "せる";
            }
            newTerminaison = ToPresent(newTerminaison, true, isPolite, isNegatif);
            return newTerminaison;
        }
        private static (string newTerm, string newTermKanji) ToCausatifIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.KURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = "こさせる";
                        newTermKanji = "来させる";
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = "こさせます";
                        newTermKanji = "来させます";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "こさせない";
                        newTermKanji = "来させない";
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = "こさせません";
                        newTermKanji = "来させません";
                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "させる");
                        newTermKanji = verbe.furigana.Replace("する", "させる");
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "させます");
                        newTermKanji = verbe.furigana.Replace("する", "させます");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "させない");
                        newTermKanji = verbe.furigana.Replace("する", "させない");
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "させません");
                        newTermKanji = verbe.furigana.Replace("する", "させません");

                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static JpString ToCausatifPassif(this VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            JpString verbConjug = (JpString)verbe;
            string terminaison = verbe.furigana.Last().ToString();
            bool isIchidan = verbe.type == E_TypeVerb.Ichidan;

            string newTerminaison = "";
            if (verbe.type == E_TypeVerb.KURU || verbe.type == E_TypeVerb.TSURU)
            {
                (string newTerm, string newTermKanji) verbIrr = ToCausatifPassifIrr(verbe, isPolite, isNegatif);
                verbConjug.kana = verbIrr.newTerm;
                verbConjug.kanji = verbIrr.newTermKanji;
            }
            else
            {
                newTerminaison = ToCausatifPassif(terminaison, isIchidan, isPolite, isNegatif);
                verbConjug.kanji = verbConjug.kanji.Replace(verbConjug.kanji.Last().ToString(), newTerminaison);
                verbConjug.kana = verbConjug.kana.Replace(verbConjug.kana.Last().ToString(), newTerminaison);
            }
            
            verbConjug.romaji = KanaTools.ToRomaji(verbConjug.kana);

            return verbConjug;
        }
        private static string ToCausatifPassif(string terminaison, bool isIchidan, bool isPolite, bool isNegatif)
        {
            //todo : verifier le polite et negatif
            string newTerminaison = ToCausatif(terminaison, isIchidan, false, false);
            newTerminaison = ToPassif(newTerminaison, true, isPolite, isNegatif);
            return newTerminaison;
        }
        private static (string newTerm, string newTermKanji) ToCausatifPassifIrr(VerbeDll verbe, bool isPolite, bool isNegatif)
        {
            string newTerm = "";
            string newTermKanji = "";
            switch (verbe.type)
            {
                case E_TypeVerb.KURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = "こさせられる";
                        newTermKanji = "来させられる";
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = "こさせられます";
                        newTermKanji = "来させられます";
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = "こさせられない";
                        newTermKanji = "来させられない";
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = "こさせられません";
                        newTermKanji = "来させられません";
                    }
                    break;
                case E_TypeVerb.TSURU:
                    if (!isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "させられる");
                        newTermKanji = verbe.furigana.Replace("する", "させられる");
                    }
                    else if (isPolite && !isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "させられます");
                        newTermKanji = verbe.furigana.Replace("する", "させられます");
                    }
                    else if (!isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "させられない");
                        newTermKanji = verbe.furigana.Replace("する", "させられない");
                    }
                    else if (isPolite && isNegatif)
                    {
                        newTerm = verbe.furigana.Replace("する", "させられません");
                        newTermKanji = verbe.furigana.Replace("する", "させられません");

                    }
                    break;
            }
            return (newTerm, newTermKanji);
        }


        private static string ToPoliteNegatif(string terminaison, bool isIchidan)
        {
            string newTerminaison = "";
            if (isIchidan)
            {
                //on supprime le ru
                newTerminaison = terminaison.Replace("る", "");
            }
            else
            {
                //on remplace la terminaison par sont equivalent en i
                newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));

            }
            //et on ajoute masu
            newTerminaison += "ません";
            return newTerminaison;
        }
        private static string ToJustNegatif(string terminaison, bool isIchidan, bool isPolite)
        {
            string newTerminaison = "";
            if (isIchidan)
            {
                //on supprime le ru
                newTerminaison = terminaison.Replace("る", "");
            }
            else
            {
                //si u on remplace par wa
                if (terminaison.Last() == 'う')
                {
                    newTerminaison = terminaison.Replace("う", "わ");
                }
                else
                {
                    //on remplace la terminaison par sont equivalent en A
                    newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'a'));
                }

            }
            //et on ajoute masu
            newTerminaison += "ない";
            return newTerminaison;
        }
        private static string ToJustPolite(string terminaison, bool isIchidan, bool isNegatif)
        {
            string newTerminaison = "";
            if (isIchidan)
            {
                //on supprime le ru
                newTerminaison = terminaison.Replace("る", "");
            }
            else
            {
                //on remplace la terminaison par sont equivalent en i
                newTerminaison = terminaison.Replace(terminaison.Last().ToString(), FindEquivalent(terminaison, 'i'));

            }
            //et on ajoute masu
            newTerminaison += "ます";
            return newTerminaison;
        }

        /// <summary>
        /// Trouve le temps a partir d'un verbe et de sa conjugaison
        /// </summary>
        /// <param name="verbeDb"></param>
        /// <param name="conjugaison"></param>
        /// <param name="infinitif"></param>
        /// <returns></returns>
        public static string FindTemps(VerbeDll verbeDb, string conjugaison, string infinitif = null)
        {
            bool isPolite = false;
            bool isNegatif = false;
            string temps = "";
            //on recupere le verbe depuis la base avec sont infinitf
            //Verbes verbeDb = (from v in db.Verbes
            //                  where v.romaji == infinitif || v.kanji == infinitif || v.furigana == infinitif
            //                  select v).FirstOrDefault();

            if (verbeDb == null)
            {
                return "Verbe Introuvable";
            }

            List<(JpString, string)> listConjug = new List<(JpString, string)>();

            //on le conjugue a tout  les temps neutre
            //on compare chaque temps avec la conjugaison donné
            listConjug.Add((verbeDb.ToPresent(isPolite, isNegatif), "Present"));
            listConjug.Add((verbeDb.ToPast(isPolite, isNegatif), "Past"));
            listConjug.Add((verbeDb.ToTe(isPolite, isNegatif), "Te"));
            listConjug.Add((verbeDb.ToProgressif(isPolite, isNegatif), "Progressif"));
            listConjug.Add((verbeDb.ToVolitional(isPolite, isNegatif), "Volitional"));
            listConjug.Add((verbeDb.ToImperatif(isPolite, isNegatif), "Imperatif"));
            listConjug.Add((verbeDb.ToRequest(isPolite, isNegatif), "Request"));
            listConjug.Add((verbeDb.ToConditionnel(isPolite, isNegatif), "Conditionnel"));
            listConjug.Add((verbeDb.ToConditionnelTara(isPolite, isNegatif), "ConditionnelTara"));
            listConjug.Add((verbeDb.ToPotential(isPolite, isNegatif), "Potential"));
            listConjug.Add((verbeDb.ToPassif(isPolite, isNegatif), "Passif"));
            listConjug.Add((verbeDb.ToCausatif(isPolite, isNegatif), "Causatif"));
            listConjug.Add((verbeDb.ToCausatifPassif(isPolite, isNegatif), "CausatifPassif"));

            //si match on retourne le temps + les indication
            temps = listConjug.FirstOrDefault(v => v.Item1.kana == conjugaison || v.Item1.kanji == conjugaison || v.Item1.romaji == conjugaison).Item2;
            //on revide la liste pour les autres test
            listConjug.Clear();

            //a tout les temps negatif
            if (string.IsNullOrEmpty(temps))
            {
                isNegatif = true;
                listConjug.Add((verbeDb.ToPresent(isPolite, isNegatif), "Present"));
                listConjug.Add((verbeDb.ToPast(isPolite, isNegatif), "Past"));
                listConjug.Add((verbeDb.ToTe(isPolite, isNegatif), "Te"));
                listConjug.Add((verbeDb.ToProgressif(isPolite, isNegatif), "Progressif"));
                listConjug.Add((verbeDb.ToVolitional(isPolite, isNegatif), "Volitional"));
                listConjug.Add((verbeDb.ToImperatif(isPolite, isNegatif), "Imperatif"));
                listConjug.Add((verbeDb.ToRequest(isPolite, isNegatif), "Request"));
                listConjug.Add((verbeDb.ToConditionnel(isPolite, isNegatif), "Conditionnel"));
                listConjug.Add((verbeDb.ToConditionnelTara(isPolite, isNegatif), "ConditionnelTara"));
                listConjug.Add((verbeDb.ToPotential(isPolite, isNegatif), "Potential"));
                listConjug.Add((verbeDb.ToPassif(isPolite, isNegatif), "Passif"));
                listConjug.Add((verbeDb.ToCausatif(isPolite, isNegatif), "Causatif"));
                listConjug.Add((verbeDb.ToCausatifPassif(isPolite, isNegatif), "CausatifPassif"));

                temps = listConjug.FirstOrDefault(v => v.Item1.kana == conjugaison || v.Item1.kanji == conjugaison || v.Item1.romaji == conjugaison).Item2;
                listConjug.Clear();
            }

            //a tout les temps polie
            if (string.IsNullOrEmpty(temps))
            {
                isNegatif = false;
                isPolite = true;
                listConjug.Add((verbeDb.ToPresent(isPolite, isNegatif), "Present"));
                listConjug.Add((verbeDb.ToPast(isPolite, isNegatif), "Past"));
                listConjug.Add((verbeDb.ToTe(isPolite, isNegatif), "Te"));
                listConjug.Add((verbeDb.ToProgressif(isPolite, isNegatif), "Progressif"));
                listConjug.Add((verbeDb.ToVolitional(isPolite, isNegatif), "Volitional"));
                listConjug.Add((verbeDb.ToImperatif(isPolite, isNegatif), "Imperatif"));
                listConjug.Add((verbeDb.ToRequest(isPolite, isNegatif), "Request"));
                listConjug.Add((verbeDb.ToConditionnel(isPolite, isNegatif), "Conditionnel"));
                listConjug.Add((verbeDb.ToConditionnelTara(isPolite, isNegatif), "ConditionnelTara"));
                listConjug.Add((verbeDb.ToPotential(isPolite, isNegatif), "Potential"));
                listConjug.Add((verbeDb.ToPassif(isPolite, isNegatif), "Passif"));
                listConjug.Add((verbeDb.ToCausatif(isPolite, isNegatif), "Causatif"));
                listConjug.Add((verbeDb.ToCausatifPassif(isPolite, isNegatif), "CausatifPassif"));

                temps = listConjug.FirstOrDefault(v => v.Item1.kana == conjugaison || v.Item1.kanji == conjugaison || v.Item1.romaji == conjugaison).Item2;
                listConjug.Clear();
            }

            //a tout les temps negatif polie
            if (string.IsNullOrEmpty(temps))
            {
                isNegatif = true;
                isPolite = true;
                listConjug.Add((verbeDb.ToPresent(isPolite, isNegatif), "Present"));
                listConjug.Add((verbeDb.ToPast(isPolite, isNegatif), "Past"));
                listConjug.Add((verbeDb.ToTe(isPolite, isNegatif), "Te"));
                listConjug.Add((verbeDb.ToProgressif(isPolite, isNegatif), "Progressif"));
                listConjug.Add((verbeDb.ToVolitional(isPolite, isNegatif), "Volitional"));
                listConjug.Add((verbeDb.ToImperatif(isPolite, isNegatif), "Imperatif"));
                listConjug.Add((verbeDb.ToRequest(isPolite, isNegatif), "Request"));
                listConjug.Add((verbeDb.ToConditionnel(isPolite, isNegatif), "Conditionnel"));
                listConjug.Add((verbeDb.ToConditionnelTara(isPolite, isNegatif), "ConditionnelTara"));
                listConjug.Add((verbeDb.ToPotential(isPolite, isNegatif), "Potential"));
                listConjug.Add((verbeDb.ToPassif(isPolite, isNegatif), "Passif"));
                listConjug.Add((verbeDb.ToCausatif(isPolite, isNegatif), "Causatif"));
                listConjug.Add((verbeDb.ToCausatifPassif(isPolite, isNegatif), "CausatifPassif"));

                temps = listConjug.FirstOrDefault(v => v.Item1.kana == conjugaison || v.Item1.kanji == conjugaison || v.Item1.romaji == conjugaison).Item2;
                listConjug.Clear();
            }

            if (string.IsNullOrEmpty(temps))
            {
                return "Conjugaison Introuvable";
            }

            return $"{temps} {(isPolite ? "polie" : "")} {(isNegatif ? "négatif" : "")}";
        }

        public static string FindEquivalent(string furigana, char letter)
        {
            //on recupere le dernier kana
            string terminaison = furigana.Last().ToString();
            //on cherche son equivalent romaji
            string terminRomaji = KanaTools.ToRomaji(terminaison);

            //on remplace la terminaison par la lettre
            terminRomaji = terminRomaji.Replace('u', letter);

            //gestion des cas particulier
            switch (terminRomaji)
            {
                case "tse":
                    terminRomaji = "te";
                    break;
                case "tsa":
                    terminRomaji = "ta";
                    break;
                case "tso":
                    terminRomaji = "to";
                    break;
                case "tsi":
                    terminRomaji = "chi";
                    break;
                case "si":
                    terminRomaji = "shi";
                    break;
                default:
                    break;
            }

            //on trouve sont equivalent en kana dans la base
            //string terminKana = db.Hiragana.FirstOrDefault(h => h.romaji == terminRomaji).hiragana1;
            string terminKana = KanaTools.ToHiragana(terminRomaji);

            //on retourne l'equivalent
            return terminKana;
        }

        public enum E_TmpsVerb
        {
            Present = 1,
            Past = 2,
            Progressif = 3,
            Te = 4,
            Potential = 5,
            Imperatif = 6,
            Causatif = 7,
            Causatif_Passif = 8,
            Conditionnel_Provisionnel = 9,
            Volitional = 10,
            Passif = 11,
            Request = 12,
            Conditionnel_Tara = 13,
            JustPolite = 14,
            JustNegatif = 15,
        }

        public enum E_TypeVerb
        {
            Ichidan = 1,
            Godan = 5,
            IKU = 6,
            KURU = 7,
            TSURU = 8,
        }
        #endregion

        #region Number
        public static List<JpNumber> NumberList = new List<JpNumber>()
        {
            new JpNumber("0","零","れい","-"),
            new JpNumber("1","一","いち","壱"),
            new JpNumber("2","二","に","弐"),
            new JpNumber("3","三","さん","参"),
            new JpNumber("4","四","よん","し",""),
            new JpNumber("5","五","ご","伍"),
            new JpNumber("6","六","ろく",""),
            new JpNumber("7","七","なな","しち",""),
            new JpNumber("8","八","はち",""),
            new JpNumber("9","九","きゅう","く",""),
            new JpNumber("10","十","じゅう","拾"),
            new JpNumber("100","百","ひゃく",""),
            new JpNumber("1000","千","せん",""),
            new JpNumber("10000","万","まん","萬	"),
            new JpNumber("100000000","億","おく",""),
            new JpNumber("1000000000000","兆","ちょう",""),
            new JpNumber("10000000000000000000","京","けい","きょう","")
        };

        public static string GetKanjiNum(ulong chiffre)
        {
            return NumberList.FirstOrDefault(n => n.Chiffre == chiffre.ToString()).Kanji;
        }
        public static string GetKanaNum(ulong chiffre, bool kana2 = false)
        {
            JpNumber num = NumberList.FirstOrDefault(n => n.Chiffre == chiffre.ToString());
            string value = num.Kana;
            if (kana2 && !String.IsNullOrEmpty(num.Kana2))
            {
                value += $" / {NumberList.FirstOrDefault(n => n.Chiffre == chiffre.ToString()).Kana2}";
            }
            return value;
        }
        public static long GetNumKanji(string kanji)
        {
            return long.Parse(NumberList.FirstOrDefault(n => n.Kanji == kanji).Chiffre);
        }

        public class JpNumber
        {
            public string Chiffre { get; set; }
            public string Kanji { get; set; }
            public string Kana { get; set; }
            public string Kana2 { get; set; }
            public string Legal { get; set; }

            public JpNumber()
            {

            }
            public JpNumber(string chiffre, string kanji, string kana, string legal)
            {
                this.Chiffre = chiffre;
                Kanji = kanji;
                Kana = kana;
                Legal = legal;
            }

            public JpNumber(string chiffre, string kanji, string kana, string kana2, string legal)
            {
                this.Chiffre = chiffre;
                Kanji = kanji;
                Kana = kana;
                Kana2 = kana2;
                Legal = legal;
            }
        }

        public static string NumberToKanji(long number)
        {
            //recuperation de la chaine d'entrée
            string nbString = number.ToString();
            string KanjiReturn = "";
            int pos = nbString.Length;
            foreach (char chiffre in nbString)
            {
                //Verification de la position du chiffre dans la chaine
                //Recuperation depuis le dictionnaire
                //avec gestion particuliere du zero
                string val = "";
                if (chiffre != '0')
                {
                    if (chiffre == '1' && (pos == 2 || pos == 3 || pos == 4 || pos == 6 || pos == 7 || pos == 10 || pos == 11))
                    {
                        //on affiche le 1 pour des cas particulier (万 et　億)
                    }
                    else
                    {
                        KanjiReturn += GetKanjiNum(ulong.Parse(chiffre.ToString()));// JapaneseTools.DicoChiffre[chiffre.ToString()];
                    }

                    if (pos == 2 || pos == 6 || pos == 10 || pos == 14)//100 000
                    {
                        KanjiReturn += "十"; //10
                    }
                    if (pos == 3 || pos == 7 || pos == 11 || pos == 15)
                    {
                        //check le chiffre pour savoir si une regle d'ecriture special s'applique
                        KanjiReturn += "百";//100
                    }
                    if (pos == 4 || pos == 8 || pos == 12)
                    {
                        KanjiReturn += "千";//1000
                    }
                    if (pos == 5 || (pos > 5 && pos < 9))
                    {
                        KanjiReturn += "万";//10 000
                    }
                    if (pos == 9 || (pos > 9 && pos < 13))
                    {
                        KanjiReturn += "億";//100 000 000　おく
                    }
                    if (pos == 13 || (pos > 13 && pos < 20))
                    {
                        KanjiReturn += "兆";//1000 000 000 000　ちょう
                    }
                    if (pos == 20)
                    {
                        KanjiReturn += "京";//10 000 000 000 000 000 000　けい
                    }

                }
                else if (chiffre == '0' && nbString.Length == 1)
                {
                    KanjiReturn += "零";
                }
                pos--;
            }
            //decompoition de celle ci
            //bouce ur chacun des chiffres
            //en fonctin de sa position ajouter ou non les signes 
            return KanjiReturn;
        }

        public static string KanjiToNumber(string kana)
        {
            string ReturnNumber = "";
            //pour chaque position on parse sa valeur en fonction de la valeur dans le dictionnaire
            long total = 0;
            int pallier = 10000; // modifier avec les pallier encore plus grand
                                 //on prends le chiffre a la derniere position
            for (int i = kana.Length; i > 0; i--)
            {

                //on le parse en int
                long val = JapaneseTools.GetNumKanji(kana[i - 1].ToString());// long.Parse(DicoChiffre[kana[i - 1].ToString()]);
                long valAvant = 1;
                try
                {
                    //on recupere le character d'avant au cas ou on aurai une dizaine
                    valAvant = JapaneseTools.GetNumKanji(kana[i - 2].ToString());//long.Parse(DicoChiffre[kana[i - 2].ToString()]);
                    if (valAvant > 1 && valAvant < 10)
                    {
                        val *= valAvant;
                        i--;
                        valAvant = JapaneseTools.GetNumKanji(kana[i - 2].ToString());//long.Parse(DicoChiffre[kana[i - 2].ToString()]);
                    }

                    //si c'est un millier ou dix millier au sauté le chiffre d'avant si c'est un un
                    if ((val == 1000 || val == 10000) && valAvant == 1)
                    {
                        i--;
                    }
                }
                catch (Exception)
                {
                    valAvant = 1;
                }

                if (total > pallier)
                {
                    val *= pallier;
                    //modifier le pallier avec les pallier plus grand ici
                }

                total += val;

            }
            return total.ToString();
        }

        public static string NumberToKana(long number, bool kana2 = false)
        {
            //recuperation de la chaine d'entrée
            string nbString = number.ToString();
            string KanaReturn = "";
            int pos = nbString.Length;
            foreach (char chiffre in nbString)
            {
                //Verification de la position du chiffre dans la chaine
                //Recuperation depuis le dictionnaire
                //avec gestion particuliere du zero
                string val = "";
                if (chiffre != '0')
                {
                    if (chiffre == '1' && (pos == 2 || pos == 3 || pos == 4 || pos == 6 || pos == 7 || pos == 10 || pos == 11))
                    {
                        //on affiche le 1 pour des cas particulier (万 et　億)
                    }
                    else
                    {
                        KanaReturn += JapaneseTools.GetKanaNum(ulong.Parse(chiffre.ToString()), kana2);// JapaneseTools.DicoChiffre[chiffre.ToString()];
                    }

                    if (pos == 2 || pos == 6 || pos == 10 || pos == 14)//100 000
                    {
                        KanaReturn += JapaneseTools.GetKanaNum(10); //"じゅう";
                    }
                    if (pos == 3 || pos == 7 || pos == 11 || pos == 15)
                    {
                        //check le chiffre pour savoir si une regle d'ecriture special s'applique
                        KanaReturn += JapaneseTools.GetKanaNum(100);//"ひゃく";//100
                    }
                    if (pos == 4 || pos == 8 || pos == 12)
                    {
                        KanaReturn += JapaneseTools.GetKanaNum(1000);//"せん";//1000
                    }
                    if (pos == 5 || (pos > 5 && pos < 9))
                    {
                        KanaReturn += JapaneseTools.GetKanaNum(10000);//"まん";//10 000
                    }
                    if (pos == 9 || (pos > 9 && pos < 13))
                    {
                        KanaReturn += JapaneseTools.GetKanaNum(100000000);//"おく";//100 000 000　おく
                    }
                    if (pos == 13 || (pos > 13 && pos < 20))
                    {
                        KanaReturn += JapaneseTools.GetKanaNum(1000000000000);//"ちゅう";//1000 000 000 000　ちょう
                    }
                    if (pos == 20)
                    {
                        KanaReturn += JapaneseTools.GetKanaNum(10000000000000000000);//"けい";//10 000 000 000 000 000 000　けい
                    }
                }
                else if (chiffre == '0' && nbString.Length == 1)
                {
                    KanaReturn += JapaneseTools.GetKanaNum(0);//"れい";
                }
                pos--;
            }
            //gestion des exception
            //300
            if (KanaReturn.Contains("さんひゃく"))
                KanaReturn = KanaReturn.Replace("さんひゃく", "さんびゃく");
            //600
            if (KanaReturn.Contains("ろくひゃく"))
                KanaReturn = KanaReturn.Replace("ろくひゃく", "ろっぴゃく");
            //800
            if (KanaReturn.Contains("はちひゃく"))
                KanaReturn = KanaReturn.Replace("はちひゃく", "はっぴゃく");
            //3000
            if (KanaReturn.Contains("さんせん"))
                KanaReturn = KanaReturn.Replace("さんせん", "さんぜん");
            //3000
            if (KanaReturn.Contains("いちせんまん"))
                KanaReturn = KanaReturn.Replace("いちせんまん", "いっせんまん");
            return KanaReturn;
        }
        #endregion
    }



    public class JpString
    {
        public string romaji { get; set; }
        public string kana { get; set; }
        public string kanji { get; set; }

        public static explicit operator JpString(VerbeDll verb) => new JpString()
        {
            romaji = verb.romaji,
            kana = verb.furigana,
            kanji = verb.kanji,
        };
        public static explicit operator JpString(AdjectifDll adj) => new JpString()
        {
            romaji = adj.romaji,
            kana = adj.furigana,
            kanji = adj.kanji,
        };

        public string GetRomaji()
        {

            return KanaTools.ToRomaji(kana);
        }
    }


    public class AdjectifDll
    {
        public string furigana { get; set; }
        public string kanji { get; set; }
        public string romaji { get; set; }
        public string type { get; set; }

        public AdjectifDll(string furigana, string kanji, string romaji, string type)
        {
            this.furigana = furigana;
            this.kanji = kanji;
            this.romaji = romaji;
            this.type = type;
        }

        public AdjectifDll()
        { }
    }
    public class VerbeDll
    {
        public string furigana { get; set; }
        public string kanji { get; set; }
        public string romaji { get; set; }
        public E_TypeVerb type { get; set; }

        public VerbeDll(string furigana, string kanji, string romaji, string type)
        {
            this.furigana = furigana;
            this.kanji = kanji;
            this.romaji = romaji;

            switch (type)
            {
                case "Ichidan":
                    this.type = E_TypeVerb.Ichidan;
                    break;
                case "Godan":
                    this.type = E_TypeVerb.Godan;
                    break;
                case "IKU":
                    this.type = E_TypeVerb.IKU;
                    break;
                case "KURU":
                    this.type = E_TypeVerb.KURU;
                    break;
                case "TSURU":
                    this.type = E_TypeVerb.TSURU;
                    break;
                default:
                    break;
            }
        }
    }
}
