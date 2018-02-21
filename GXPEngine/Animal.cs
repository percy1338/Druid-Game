using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Animal : AnimationSprite , IActivatable
    {
        private int _step;
        private int _animDrawsBetweenFrames = 5;
        int _maxFramesInAnim = 7;
        bool playAnim;
        int timer;

        public Animal(Map map) : base("Sprites/Animal_Sheet.png", 4,2,-1)
        {
            SetFrame(8);
        }

        public void Activateble(Player player)
        {
            playAnim = true;
        }

        public void Update()
        {
            if(playAnim)
            {
                _step = _step + 1;
                if (_step > _animDrawsBetweenFrames)
                {
                    NextFrame();
                    _step = 0;

                    if (currentFrame == _maxFramesInAnim)
                    {
                        playAnim = false;
 
                            this.Destroy();
                        
                    }
                }
            }
        }
    }
}
