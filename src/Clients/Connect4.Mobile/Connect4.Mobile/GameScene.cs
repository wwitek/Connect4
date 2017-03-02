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
        private CCLayer _layer;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            _layer = new GameLayer(gameView.DesignResolution.Width, gameView.DesignResolution.Height);
            AddLayer(_layer);
        }
    }
}
