using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public enum GameState : int { Player1Turn, Player2Turn, Player1Win, Player2Win }

    [Serializable]
    public class Game
    {
        public Matches MatchesList { get; set; }
        public int MaxMatchesCount { get; set; }
        public int MaxSelection { get; set; }
        public GameState CurrentState { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Game()
        {
            CurrentState = GameState.Player1Turn;
        }

        public Game(int newMatchesCount, int newMaxSelection)
        {
            MaxMatchesCount = newMatchesCount;
            MaxSelection = newMaxSelection;
            CurrentState = GameState.Player1Turn;
        }

        public void StartNew()
        {
            CurrentState = GameState.Player1Turn;
            MatchesList = new Matches(MaxMatchesCount);
            for (int i = 0; i < MaxMatchesCount; ++i)
                MatchesList.Items.Add(new Match());
        }

        public void TakeMatchToggle(int index)
        {
            MatchesList.TakeMatchToggle(index);
        }

        public void MakeTurn() {
            MatchesList.RemoveTaken();

            GameState turn = CurrentState;
            int remainCount = MatchesList.Items.Count;
            GameState whoWin, whoLoose, whoNext;

            switch (turn)
            {
                case GameState.Player1Turn:
                    whoWin = GameState.Player1Win;
                    whoLoose = GameState.Player2Win;
                    whoNext = GameState.Player2Turn;
                    break;
                case GameState.Player2Turn:
                    whoWin = GameState.Player2Win;
                    whoLoose = GameState.Player1Win;
                    whoNext = GameState.Player1Turn;
                    break;
                default:
                    whoWin = GameState.Player1Win;
                    whoLoose = GameState.Player2Win;
                    whoNext = GameState.Player2Turn;
                    break;
            }

            if (remainCount == 1)
            {
                CurrentState = whoWin;
            }
            else if (remainCount == 0)
                CurrentState = whoLoose;
            else
                CurrentState = whoNext;
        }
    }
}
