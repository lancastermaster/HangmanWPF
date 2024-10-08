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
        public static string HighScoreList = "HighScores.txt";
        public static Random random;

        public Helper() 
        {
            GameData = new Data();
            random = new Random();
            InitializeWordList();
            InitializeHighScores();
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

        public void InitializeHighScores() 
        {
            string[] data = File.ReadAllLines(HighScoreList);

            foreach (string line in data)
            {
                GameData.HighScores.Add(int.Parse(line));
            }
        }

        public void UpdateHighScores(int ScoreToCheck)
        {
            int HighestScore = 0;

            foreach (int i in GameData.HighScores)
            {
                if (HighestScore < i) HighestScore = i;
            }

            if (GameData.HighScores.Count >= 5)
            {
                if (ScoreToCheck > HighestScore)
                {
                    HighestScore = ScoreToCheck;
                    GameData.HighScores.Add(ScoreToCheck);
                }
            }
            else GameData.HighScores.Add(ScoreToCheck);


            SortHighScores();

            List<string> scoreStrings = new List<string>();
            foreach (int i in GameData.HighScores)
            {
                scoreStrings.Add(i.ToString());
            }

            scoreStrings.Sort();

                    //SortHighScores();

            File.WriteAllLines(HighScoreList, scoreStrings);
                    //GameData.HighScores.Sort();

        }

        public void SortHighScores()
        {
            GameData.HighScores.Sort();
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
