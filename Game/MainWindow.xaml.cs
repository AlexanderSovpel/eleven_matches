using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Windows.Media.Animation;
using Game;

namespace GameProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Game game;
        private List<Player> players = new List<Player>();
        private List<Button> selectedMatches;

        private bool firstTurn = false;
        private bool saved = true;

        private static XmlSerializer serializerGame = new XmlSerializer(typeof(Game));
        private static XmlSerializer serializerPlayers = new XmlSerializer(typeof(List<Player>));
        private string gamePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TheElevenGame";

        private const string makesTurnConstText = " ходит...";
        private const string winsConstText = " побеждает!";

        public MainWindow()
        {
            InitializeComponent();

            CommandBinding newBind = new CommandBinding(ApplicationCommands.New);
            CommandBinding openBind = new CommandBinding(ApplicationCommands.Open);
            CommandBinding saveBind = new CommandBinding(ApplicationCommands.Save);
            CommandBinding closeBind = new CommandBinding(ApplicationCommands.Close);
            CommandBinding helpBind = new CommandBinding(ApplicationCommands.Help);

            newBind.Executed += NewCommand_Executed;
            openBind.Executed += OpenBind_Executed;
            saveBind.Executed += SaveBind_Executed;
            closeBind.Executed += CloseBind_Executed;
            helpBind.Executed += HelpBind_Executed;

            this.CommandBindings.Add(newBind);
            this.CommandBindings.Add(openBind);
            this.CommandBindings.Add(saveBind);
            this.CommandBindings.Add(closeBind);
            this.CommandBindings.Add(helpBind);

            // загружаем данные из файла настроек 
            using (Stream stream = new FileStream("settings.xml", FileMode.Open))
            {
                game = (Game)serializerGame.Deserialize(stream);
            }

            using (Stream stream = new FileStream("players.xml", FileMode.Open))
            {
                players = (List<Player>)serializerPlayers.Deserialize(stream);
            }

            //game.Player1 = players[players.Count - 1];
            player1.Text = game.Player1.Name;
            player1Save.Visibility = Visibility.Hidden;
            player1Win.Text = game.Player1.Win.ToString();
            player1Loose.Text = game.Player1.Loose.ToString();

            //game.Player2 = players[players.Count - 2];
            player2.Text = game.Player2.Name;
            player2Save.Visibility = Visibility.Hidden;
            player2Win.Text = game.Player2.Win.ToString();
            player2Loose.Text = game.Player2.Loose.ToString();

            gameState.Text = game.Player1.Name + makesTurnConstText;

            game.StartNew();
            for (int i = 0; i < game.MaxMatchesCount; ++i)
            {
                Button match = new Button();
                match.Name = "match_" + i;
                match.Template = (ControlTemplate)Application.Current.Resources["matchButton"];
                match.Click += MatchButton_Click;
                match.MouseEnter += MatchButton_Enter;
                match.MouseLeave += MatchButton_Leave;
                matches.Children.Add(match);
            }

            selectedMatches = new List<Button>();

            container.Effect = new BlurEffect();
            ((BlurEffect)container.Effect).Radius = 20;

            Grid aboutPage = (Grid)this.Resources["aboutPage"];
            this.mainGrid.Children.Add(aboutPage);
            this.ApplyTemplate();
        }

        private void HelpBind_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            makeTurn.IsDefault = false;
            Grid aboutPage = (Grid)this.Resources["aboutPage"];
            mainGrid.Children.Add(aboutPage);
            container.Effect = new BlurEffect();
            ((BlurEffect)container.Effect).Radius = 20;

            this.FadeInAnimate(aboutPage);
            this.BlurAnimate();
        }

        private void CloseBind_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!saved)
            {
                GameMessageWindow gmw = new GameMessageWindow("Сохранить текущую игру?", " Закрытие игры", GameButtons.YesNo);
                if (gmw.ShowDialog() == true)
                {
                    SaveGame();
                }
            }
            else
                this.Close();
        }

        private void SaveBind_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveGame();
        }

        private void SaveGame()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Game Saves (*.xml)|*.xml";

            if (!Directory.Exists(gamePath))
                Directory.CreateDirectory(gamePath);

            saveFile.InitialDirectory = gamePath;
            if (saveFile.ShowDialog() == true)
            {
                using (Stream writer = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    serializerGame.Serialize(writer, game);
                }
            }

            saved = true;
        }

        private void OpenBind_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!saved)
            {
                GameMessageWindow gmw = new GameMessageWindow("Сохранить текущую игру?", " Старт новой игры", GameButtons.YesNo);
                if (gmw.ShowDialog() == true)
                {
                    SaveGame();
                }
            }

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Game Saves (*.xml)|*.xml";

            if (!Directory.Exists(gamePath))
                Directory.CreateDirectory(gamePath);

            openFile.InitialDirectory = gamePath;
            if (openFile.ShowDialog() == true)
            {
                using (Stream reader = new FileStream(openFile.FileName, FileMode.Open))
                {
                    game = (Game)serializerGame.Deserialize(reader);

                    switch(game.CurrentState)
                    {
                        case GameState.Player1Turn:
                            gameState.Text = game.Player1.Name + makesTurnConstText;
                            break;
                        case GameState.Player2Turn:
                            gameState.Text = game.Player2.Name + makesTurnConstText;
                            break;
                    }

                    matches.Children.Clear();
                    for (int i = 0; i < game.MatchesList.Items.Count; ++i)
                    {
                        Button match = new Button();
                        match.Name = "match_" + i;
                        if (game.MatchesList.Items[i].Taken)
                            match.Template = (ControlTemplate)Application.Current.Resources["matchPressed"];
                        else
                            match.Template = (ControlTemplate)Application.Current.Resources["matchButton"];
                        match.Click += MatchButton_Click;
                        match.MouseEnter += MatchButton_Enter;
                        match.MouseLeave += MatchButton_Leave;
                        matches.Children.Add(match);
                    }
                }
            }
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!saved)
            {
                GameMessageWindow gmw = new GameMessageWindow("Сохранить текущую игру?", " Старт новой игры", GameButtons.YesNo);
                if (gmw.ShowDialog() == true)
                {
                    SaveGame();
                }
            }

            game.StartNew();
            firstTurn = true;
            player1.IsEnabled = true;
            player2.IsEnabled = true;
            gameState.Text = game.Player1.Name + makesTurnConstText;

            matches.Children.Clear();
            for (int i = 0; i < game.MaxMatchesCount; ++i)
            {
                Button match = new Button();
                match.Name = "match_" + i;
                match.Template = (ControlTemplate)Application.Current.Resources["matchButton"];
                match.Click += MatchButton_Click;
                match.MouseEnter += MatchButton_Enter;
                match.MouseLeave += MatchButton_Leave;
                matches.Children.Add(match);
            }
        }


        private void makeTurn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMatches.Count > 0)
            {

                saved = false;
                firstTurn = false;
                player1.IsEnabled = false;
                player2.IsEnabled = false;
                player2Save.Visibility = Visibility.Hidden;
                player1Save.Visibility = Visibility.Hidden;
                string winnerText = "";

                foreach (Button selectedMatch in selectedMatches)
                {
                    matches.Children.Remove(selectedMatch);
                }

                game.MakeTurn();

                switch (game.CurrentState)
                {
                    case GameState.Player1Win:
                        game.Player1.Win += 1;
                        game.Player2.Loose += 1;
                        gameState.Text = winnerText = game.Player1.Name + winsConstText;
                        break;
                    case GameState.Player2Win:
                        game.Player2.Win += 1;
                        game.Player1.Loose += 1;
                        gameState.Text = winnerText = game.Player2.Name + winsConstText;
                        break;
                    case GameState.Player1Turn:
                        gameState.Text = game.Player1.Name + makesTurnConstText;
                        break;
                    case GameState.Player2Turn:
                        gameState.Text = game.Player2.Name + makesTurnConstText;
                        break;
                }

                selectedMatches.Clear();

                if (game.CurrentState == GameState.Player1Win || game.CurrentState == GameState.Player2Win)
                {
                    Grid endGamePage = (Grid)this.Resources["endGamePage"];
                    TextBlock winner = (TextBlock)LogicalTreeHelper.FindLogicalNode(endGamePage, "winner");
                    winner.Text = winnerText;

                    player1Win.Text = game.Player1.Win.ToString();
                    player1Loose.Text = game.Player1.Loose.ToString();
                    player2Win.Text = game.Player2.Win.ToString();
                    player2Loose.Text = game.Player2.Loose.ToString();

                    mainGrid.Children.Add(endGamePage);
                    container.Effect = new BlurEffect();
                    this.FadeInAnimate(endGamePage);
                    this.BlurAnimate();

                    makeTurn.IsDefault = false;
                }
            }
        }

        private void FadeOutAnimate(Grid page)
        {
            DoubleAnimation fadeOut = new DoubleAnimation();
            fadeOut.From = 1;
            fadeOut.To = 0;
            fadeOut.Duration = TimeSpan.FromSeconds(0.5);
            fadeOut.Completed += FadeOut_Completed;
            page.BeginAnimation(Grid.OpacityProperty, fadeOut);
        }

        private void FadeInAnimate(Grid page)
        {
            DoubleAnimation fadeIn = new DoubleAnimation();
            fadeIn.From = 0;
            fadeIn.To = 1;
            fadeIn.Duration = TimeSpan.FromSeconds(0.5);
            fadeIn.Completed += FadeIn_Completed;
            page.BeginAnimation(Grid.OpacityProperty, fadeIn);
        }

        private void FadeIn_Completed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void SharpenAnimate()
        {
            DoubleAnimation sharpen = new DoubleAnimation();
            sharpen.From = 20;
            sharpen.To = 0;
            sharpen.Duration = TimeSpan.FromSeconds(0.5);
            sharpen.Completed += Sharpen_Completed;
            container.BeginAnimation(BlurEffect.RadiusProperty, sharpen);
        }

        private void BlurAnimate()
        {
            DoubleAnimation blur = new DoubleAnimation();
            blur.From = 0;
            blur.To = 20;
            blur.Duration = TimeSpan.FromSeconds(0.5);
            container.BeginAnimation(BlurEffect.RadiusProperty, blur);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            firstTurn = true;
            makeTurn.IsDefault = true;

            Grid aboutPage = (Grid)LogicalTreeHelper.FindLogicalNode(mainGrid, "aboutPage");
            this.FadeOutAnimate(aboutPage);
            this.SharpenAnimate();            
        }

        private void FadeOut_Completed(object sender, EventArgs e)
        {
            Grid aboutPage = (Grid)LogicalTreeHelper.FindLogicalNode(mainGrid, "aboutPage");
            Grid endGamePage = (Grid)LogicalTreeHelper.FindLogicalNode(mainGrid, "endGamePage");

            if (aboutPage != null)
                mainGrid.Children.Remove(aboutPage);
            else if (endGamePage != null)
                mainGrid.Children.Remove(endGamePage);
        }

        private void Sharpen_Completed(object sender, EventArgs e)
        {
            container.Effect = null;
        }

        private void MatchButton_Leave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            
        }

        private void MatchButton_Enter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            makeTurn.IsDefault = true;
            Grid endGamePage = (Grid)LogicalTreeHelper.FindLogicalNode(mainGrid, "endGamePage");
            this.FadeOutAnimate(endGamePage);
            this.SharpenAnimate();

            game.StartNew();
            firstTurn = true;
            player1.IsEnabled = true;
            player2.IsEnabled = true;
            gameState.Text = game.Player1.Name + makesTurnConstText;

            matches.Children.Clear();
            for (int i = 0; i < game.MaxMatchesCount; ++i)
            {
                Button match = new Button();
                match.Name = "match_" + i;
                match.Template = (ControlTemplate)Application.Current.Resources["matchButton"];
                match.Click += MatchButton_Click;
                match.MouseEnter += MatchButton_Enter;
                match.MouseLeave += MatchButton_Leave;
                matches.Children.Add(match);
            }
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!saved)
            {
                GameMessageWindow gmw = new GameMessageWindow("Сохранить текущую игру?", " Старт новой игры", GameButtons.YesNo);
                if (gmw.ShowDialog() == true)
                {
                    SaveGame();
                }
            }

            using (Stream writer = new FileStream("settings.xml", FileMode.Create))
            {
                serializerGame.Serialize(writer, game);
            }

            using (Stream writer = new FileStream("players.xml", FileMode.Create))
            {
                serializerPlayers.Serialize(writer, players);
            }
        }

        private void MatchButton_Click(object sender, RoutedEventArgs e)
        {
            saved = false;
            Button match = (Button)sender;

            ControlTemplate matchPressed = (ControlTemplate)Application.Current.Resources["matchPressed"];
            if (match.Template == matchPressed)
            {
                match.Template = (ControlTemplate)Application.Current.Resources["matchButton"];
                selectedMatches.Remove(match);

                int index = matches.Children.IndexOf(match);
                game.TakeMatchToggle(index);
            }
            else
            {
                if (selectedMatches.Count < game.MaxSelection)
                {
                    match.Template = matchPressed;
                    selectedMatches.Add(match);

                    int index = matches.Children.IndexOf(match);
                    game.TakeMatchToggle(index);
                }
            }
        }

        private void SettingsMenu_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(game.MaxMatchesCount, game.MaxSelection);
            if (settingsWindow.ShowDialog() == true)
            {
                game.MaxMatchesCount = settingsWindow.MaxMatches;
                game.MaxSelection = settingsWindow.MaxSelect;

                GameMessageWindow gmw = new GameMessageWindow("Начать новую игру с изменёнными ностройками?", "Изменение настроек", GameButtons.YesNo);
                if (gmw.ShowDialog() == true)
                {
                    game.StartNew();
                    gameState.Text = game.Player1.Name + makesTurnConstText;

                    matches.Children.Clear();
                    for (int i = 0; i < game.MaxMatchesCount; ++i)
                    {
                        Button match = new Button();
                        match.Name = "match_" + i;
                        match.Template = (ControlTemplate)Application.Current.Resources["matchButton"];
                        match.Click += MatchButton_Click;
                        match.MouseEnter += MatchButton_Enter;
                        match.MouseLeave += MatchButton_Leave;
                        matches.Children.Add(match);
                    }
                }
            }
        }

        private void Player1_TextChanged(object sender, TextChangedEventArgs e)
        {
            player1Save.Visibility = Visibility.Visible;
        }

        private void Player2_TextChanged(object sender, TextChangedEventArgs e)
        {
            player2Save.Visibility = Visibility.Visible;
        }

        private void SavePlayer2()
        {
            if (firstTurn)
            {
                if (player2.Text.Equals(player1.Text))
                {
                    GameMessageWindow gmw = new GameMessageWindow("Имена игроков не должны совпадать", "Смена игрока", GameButtons.Ok);
                    gmw.ShowDialog();
                    player2.Text = "";
                    return;
                }

                if (player2.Text == "")
                {
                    GameMessageWindow gmw = new GameMessageWindow("Имя не может быть пустым", "Смена игрока", GameButtons.Ok);
                    gmw.ShowDialog();
                    return;
                }

                Player currentPlayer = players.Find(p => p.Name.Equals(player2.Text));
                if (currentPlayer == null)
                {
                    currentPlayer = new Player(player2.Text);
                    players.Add(currentPlayer);
                }
                game.Player2 = currentPlayer;
                player2Win.Text = currentPlayer.Win.ToString();
                player2Loose.Text = currentPlayer.Loose.ToString();
            }

            player2Save.Visibility = Visibility.Hidden;
        }

        private void player2Save_Click(object sender, RoutedEventArgs e)
        {
            SavePlayer2();
        }

        private void SavePlayer1()
        {
            if (firstTurn)
            {
                if (player1.Text.Equals(player2.Text))
                {
                    GameMessageWindow gmw = new GameMessageWindow("Имена игроков не должны совпадать", "Смена игрока", GameButtons.Ok);
                    gmw.ShowDialog();
                    player1.Text = "";
                    return;
                }

                if (player1.Text == "")
                {
                    GameMessageWindow gmw = new GameMessageWindow("Имя не может быть пустым", "Смена игрока", GameButtons.Ok);
                    gmw.ShowDialog();
                    return;
                }

                Player currentPlayer = players.Find(p => p.Name.Equals(player1.Text));
                if (currentPlayer == null)
                {
                    currentPlayer = new Player(player1.Text);
                    players.Add(currentPlayer);
                }
                game.Player1 = currentPlayer;
                gameState.Text = currentPlayer.Name + makesTurnConstText;
                player1Win.Text = currentPlayer.Win.ToString();
                player1Loose.Text = currentPlayer.Loose.ToString();
            }
            player1Save.Visibility = Visibility.Hidden;
        }

        private void player1Save_Click(object sender, RoutedEventArgs e)
        {
            SavePlayer1();
        }

        private void surrender_Click(object sender, RoutedEventArgs e)
        {
            string winnerText = "";
            switch (game.CurrentState)
            {
                case GameState.Player1Turn:
                    game.CurrentState = GameState.Player2Win;
                    game.Player2.Win += 1;
                    game.Player1.Loose += 1;
                    gameState.Text = winnerText = game.Player2.Name + winsConstText;
                    break;
                case GameState.Player2Turn:
                    game.CurrentState = GameState.Player1Win;
                    game.Player1.Win += 1;
                    game.Player2.Loose += 1;
                    gameState.Text = winnerText = game.Player1.Name + winsConstText;
                    break;
            }

            selectedMatches.Clear();

            Grid endGamePage = (Grid)this.Resources["endGamePage"];
            TextBlock winner = (TextBlock)LogicalTreeHelper.FindLogicalNode(endGamePage, "winner");
            winner.Text = winnerText;

            player1Win.Text = game.Player1.Win.ToString();
            player1Loose.Text = game.Player1.Loose.ToString();
            player2Win.Text = game.Player2.Win.ToString();
            player2Loose.Text = game.Player2.Loose.ToString();

            mainGrid.Children.Add(endGamePage);
            container.Effect = new BlurEffect();
            this.FadeInAnimate(endGamePage);
            this.BlurAnimate();
        }

        private void player1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SavePlayer1();
            }
        }

        private void player2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SavePlayer2();
            }
        }
    }
}
