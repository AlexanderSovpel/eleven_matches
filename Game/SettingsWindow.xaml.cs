using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class SettingsWindow : Window
    {
        public int MaxMatches { get; set; }
        public int MaxSelect { get; set; }

        public SettingsWindow(int maxMatches, int maxSelect)
        {
            InitializeComponent();

            maxMatchesCountTextBox.Text = maxMatches.ToString();
            maxSelectTextBox.Text = maxSelect.ToString();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void maxMatchesCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CheckNumbers(maxMatchesCountTextBox) && CheckNumbers(maxSelectTextBox))
            {
                int matchValue = Convert.ToInt32(maxMatchesCountTextBox.Text);
                int selectValue = Convert.ToInt32(maxSelectTextBox.Text);

                if (CheckLogic(matchValue, selectValue))
                {
                    MaxMatches = matchValue;
                    MaxSelect = selectValue;
                }
            }
        }

        private bool CheckNumbers(TextBox textBox)
        {
            Regex pattern = new Regex("^\\d+$");
            if (pattern.IsMatch(textBox.Text))
            {
                error.Text = "";
                saveButton.IsEnabled = true;
                return true;
            }
            else
            {
                error.Text = "введите число";
                saveButton.IsEnabled = false;
                return false;
            }
        }

        private bool CheckLogic(int matchValue, int selectValue)
        {
            if (selectValue < matchValue)
            {
                if (selectValue > 1)
                {
                    error.Text = "";
                    saveButton.IsEnabled = true;
                    return true;
                }
                else
                {
                    error.Text = "нельзя взять так мало спичек";
                    saveButton.IsEnabled = false;
                    return false;
                }
            }
            else
            {
                error.Text = "нельзя взять так много спичек";
                saveButton.IsEnabled = false;
                return false;
            }

        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            maxMatchesCountTextBox.Text = "11";
            MaxMatches = 11;
            maxSelectTextBox.Text = "3";
            MaxSelect = 3;
        }
    }
}
