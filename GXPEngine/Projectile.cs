using System;
namespace GXPEngine
{
	public class Projectile : Sprite
	{
		public Vec2 _position;
		public Vec2 _velocity;
		private Player _player;

		//values
		private float _speed;
		private bool _lastLeft;
		private int _timer;

		public Projectile(Player player) : base("Sprites/checkers.png")
		{
			_velocity = Vec2.zero;
			_position = Vec2.zero;

			_player = player;

			if (player._velocity.x < 0 || _lastLeft == true)
			{
				this.x = _player._hitbox.x - 48;
				this.y = _player._hitbox.y - 32;
				_position.x += _player._hitbox.x - 48;
				_position.y += _player._hitbox.y - 32;
				_speed = -5;
				_lastLeft = true;
			}

			if (player._velocity.x > 0 || _lastLeft == false)
			{
				this.x = _player._hitbox.x + 48;
				this.y = _player._hitbox.y - 32;
				_position.x += _player._hitbox.x + 48;
				_position.y += _player._hitbox.y - 32;
				_speed = 5;
				_lastLeft = false;
			}

			this.SetOrigin(width / 2, height);
			_velocity.x = _speed;
			_velocity.y = 1;
		}
		public void Update()
		{
			TimerProjectile();
			ProjectileCollision();

			//ProjectilePhysics();
		}

		private void ProjectileCollision()
		{

			int direction;
			GameObject TiledObject;

			//X - collision;

			_position.x += _velocity.x;
			this.x = _position.x;

			TiledObject = Level.Return().CheckCollision(this);

			if (TiledObject != null)
			{
				Console.WriteLine("nani");
				direction = _velocity.x > 0 ? -1 : 1;
				if (direction == -1)
				{
					_position.x = TiledObject.x - width / 2;
					_velocity.x *= -1;
				}

				if (direction == 1)
				{
					_position.x = TiledObject.x + 64 + width / 2;
					_velocity.x *= -1;
				}
			}

			x = _position.x - _velocity.x;



			// y - collision;
			_position.y += _velocity.y;
			this.y = _position.y;

			TiledObject = Level.Return().CheckCollision(this);

			if (TiledObject != null)
			{
				direction = _velocity.y > 0 ? -1 : 1;

				Console.WriteLine("ultranani");
				if (direction == 1)
				{
					_position.y = TiledObject.y + height + 64;
					_velocity.y *= -1;
				}

				if (direction == -1)
				{
					_position.y = TiledObject.y;
					_velocity.y *= -1;
				}


			}

			_position.y += _velocity.y;
			y = _position.y;


		}


		private void TimerProjectile()
		{
			_timer++;
			if (_timer > 240)
			{
				this.Destroy();
			}
		}
	}
}
