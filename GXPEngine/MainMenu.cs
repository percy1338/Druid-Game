using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class MainMenu : Sprite
    {
        private Sprite _startButton = new Sprite("HUD/start.png");
        private MyGame _mygame;

        private int _width = 1600;
        private int _height = 900;

        public MainMenu(MyGame mygame) : base("HUD/menu.png")
        {
            _mygame = mygame;
            this.width = game.width;
            this.height = game.height;

            _startButton.x = game.width * 0.5f - (_startButton.width * 0.5f);
            _startButton.y = game.height * 0.5f - (_startButton.height * 0.25f);

            AddChild(_startButton);
        }

        public void Update()
        {
            this.width = game.width;
            this.height = game.height;

            if (Input.GetMouseButtonDown(0))
            {
                if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    StartGame();
                }
            }
        }

        private void StartGame()
        {
           // Level level = new Level((MyGame)this.parent, _width, _height);
           // game.AddChild(level);
            _mygame.generateLevel();

            Destroy();
        }
    }

    public class GameOver : Sprite
    {
        public GameOver() : base("HUD/gameover.png")
        {

        }
    }
    public class WinScreen : Sprite
    {
        public WinScreen() : base("HUD/winscreen.png")
        {

        }
    }

    public class InfoScreen : Sprite
    {
        public InfoScreen() : base("HUD/info page.png")
        {

        }
    }


}
