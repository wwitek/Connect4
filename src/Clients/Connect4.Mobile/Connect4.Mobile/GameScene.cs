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
        private GameLayer _layer;

        public event EventHandler OnTouched;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            _layer = new GameLayer(gameView.DesignResolution.Width, gameView.DesignResolution.Height);
            _layer.OnTouched += (s, e) =>
            {
                OnTouched?.Invoke(s, e);
            };
            AddLayer(_layer);
        }

        public void MoveBall(int x, int y)
        {
            _layer.MoveBall(x, y);
        }
    }
}
