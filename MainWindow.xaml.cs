﻿using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HangmanWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Helper GameHelper;

        public TitleWindow titleWindow = new TitleWindow();
        public GameOverWindow gameOverWindow = new GameOverWindow();
        public MainWindow()
        {
            InitializeComponent();
            GameHelper = new Helper();
            titleWindow.mainWindow = this;
            gameOverWindow.mainWindow = this;
            gameOverWindow.helper = GameHelper;
            titleWindow.Show();
            gameOverWindow.Hide();
            this.Hide();

            //SetupGameWindow();
        }

        public void SetupGameWindow()
        {
            DisplayWord();
            DisplayUsedLetters();
            label_CurrentHP.Content = GameHelper.GameData.HP.ToString();
            label_CurrentScore.Content = GameHelper.GameData.Score.ToString();
        }

        public void DisplayWord() 
        {
            string WordToShow = "";

            List<char> chars = GameHelper.GameData.TargetWordLetters;

            label_WordToGuess.Content = String.Empty;

            foreach (Char c in chars) 
            {
                if (GameHelper.GameData.LettersUsed.Contains(c))
                {
                    WordToShow = String.Concat(WordToShow, c.ToString());
                }
                else 
                {
                    WordToShow = String.Concat(WordToShow, "*");
                }
            }

            label_WordToGuess.Content = WordToShow;
        }

        public void DisplayUsedLetters()
        {
            listBox_UsedLetters.Items.Clear();

            foreach (char c in GameHelper.GameData.LettersUsed)
            {
                listBox_UsedLetters.Items.Add(c.ToString());
            }
        }

        private void button_ConfirmLetter_Click(object sender, RoutedEventArgs e)
        {
            if (GameHelper.GameData.HP <= 0) return;

            char LetterGuess;
            string WordGuess;

            if (textBox_GuessLetter.Text != null && textBox_GuessLetter.Text != "")
            {
                LetterGuess = Char.Parse(textBox_GuessLetter.Text);
                bool bLetterInWord = GameHelper.IsLetterInWord(LetterGuess);
                if (bLetterInWord)
                {
                    //reveal each instance of the letter in the word
                    GameHelper.IncreaseScore(10);
                    DisplayWord();
                }
                else 
                {
                    GameHelper.TakeDamage();
                    label_CurrentHP.Content = GameHelper.GameData.HP;
                }

                DisplayUsedLetters();
            }
            else if (textBox_GuessWord.Text != null && textBox_GuessWord.Text != "")
            {
                WordGuess = textBox_GuessWord.Text;

                if (WordGuess == GameHelper.GameData.TargetWord.ToString())
                {
                    //if the word is correct, the player wins/increases their score
                    GameHelper.IncreaseScore(100);
                    label_WordToGuess.Content = WordGuess.ToString();
                    //make the game wait a few seconds
                    //then a new word will be chosen
                    GameHelper.GameData.LettersUsed.Clear();
                    GameHelper.ChooseTargetWord();
                    DisplayWord();
                }
                else 
                {
                    GameHelper.TakeDamage();
                    label_CurrentHP.Content = GameHelper.GameData.HP;
                }
            }
            else 
            {
                Console.WriteLine("Please either guess the word or a letter in the word.");
            }

            label_CurrentScore.Content = GameHelper.GameData.Score.ToString();

            if (GameHelper.GameData.HP <= 0)
            {
                gameOverWindow.label_FinalScore.Content = GameHelper.GameData.Score;
                GameHelper.UpdateHighScores(GameHelper.GameData.Score);


                if (GameHelper.GameData.HighScores.Count < 1)
                {
                    gameOverWindow.label_HighScoreNumber.Content = "0";
                }
                else if (GameHelper.GameData.HighScores.Count > 0 && GameHelper.GameData.HighScores.Count < 4)
                {
                    switch (GameHelper.GameData.HighScores.Count)
                    {
                        case 1:
                            gameOverWindow.label_HighScoreNumber.Content = GameHelper.GameData.HighScores[0].ToString(); ;
                            break;

                        case 2:
                            gameOverWindow.label_HighScoreNumber.Content =
                                GameHelper.GameData.HighScores[GameHelper.GameData.HighScores.Count - 1].ToString()
                        + "\n" + GameHelper.GameData.HighScores[GameHelper.GameData.HighScores.Count - 2].ToString();
                            break;

                        case 3:
                            gameOverWindow.label_HighScoreNumber.Content =
                            GameHelper.GameData.HighScores[GameHelper.GameData.HighScores.Count - 1].ToString()
                        + "\n" + GameHelper.GameData.HighScores[GameHelper.GameData.HighScores.Count - 2].ToString()
                        + "\n" + GameHelper.GameData.HighScores[GameHelper.GameData.HighScores.Count - 3].ToString();
                            break;
                    }
                }
                else 
                {
                    gameOverWindow.label_HighScoreNumber.Content =
                            GameHelper.GameData.HighScores[GameHelper.GameData.HighScores.Count - 1].ToString()
                        + "\n" + GameHelper.GameData.HighScores[GameHelper.GameData.HighScores.Count - 2].ToString()
                        + "\n" + GameHelper.GameData.HighScores[GameHelper.GameData.HighScores.Count - 3].ToString();
                }

                gameOverWindow.Show();
                this.Hide();
            }
        }
    }
}