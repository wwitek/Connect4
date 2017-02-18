using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Connect4.Mobile.Pages
{
    public partial class HomePage : ContentPage
    {
        GameScene gameScene;

        public HomePage()
        {
            InitializeComponent();

            // This is the top-level grid, which will split our page in half
            var grid = new Grid();
            this.Content = grid;
            grid.RowDefinitions = new RowDefinitionCollection {
                // Each half will be the same size:
                new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
                new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)},
            };
            CreateTopHalf(grid);
            CreateBottomHalf(grid);
        }

        void CreateTopHalf(Grid grid)
        {
            // This hosts our game view.
            var gameView = new CocosSharpView()
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = HandleViewCreated
            };
            // We'll add it to the top half (row 0)
            grid.Children.Add(gameView, 0, 0);
        }

        void CreateBottomHalf(Grid grid)
        {
            // We'll use a StackLayout to organize our buttons
            var stackLayout = new StackLayout();

            // The first button will move the circle to the left when it is clicked:
            var moveLeftButton = new Button
            {
                Text = "Move Circle Left"
            };
            moveLeftButton.Clicked += (sender, e) => gameScene.MoveCircleLeft();
            stackLayout.Children.Add(moveLeftButton);

            // The second button will move the circle to the right when clicked:
            var moveCircleRight = new Button
            {
                Text = "Move Circle Right"
            };
            moveCircleRight.Clicked += (sender, e) => gameScene.MoveCircleRight();
            stackLayout.Children.Add(moveCircleRight);

            // The stack layout will be in the bottom half (row 1):
            grid.Children.Add(stackLayout, 0, 1);
        }

        void HandleViewCreated(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                // This sets the game "world" resolution to 100x100:
                gameView.DesignResolution = new CCSizeI(100, 100);
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new GameScene(gameView);
                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);
            }
        }
    }

    public class GameScene : CCScene
    {
        CCDrawNode circle;
        public GameScene(CCGameView gameView) : base(gameView)
        {
            var layer = new CCLayer();
            this.AddLayer(layer);
            circle = new CCDrawNode();
            layer.AddChild(circle);
            circle.DrawCircle(
                // The center to use when drawing the circle,
                // relative to the CCDrawNode:
                new CCPoint(0, 0),
                radius: 15,
                color: CCColor4B.White);
            circle.PositionX = 20;
            circle.PositionY = 50;
        }

        public void MoveCircleLeft()
        {
            circle.PositionX -= 10;
        }

        public void MoveCircleRight()
        {
            circle.PositionX += 10;
        }
    }
}
