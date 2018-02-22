using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class MainMenu : Sprite
    {
        private Sprite _startButton = new Sprite("HUD/start.png");
        private Sprite _infoButton = new Sprite("HUD/info.png");
        private Sprite _quitButton = new Sprite("HUD/quit.png");
        private MyGame _mygame;

        private int _width = 1600;
        private int _height = 900;

        public MainMenu(MyGame mygame) : base("HUD/menu.png")
        {
            _mygame = mygame;
            this.width = game.width;
            this.height = game.height;

            _startButton.x = game.width * 0.55f;
            _startButton.y = _height * 0.75f;

            _infoButton.x = game.width * 0.55f;
            _infoButton.y = _height * 0.85f;

            _quitButton.x = game.width * 0.55f;
            _quitButton.y = _height * 0.95f;

            AddChild(_startButton);
            AddChild(_infoButton);
            AddChild(_quitButton);
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
                if (_quitButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    game.Destroy();
                }
                if (_infoButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    WinScreen win = new WinScreen();
                    game.AddChild(win);
                    this.Destroy();
                }
            }
        }

        private void StartGame()
        {
            _mygame.generateLevel();
            Destroy();
        }
    }

    public class GameOver : Sprite
    {
        public GameOver() : base("HUD/gameover.png")
        {
            this.width = game.width;
            this.height = game.height;
        }
    }
    public class WinScreen : Sprite
    {
        public WinScreen() : base("HUD/winscreen.png")
        {
            this.width = game.width;
            this.height = game.height;
        }
    }

    public class InfoScreen : Sprite
    {
        public InfoScreen() : base("HUD/info page.png")
        {
            this.width = game.width;
            this.height = game.height;
        }
    }


}
