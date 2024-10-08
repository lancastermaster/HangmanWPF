using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HangmanWPF
{
    /// <summary>
    /// Interaction logic for GameOverWindow.xaml
    /// </summary>
    public partial class GameOverWindow : Window
    {
        public MainWindow mainWindow;
        public Helper helper;

        public GameOverWindow()
        {
            InitializeComponent();
            //helper = mainWindow.GameHelper;
        }

        private void button_PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            helper = mainWindow.GameHelper;

            helper.GameData.HP = 10;
            helper.GameData.Score = 0;
            mainWindow.SetupGameWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void button_ExitGame_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
