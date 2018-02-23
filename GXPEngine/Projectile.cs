using System;
namespace GXPEngine
{
	public class Projectile : Sprite
	{
		public Vec2 _position;
		public Vec2 _velocity;
		public Vec2 _gravity;
		private Player _player;

		//values
		private float _speed;
		private int _timer;
		private float _bouncinessX = 0.98f;
		private float _bouncinessY = 0.8f;

		public Projectile(Player player) : base("Sprites/PlayerProjectile.png")
		{
			_velocity = Vec2.zero;
			_position = Vec2.zero;
			_gravity = Vec2.zero;

			_player = player;

			if (_player.left == true)
			{
				this.x = _player._hitbox.x - 48;
				this.y = _player._hitbox.y - 64;
				_position.x += _player._hitbox.x - 48;
				_position.y += _player._hitbox.y - 64;
				_speed = -10;
			}

			if (_player.left == false)
			{
				this.x = _player._hitbox.x + 48;
				this.y = _player._hitbox.y - 64;
				_position.x += _player._hitbox.x + 48;
				_position.y += _player._hitbox.y - 64;
				_speed = 10;
			}

			this.SetOrigin(width / 2, height);
			_velocity.x = _speed;
			_velocity.y = 1;

			_gravity.y = 1;

		}
		public void Update()
		{
			updatePosition();

			_velocity.Add(_gravity);

			TimerProjectile();
			ProjectileCollision();
			enemyCollision();

		}

		private void ProjectileCollision()
		{

			GameObject TiledObject;
			int directionY;


			this.x = _position.x + _velocity.x;

			TiledObject = Level.Return().CheckCollision(this);

			if (TiledObject != null)
			{
				_velocity.x *= -1;
				_velocity.x *= _bouncinessX;

			}

			x = _position.x - _velocity.x;

			//Y-Collision


			this.y = _position.y + _velocity.y;
			TiledObject = Level.Return().CheckCollision(this);

			if (TiledObject != null)
			{
				directionY = _velocity.y > 0 ? 1 : -1;

				if (directionY == 1)
				{
					_velocity.y *= -1;
					_velocity.y *= _bouncinessY;
				}

				if (directionY == -1)
				{
					_velocity.y = 0;
				}
			}

			y = _position.y - _velocity.y;

			_position.Add(_velocity);
		}


		private void enemyCollision()
		{
			GameObject[] others = GetCollisions();//get all objects player is coliding with and puts it in a list
			foreach (GameObject other in others)
			{
				if (other is Enemy)// check if colliderd object is an player and if the enemy shot the bullet
				{
					((Enemy)other).Hit();
					this.Destroy();
				}
				if (other is Enemy2)
				{
					((Enemy2)other).Hit();
					this.Destroy();
				}
				if (other is EnemyBullet)
				{
					((EnemyBullet)other).Hit();
				}

			}
		}


		private void updatePosition()
		{
			x = _position.x;
			y = _position.y;
		}

		private void TimerProjectile()
		{
			_timer++;
			if (_timer > 120)
			{
				this.Destroy();
			}
		}
	}
}