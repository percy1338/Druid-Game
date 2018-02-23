using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class MainMenu : Sprite
    {
        private Sound _backgroundMusic = new Sound("audio/Realm-of-Fantasy.mp3", true, true);
        private SoundChannel _backgroundChanel;

        private Sprite _startButton = new Sprite("HUD/start.png");
        private Sprite _infoButton = new Sprite("HUD/info.png");
        private Sprite _quitButton = new Sprite("HUD/quit.png");

        private MyGame _mygame;

        public MainMenu(MyGame mygame) : base("HUD/menu.png")
        {
            _backgroundChanel = _backgroundMusic.Play();
            _backgroundChanel.Volume = 0.5f;

            _mygame = mygame;

            this.width = game.width;
            this.height = game.height;

            _startButton.x = game.width * 0.35f;
            _startButton.y = game.height * 0.75f;

            _infoButton.x = game.width * 0.55f;
            _infoButton.y = game.height * 0.75f;

            _quitButton.x = game.width * 0.75f;
            _quitButton.y = game.height * 0.75f;

            AddChild(_startButton);
            AddChild(_infoButton);
            AddChild(_quitButton);
        }

        public void Update()
        {
            this.width = game.width;
            this.height = game.height;

            if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _startButton.texture.Load("HUD/start selected.png");
            }
            else
            {
                _startButton.texture.Load("HUD/start.png");
            }


            if (_quitButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _quitButton.texture.Load("HUD/quit selected.png");
            }
            else
            {
                _quitButton.texture.Load("HUD/quit.png");
            }

            if (_infoButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _infoButton.texture.Load("HUD/info selected.png");
            }
            else
            {
                _infoButton.texture.Load("HUD/info.png");
            }

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
                    InfoScreen info = new InfoScreen(_mygame);
                    game.AddChild(info);

                    _backgroundChanel.Stop();
                    _backgroundMusic = null;
                    _backgroundChanel = null;
                    this.Destroy();
                }
            }
        }

        private void StartGame()
        {
            _mygame.generateLevel();
            _backgroundChanel.Stop();
            _backgroundMusic = null;
            _backgroundChanel = null;
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
        private Sound _backgroundMusic = new Sound("audio/Realm-of-Fantasy.mp3", true, true);
        private SoundChannel _backgroundChanel;

        private Sprite _startButton = new Sprite("HUD/start.png");
        private Sprite _quitButton = new Sprite("HUD/quit.png");

        private int _width = 1600;
        private int _height = 900;

        private MyGame _mygame;

        public InfoScreen(MyGame mygame) : base("HUD/info page.png")
        {
            _backgroundChanel = _backgroundMusic.Play();
            _backgroundChanel.Volume = 0.5f;

            _mygame = mygame;
            this.width = game.width;
            this.height = game.height;

            _startButton.x = _width * 0.75f;
            _startButton.y = _height * 0.75f;

            _quitButton.x = _width * 0.75f;
            _quitButton.y = _height * 0.90f;

            AddChild(_startButton);
            AddChild(_quitButton);
        }

        public void Update()
        {
            this.width = game.width;
            this.height = game.height;

            if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _startButton.texture.Load("HUD/start selected.png");
            }
            else
            {
                _startButton.texture.Load("HUD/start.png");
            }


            if (_quitButton.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _quitButton.texture.Load("HUD/quit selected.png");
            }
            else
            {
                _quitButton.texture.Load("HUD/quit.png");
            }

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
            }
        }

        private void StartGame()
        {
            _mygame.generateLevel();
            _backgroundChanel.Stop();
            _backgroundMusic = null;
            _backgroundChanel = null;
            this.Destroy();
        }
    }
}
