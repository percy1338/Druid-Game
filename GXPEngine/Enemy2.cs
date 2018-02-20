using System;
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

        private int _step;
        private int _animDrawsBetweenFrames = 5;
        private int _maxFramesInAnim = 2;
        

        private Player _player;
        Projectile bullet;

        public Enemy2(int frame, Map map, int index, Player player) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);
            _player = player;

            for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
            {
                if (map.objGroup.TiledObject[index].properties.property[i].name == "damage")
                {
                    _dmg = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
            }
        }

        public void Update()
        {
            _step = _step + 1;
            if (_step > _animDrawsBetweenFrames)
            {
                NextFrame();
                _step = 0;

                if (currentFrame > _maxFramesInAnim)
                {
                    SetFrame(-1);
                }
            }
           // ShootPlayer();
            _cooldown--;
        }

        void ShootPlayer()
        {
            float deltaX = _player.x - this.x;
            float deltaY = _player.y - this.y;
            float length = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);

            float velocityX = deltaX / length;
            float velocityY = deltaY / length;

            if (length <= _range)
            {
                if (_cooldown <= 0)
                {
                    bullet = new Projectile(_player);
                    parent.AddChild(bullet);
                    //_shootingSound.Play();
                    _cooldown = 60;
                }
            }
        }
    }
}
