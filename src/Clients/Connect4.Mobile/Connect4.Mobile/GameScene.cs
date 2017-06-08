using CocosSharp;
using Connect4.Mobile.Enums;
using Connect4.Mobile.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile
{
    public class GameScene : CCScene
    {
        private GameLayer GameLayer { get; }
        public event EventHandler OnPreTouched;
        public event EventHandler OnTouched;

        public GameScene(CCGameView gameView, Dimensions dimensions, Colors colors) : base(gameView)
        {
            GameLayer = new GameLayer(gameView.DesignResolution.Width, gameView.DesignResolution.Height, dimensions, colors);
            GameLayer.OnPreTouched += (s, e) => OnPreTouched?.Invoke(s, e);
            GameLayer.OnTouched += (s, e) => OnTouched?.Invoke(s, e);
            AddLayer(GameLayer);
        }

        public void PreTouch(PlayerColor player, int column)
        {
            GameLayer.PreTouch(player, column);
        }

        public void MoveBall(PlayerColor player, int x, int y)
        {
            GameLayer.MoveBall(player, x, y);
        }
    }
}
