using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class HUD :Sprite
    {
        private Level _level;
        private int offsetx = -50;
        private int offsety = -100;

        public HUD(Level level) : base("HUD/HUD (1).png")
        {
            _level = level;
            this.scale = 0.30f;
            this.x = offsetx;
            this.y = offsety;

            Healthbar healthbar = new Healthbar(this);
            AddChild(healthbar);
        }

        public void Update()
        {
            this.x = -_level.x + offsetx;
            this.y = -_level.y + offsety;
        }
    }

    public class Healthbar : AnimationSprite
    {
        private int _step;
        private int _animDrawsBetweenFrames = 5;
        private int _maxFramesInAnim = 2;
        public bool TakeDamage = false;

        private HUD _hud;
        public Healthbar(HUD hud) : base("HUD/HUD Bar.png",1,1,-1)
        {
            _hud = hud;
        }

        public void Update()
        {
            if(TakeDamage)
            {
                _step = _step + 1;
                if (_step > _animDrawsBetweenFrames)
                {
                    NextFrame();
                    _step = 0;

                    if (currentFrame > _maxFramesInAnim)
                    {
                        //gameover
                    }
                }
                TakeDamage = false;
            }
        }
    }
}
