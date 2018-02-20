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
		private float _weight = 0.05f;
		private bool _lastLeft;
		private int _timer;

		public Projectile(Player player) : base("Sprites/checkers.png")
		{
			_velocity = Vec2.zero;
			_position = Vec2.zero;
			_gravity = Vec2.zero;

			this.SetOrigin(width / 2, height / 2);
			this.SetScaleXY(0.5f, 0.5f);

			_player = player;

			if (player._velocity.x < 0 || _lastLeft == true)
			{
				this.x = _player.position.x - 48;
				this.y = _player.position.y - 64;
				_position.x += _player.position.x - 48;
				_position.y += _player.position.y - 64;
				_speed = -10;
				_lastLeft = true;
			}

			if (player._velocity.x > 0 || _lastLeft == false)
			{
				this.x = _player.position.x + 48;
				this.y = _player.position.y - 64;
				_position.x += _player.position.x + 48;
				_position.y += _player.position.y - 64;
				_speed = 10;
				_lastLeft = false;
			}
			this.SetOrigin(width / 2, height / 2);
			this.SetScaleXY(0.5f, 0.5f);

			_velocity.x = _speed;

		}
		public void Update()
		{
			TimerProjectile();
			ProjectileCollision();

			//ProjectilePhysics();


			_velocity.Multiply(0.98f);
			_position.Add(_velocity);
		}

		private void ProjectileCollision()
		{
			GameObject TiledObject;
			TiledObject = Level.Return().CheckCollision(this);

			//X-collision
            this.x += _velocity.x;

			if (TiledObject != null)
			{
				_velocity.Multiply(-1);

			}
			x = _position.x + _velocity.x;
		}

		private void ProjectilePhysics()
		{
			//float gravityForce = _weight * 0.981f;

			//_gravity.y += gravityForce;

			//_speed *= 0.99f;
			//_velocity.x = _speed;
			//_velocity.y = 0;


			//_position.Add(_velocity);
			//_position.Add(_gravity);

			//this.x = _position.x;
			//this.y = _position.y;
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
