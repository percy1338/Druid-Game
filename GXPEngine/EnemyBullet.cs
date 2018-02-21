using System;

namespace GXPEngine
{
    public class EnemyBullet : Sprite
    {
        private float _speed = 15;
        private float _velocityX, _velocityY;
        private float _lifetime = 120;
        private bool _shootleft;

        public EnemyBullet(float x, float y, bool shootleft) : base("Sprites/colors.png")
        {
            this.x = x;
            this.y = y;
            _shootleft = shootleft;
        }

        public void Update()
        {
            if(_shootleft)
            {
                this.x -= 5;
            }
            else
            {
                this.x += 5;
            }

            GameObject[] others = GetCollisions();//get all objects player is coliding with and puts it in a list
            foreach (GameObject other in others)
            {
                if (other is Tiles)
                {
                    this.Destroy();
                }
                if (other is Player)// check if colliderd object is an player and if the enemy shot the bullet
                {
                   // ((Player)other).SubtractLives();
                    this.Destroy();
                }
            }
            if (_lifetime <= 0)
            {
                this.Destroy();
            }
            _lifetime--;
        }
    }
}
