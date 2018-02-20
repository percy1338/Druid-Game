using System;
namespace GXPEngine
{
    public class Bullet : Sprite
    {
        //private Sound _playerHit = new Sound();
		//

        private float _speed = 15;
        private float _velocityX, _velocityY;
        private float _lifetime = 120;
 

        public Bullet(float x, float y, float velocityX, float velocityY) : base("Sprites/Color.png")
        {
            this.x = x;
            this.y = y;
            _velocityX = velocityX * _speed;
            _velocityY = velocityY * _speed;
        }

        void Update()
        {
            x += _velocityX;
            y += _velocityY;

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
