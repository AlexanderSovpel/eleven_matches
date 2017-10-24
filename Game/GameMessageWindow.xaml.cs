using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game
{
    public enum GameButtons { Ok, OkCancel, YesNo, YesNoCancel };

    public partial class GameMessageWindow : Window
    {
        public GameButtons Result { get; set; }

        public GameMessageWindow(string message, string title, GameButtons buttons)
        {
            InitializeComponent();

            messageText.Text = message;
            this.Title = title;

            switch (buttons)
            {
                case GameButtons.Ok:
                    CancelButton.Visibility = Visibility.Collapsed;
                    YesButton.Visibility = Visibility.Collapsed;
                    NoButton.Visibility = Visibility.Collapsed;
                    break;
                case GameButtons.OkCancel:
                    YesButton.Visibility = Visibility.Collapsed;
                    NoButton.Visibility = Visibility.Collapsed;
                    break;
                case GameButtons.YesNo:
                    OkButton.Visibility = Visibility.Collapsed;
                    CancelButton.Visibility = Visibility.Collapsed;
                    break;
                case GameButtons.YesNoCancel:
                    OkButton.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
