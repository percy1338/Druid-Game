﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Animal : AnimationSprite , IActivatable
    {
        private Sound _backgroundMusic = new Sound("audio/332629__treasuresounds__item-pickup.ogg", false, true);
        private SoundChannel _backgroundChanel;
        private bool audioPlayed = false;

        private int _step;
        private int _animDrawsBetweenFrames = 5;
        int _maxFramesInAnim = 7;
        bool playAnim;

        

        public Animal(Map map) : base("Sprites/Animal_Sheet.png", 4,2,-1)
        {

            SetFrame(8);
        }

        public void Activateble(Player player)
        {
            if(!audioPlayed)
            {
                _backgroundChanel = _backgroundMusic.Play();
                _backgroundChanel.Volume = 0.5f;
                audioPlayed = true;
            }


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
