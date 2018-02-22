using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class HUD :AnimationSprite
    {
        private Level _level;
        private Player _player;
        private int offsetx = -50;
        private int offsety = -100;

        private int _step;
        private int _animDrawsBetweenFrames = 5;
        private int _maxFramesInAnim = 2;
        public bool TakeDamage = false;

        public HUD(Level level, Player player) : base("HUD/HUD HP.png",1,3,-1)
        {
            _player = player;
            _level = level;
            this.scale = 0.30f;
            FormUI fui = new FormUI(this, _player);
            AddChild(fui);
        }

        public void Update()
        {
            if(_player.GetHP() == 0.0f)
            {

            }

            if (TakeDamage)
            {
                _step = _step + 1;
                if (_step > _animDrawsBetweenFrames)
                {
                    NextFrame();
                    _step = 0;

                    if (currentFrame > _maxFramesInAnim)
                    {
                        GameOver go = new GameOver();
                        game.AddChild(go);
                    }
                }
                TakeDamage = false;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////
    public class FormUI : Sprite
    {
        private HUD _hud;
        private Player _player;

        public FormUI(HUD hud, Player player) : base("HUD/Human.png")
        {
            _player = player;
            _hud = hud;

            this.x += 20;
            this.y += 110;
        }

        public void Update()
        {
            if(_player.GetShape() == Player.Shape.Snake)
            {
                this.texture.Load("HUD/Snake.png");
            }
            if (_player.GetShape() == Player.Shape.Human)
            {
                this.texture.Load("HUD/Human.png");
            }
            if (_player.GetShape() == Player.Shape.Bird)
            {
                this.texture.Load("HUD/Eagle.png");
            }
            if (_player.GetShape() == Player.Shape.Bear)
            {
                this.texture.Load("HUD/Bear.png");
            }
        }
    }
}
