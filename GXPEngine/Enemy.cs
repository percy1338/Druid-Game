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
        private float _dmg;
        private bool _turn = false;
        private int _step;
        private int _animDrawsBetweenFrames = 10;

        public Enemy(int frame, Map map, int index) : base("sprites/EnemySprite.png", 3, 1, -1)
		{
			this.SetOrigin(width / 2, height / 2 + 28);
			this.SetScaleXY(1.75f, 1.75f);
			SetFrame(0);
            this.Mirror(true, false);

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

                if (currentFrame == 2)
                {
                    SetFrame(0);
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
        }

        public void Death()
		{
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
				if (other is Hitbox)
				{
					((Hitbox)other).GetHit(1);
				}
            }
        }

		public void Hit()
		{
			this.Destroy();
		}
    }
}
