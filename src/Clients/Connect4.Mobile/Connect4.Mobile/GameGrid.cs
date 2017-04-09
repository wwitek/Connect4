using Connect4.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Connect4.Mobile
{
    public class GameGrid : Grid
    {
        public event EventHandler OnCreated;
        public event EventHandler OnTouched;
        public event EventHandler OnReset;
        public event EventHandler OnQuit;
        public GameCocosSharpView GameView { get; }

        public GameGrid()
        {
            //var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            //tgr.Tapped += Tgr_Tapped;
            //this.GestureRecognizers.Add(tgr);

            GameView = new GameCocosSharpView();
            GameView.OnCreated += (s, e) => OnCreated?.Invoke(s, e);
            GameView.OnTouched += (s, e) => OnTouched?.Invoke(s, e);
            GameView.OnReset += (s, e) => OnReset?.Invoke(s, e);
            GameView.OnQuit += (s, e) => Tgr_Tapped(s, e);
            Children.Add(GameView);
        }

        private void Tgr_Tapped(object sender, EventArgs e)
        {
            OnQuit?.Invoke(sender, e);
        }

        public void MoveBall(PlayerColor player, int x, int y)
        {
            GameView.MoveBall(player, x, y);
        }
    }
}