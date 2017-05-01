using Connect4.API;
using Connect4.Domain.Factories;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using Connect4.Tester.ViewModels;
using Connect4.Tester.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Connect4.Domain.AI;

namespace Connect4.Tester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IBoardEvaluation eval = new BasicBoardEvaluation();
            AlphaBeta alphaBetaSearch = new AlphaBeta(eval);
            IterativeDeepeningSearch interDeepeningSearch = new IterativeDeepeningSearch(alphaBetaSearch);

            IFieldFactory fieldFactory = new FieldFactory();
            IBoardFactory boardFactory = new BoardFactory(fieldFactory);
            IPlayerFactory playerFactory = new PlayerFactory(interDeepeningSearch);
            IGameFactory gameFactory = new GameFactory(boardFactory);

            GameAPI api = new GameAPI(gameFactory, playerFactory);
            MainWindow window = new MainWindow();
            window.DataContext = new MainWindowViewModel(api);
            window.Show();
        }
    }
}
