using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWPF
{

    internal class Data
    {
        public int HP = 10; //default max is 10
        public int Score = 0;
        public string TargetWord = new string("");
        public List<char>? TargetWordLetters = new List<char>();
        public List<char>? LettersUsed = new List<char>();
        public List<string> WordList = new List<string>();
    }
}
