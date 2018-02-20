using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Enemy : AnimationSprite
    {
		//
        private float _speedX = 5;
        private float _speedY;
        private float _dmg;
        private bool _turn = false;

        private Player _player;

        private int _step;
        private int _animDrawsBetweenFrames = 5;
        int _maxFramesInAnim = 1;

        public Enemy(int frame, Map map, int index) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
		{
            SetFrame(frame - map.tileSet.firstGid);
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
                movement();
            }
        }

        private void movement()
        {
            this.x += _speedX;
            checkCollision(_speedX, 0);
            if (_turn)
            {
                _speedX = -5;
            }
            if (!_turn)
            {
                _speedX = 5;
            }
            this.y += _speedY;
            checkCollision(0, _speedY);
        }

        public void Death()
        {
            SetFrame(2);
            this.Destroy();
        }

        private void checkCollision(float mx, float my)
        {
            GameObject[] others = GetCollisions();//get all objects player is coliding with and puts it in a list
            foreach (GameObject other in others)
            {
                if (other is Tiles)//check if object is tile
                {
                    this.x -= mx;//return to previous non coliding position
                    this.y -= my;
                    _turn = !_turn;
                    if (mx > 0)
                    {
                        if (_turn)
                        {
                            _speedX = 5;
                            Mirror(false, false);
                        }
                    }
                    else
                    {
                        Mirror(true, false);
                    }
                    return;
                }
                if (other is Tiles)
                {
                    Console.WriteLine("invisabld block hit " + mx);
                    this.x -= mx;//return to previous non coliding position
                    this.y -= my;
                    _turn = !_turn;
                    if (mx > 0)
                    {
                        if (_turn)
                        {
                            _speedX = 5;
                            Mirror(false, false);
                        }
                    }
                    else
                    {
                        Mirror(true, false);
                    }
                    return;
                }
            }
        }
    }
}
