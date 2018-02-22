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
            Healthbar healthbar = new Healthbar(this, _player);
            AddChild(healthbar);
        }

        public void Update()
        {
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
    public class Healthbar : Sprite
    {
        private HUD _hud;
        private Player _player;

        public Healthbar(HUD hud, Player player) : base("HUD/Human.png")
        {
            _player = player;
            _hud = hud;

           // this.x -= this.width;
           // this.y += this.height * 0.5f;

        }
    }
}
