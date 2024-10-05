using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HangmanWPF
{
    public class Helper
    {
        public Data GameData;
        public static string WordListFile = "HangmanWordList.txt"; //E:\C# Projects - Programs\NewProjects\HangmanWPF\HangmanWordList.txt
        public static Random random;

        public Helper() 
        {
            GameData = new Data();
            random = new Random();
            InitializeWordList();
        }

        public void InitializeWordList() 
        {
            string[] data = File.ReadAllLines(WordListFile);

            foreach (string line in data) 
            {
                GameData.WordList.Add(line);
            }

            ChooseTargetWord();
        }

        public void ChooseTargetWord() 
        {
            int r = random.Next(GameData.WordList.Count);

            GameData.TargetWord = GameData.WordList[r];
            GameData.TargetWordLetters = GameData.TargetWord.ToList<Char>();
        }

        public bool IsLetterInWord(char LetterToCheck)
        {
            if(!GameData.LettersUsed.Contains(LetterToCheck)) GameData.LettersUsed.Add(LetterToCheck);

            if (GameData.TargetWordLetters.Contains(LetterToCheck)) return true; 
            else return false;
            
        }

        public void TakeDamage() 
        {
            if (GameData.HP - 1 > 0) GameData.HP--;
            else GameData.HP = 0;
        }

        public void IncreaseScore(int InScore)
        {
            GameData.Score += InScore;
        }
    }
}
