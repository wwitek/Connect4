using Connect4.API;
using Connect4.Domain.Entities;
using Connect4.Domain.Enums;
using Connect4.Domain.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Connect4.Tester.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _board;
        private string _status;
        private string _column;
        public string Board
        {
            get { return _board; }
            set { SetProperty(ref _board, value); }
        }
        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }
        public string Column
        {
            get { return _column; }
            set { SetProperty(ref _column, value); }
        }
        public ICommand StartSinglePlayer { get; set; }
        public ICommand StartTwoPlayers { get; set; }
        public ICommand StartOnline { get; set; }
        public ICommand Move { get; set; }
        public ICommand Reset { get; set; }
        public ICommand Quit { get; set; }

        private GameAPI Api { get; }
        private IField[,] Fields { get; set; }

        public MainWindowViewModel(GameAPI api)
        {
            InitFields();
            Api = api;
            Api.OnMoveMade += Api_OnMoveMade;
            Board = DrawBoard();

            Status = "";
            Column = "1";

            StartSinglePlayer = new DelegateCommand(OnStartSinglePlayer, CanStartSinglePlayer);
            StartTwoPlayers = new DelegateCommand(OnStartTwoPlayers, CanStartTwoPlayers);
            StartOnline = new DelegateCommand(OnStartOnline, CanStartOnline);
            Move = new DelegateCommand(OnMove, CanMove);
            Reset = new DelegateCommand(OnReset, CanReset);
            Quit = new DelegateCommand(OnQuit, CanQuit);
        }

        private void Api_OnMoveMade(object sender, Domain.EventArguments.MoveEventArgs e)
        {
            Fields[e.Move.Row, e.Move.Column].PlayerId = e.Move.PlayerId;
            Board = DrawBoard();

            if (e.Move.IsWinner) Status = $"Player {e.Move.PlayerId} won!";
            if (e.Move.IsDraw) Status = "It's a draw!";
        }

        private void InitFields()
        {
            Fields = new IField[6, 7];
            for (int i = 0; i < Fields.GetLength(0); i++)
            {
                for (int j = 0; j < Fields.GetLength(1); j++)
                {
                    Fields[i, j] = new Field(i, j);
                }
            }
        }
        private string DrawBoard()
        {
            string toReturn = "";
            string line = "";
            for (int i = 0; i < Fields.GetLength(0); i++)
            {
                for (int j = 0; j < Fields.GetLength(1); j++)
                {
                    line += $" {Fields[i, j].PlayerId} ";
                }
                toReturn += line + Environment.NewLine;
                line = "";
            }
            return toReturn;
        }

        public bool CanStartSinglePlayer()
        {
            return true;
        }
        public bool CanStartTwoPlayers()
        {
            return true;
        }
        public bool CanStartOnline()
        {
            return true;
        }
        public bool CanMove()
        {
            return true;
        }
        public bool CanReset()
        {
            return true;
        }
        public bool CanQuit()
        {
            return true;
        }

        public void OnStartSinglePlayer()
        {
            Api.Start(GameType.SinglePlayer);
        }
        public void OnStartTwoPlayers()
        {
            Api.Start(GameType.TwoPlayers);
        }
        public void OnStartOnline()
        {
            Api.Start(GameType.Online);
        }
        public void OnMove()
        {
            Api.TryMove(Convert.ToInt32(Column));
        }
        public void OnReset()
        {
            InitFields();
            Board = DrawBoard();
            Status = "";
        }
        public void OnQuit()
        {

        }
    }
}
