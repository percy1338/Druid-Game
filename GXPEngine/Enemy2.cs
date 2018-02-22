﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Enemy2 : AnimationSprite
    {
		//
        private float _range = 300;
        private float _cooldown = 60;
        private int _dmg;
        private bool _shootLeft;
        private int _step;
       

        EnemyBullet bullet;
        private Player _player;
        

        public Enemy2(int frame, Map map, int index, Player player) : base("sprites/EnemySprite.png", 3, 1, -1)
        {
            SetFrame(0);
			SetOrigin(width / 2, height / 2);
			SetScaleXY(1.75f, 1.75f);
            _player = player;

            for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
            {
                if (map.objGroup.TiledObject[index].properties.property[i].name == "damage")
                {
                    _dmg = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
                if (map.objGroup.TiledObject[index].properties.property[i].name == "shootLeft")
                {
                    _shootLeft = bool.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
            }
        }

        public void Update()
        {
			_step += 1;
            if (_step > 15)
            {
                NextFrame();
                _step = 0;

                if (currentFrame == 3)
                {
                    SetFrame(0);
                }
            }
            ShootPlayer();
            _cooldown--;
        }

        void ShootPlayer()
        {
                if (_cooldown <= 0)
                {
                    bullet = new EnemyBullet(this.x,this.y, _shootLeft);
                    parent.AddChild(bullet);
                    
                    _cooldown = 120;
                }
        }
    }
}
