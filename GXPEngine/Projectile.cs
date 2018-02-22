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
		private bool _lastLeft;
		private int _timer;
		private float _bouncinessX = 0.98f;
		private float _bouncinessY = 0.8f;

		public Projectile(Player player) : base("Sprites/PlayerProjectile.png")
		{
			_velocity = Vec2.zero;
			_position = Vec2.zero;
			_gravity = Vec2.zero;

			_player = player;

			if (player._velocity.x < 0 || _lastLeft == true)
			{
				this.x = _player._hitbox.x - 48;
				this.y = _player._hitbox.y - 32;
				_position.x += _player._hitbox.x - 48;
				_position.y += _player._hitbox.y - 32;
				_speed = -10;
				_lastLeft = true;
			}

			if (player._velocity.x > 0 || _lastLeft == false)
			{
				this.x = _player._hitbox.x + 48;
				this.y = _player._hitbox.y - 32;
				_position.x += _player._hitbox.x + 48;
				_position.y += _player._hitbox.y - 32;
				_speed = 10;
				_lastLeft = false;
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

		}

		private void ProjectileCollision()
		{

			GameObject TiledObject;
			int directionY;


			this.x = _position.x + _velocity.x;

			TiledObject = Level.Return().CheckCollision(this);

			if (TiledObject != null)
			{
				Console.WriteLine("HIT");
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
					Console.WriteLine("hitbot");
					_velocity.y *= -1;
					_velocity.y *= _bouncinessY;
				}

				if (directionY == -1)
				{
					Console.WriteLine("hitTop");
					_velocity.y = 0;
				}
			}

			y = _position.y - _velocity.y;

			_position.Add(_velocity);
		}


		private void updatePosition()
		{
			x = _position.x;
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