using CocosSharp;
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
        public event EventHandler OnTouched;
        public event EventHandler OnReset;
        public event EventHandler OnQuit;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            GameLayer = new GameLayer(gameView.DesignResolution.Width, gameView.DesignResolution.Height);
            GameLayer.OnTouched += (s, e) => OnTouched?.Invoke(s, e);
            GameLayer.OnReset += (s, e) => OnReset?.Invoke(s, e);
            GameLayer.OnQuit += (s, e) => OnQuit?.Invoke(s, e);
            AddLayer(GameLayer);
        }

        public void MoveBall(int x, int y)
        {
            GameLayer.MoveBall(x, y);
        }
    }
}
