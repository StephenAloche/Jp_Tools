using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiraKana;
using Jp_Tools;
using static Jp_Tools.JapaneseTools;

namespace TestJp_Tools
{
    [TestClass]
    public class TestHiraKana
    {
        //TestKanaToRomaji
        [TestMethod]
        public void ConverHiraganaToRomaji()
        {
            Assert.AreEqual(
                KanaTools.ToRomaji("きょくたんなざんきょう"),
                "kyokutannazankyou");

        }

        [TestMethod]
        public void ConverKatakanaToRomaji()
        {
            Assert.AreEqual(
                KanaTools.ToRomaji("キョクタンナザンキョウ"),
                "kyokutannazankyou");
        }

        //TestRomajiToHiragana
        [TestMethod]
        public void ConvertSimpleStringsToHiragana()
        {
            Assert.AreEqual(
                KanaTools.ToHiragana("monono"),
                "ものの");

            Assert.AreEqual(
                KanaTools.ToHiragana("kyokutannazankyou"),
                "きょくたんなざんきょう");
        }

        [TestMethod]
        public void ConvertTrickyStringsToHiragana()
        {
            Assert.AreEqual(
                KanaTools.ToHiragana("teppennowebbutyotto"),
                "てっぺんのうぇっぶちょっと");

            Assert.AreEqual(
                KanaTools.ToHiragana("tunaototsunadiji"),
                "つなおとつなぢじ");

            Assert.AreEqual(
                KanaTools.ToHiragana("bizinessu"),
                "びじねっす");

            Assert.AreEqual(
                KanaTools.ToHiragana("pyottonn"),
                "ぴょっとん");
        }

        [TestMethod]
        public void TestOptionsForHiragana()
        {
            Assert.AreEqual(
                KanaTools.ToHiragana("wiwewo"),
                "うぃうぇを"
            );
        }

        //TestRomajiToKana
        [TestMethod]
        public void ConvertSimpleStringsToKatakana()
        {
            Assert.AreEqual(
                KanaTools.ToKatakana("monono"),
                "モノノ");

            Assert.AreEqual(
                KanaTools.ToKatakana("kyokutannazankyou"),
                "キョクタンナザンキョウ");
        }

        [TestMethod]
        public void ConvertTrickyStringsToKatakana()
        {
            Assert.AreEqual(
                KanaTools.ToKatakana("teppennowebbutyotto"),
                "テッペンノウェッブチョット");

            Assert.AreEqual(
                KanaTools.ToKatakana("tunaototsunadiji"),
                "ツナオトツナヂジ");

            Assert.AreEqual(
                KanaTools.ToKatakana("bizinessu"),
                "ビジネッス");

            Assert.AreEqual(
                KanaTools.ToKatakana("pyottonn"),
                "ピョットン");
        }

        //UtilityMethode

        [TestMethod]
        public void TestBooleanChecks()
        {
            Assert.IsTrue(KanaTools.IsHiragana("ものの"));
            Assert.IsFalse(KanaTools.IsHiragana("キョクタン"));
            Assert.IsFalse(KanaTools.IsHiragana("romaji"));
            Assert.IsTrue(KanaTools.IsKatakana("キョクタン"));
            Assert.IsTrue(KanaTools.IsKana("キョクタンものの"));
            Assert.IsTrue(KanaTools.IsRomaji("romaji"));
        }

        [TestClass]
        public class TestJapaneseTools
        {
            VerbeDll taberu = new VerbeDll("たべる", "食べる", "taberu", E_TypeVerb.Ichidan);            

            //Conjugaison Verbes
            [TestMethod]
            public void TestConjugVerb()
            {
                JpString taberuConjug = (JpString)new VerbeDll("たべます", "食べます", "tabemasu", E_TypeVerb.Ichidan);
                Assert.AreEqual(
                    JapaneseTools.ConjugVerb(taberu, E_TmpsVerb.Present, true, false),
                    taberuConjug);
            }

            [TestMethod]
            public void TestKanjiToNumber()
            {
                Assert.AreEqual(
                    JapaneseTools.KanjiToNumber("一万二千三百四十五"),
                    "12345");

                Assert.AreEqual(
                    JapaneseTools.KanjiToNumber("八億百万二百"),
                    "801000200");

                Assert.AreEqual(
                    JapaneseTools.KanjiToNumber("八億四十万二百"),
                    "800400200");

                Assert.AreEqual(
                    JapaneseTools.KanjiToNumber("八億七千九百三十二万五千八百四十六"),
                    "879325846");

                
                Assert.AreEqual(
                    JapaneseTools.KanjiToNumber("一千万三千八百"),
                    "10003800");
                
            }

            //Number
            [TestMethod]
            public void TestNumberToKana()
            {
                Assert.AreEqual(
                    JapaneseTools.NumberToKana(12345, false, false, false),
                    "いちまんにせんさんびゃくよんじゅうご");

                Assert.AreEqual(
                    JapaneseTools.NumberToKana(849846846, false, false, false),
                    "はちおくよんせんきゅうひゃくはちじゅうよんまんろくせんはっぴゃくよんじゅうろく");

                Assert.AreEqual(
                    JapaneseTools.NumberToKana(10003800, false, false, false),
                    "いっせんまんさんぜんはっぴゃく");


            }
            [TestMethod]
            public void TestNumberToKanji()
            {
                Assert.AreEqual(
                    JapaneseTools.NumberToKana(12345, true, false, false),
                    "一万二千三百四十五");

                Assert.AreEqual(
                    JapaneseTools.NumberToKana(849846846, true, false, false),
                    "八億四千九百八十四万六千八百四十六");

                Assert.AreEqual(
                    JapaneseTools.NumberToKana(10003800, true, false, false),
                    "一千万三千八百");
            }

        }
    }


}