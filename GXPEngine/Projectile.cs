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
		private float _weight = 0.25f;
		private bool _lastLeft;
		private int _timer;

		public Projectile(Player player) : base("Sprites/checkers.png")
		{
			_velocity = Vec2.zero;
			_position = Vec2.zero;
			_gravity = Vec2.zero;

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

			_velocity.x = _speed;

		}
		public void Update()
		{
			//Y-stuff;
			_gravity.y = _weight * 0.981f;
			_velocity.y += _gravity.y;

			//X-stuff;

			TimerProjectile();
			ProjectileCollision();

			//ProjectilePhysics();
		}

		private void ProjectileCollision()
		{
			_gravity.y = _weight * 0.981f;
			_velocity.y += _gravity.y;

			_velocity.x *= 0.98f;

			//int direction;
			GameObject TiledObject;

			//X - collision;

			_position.x += _velocity.x;
			this.x = _position.x;

			TiledObject = Level.Return().CheckCollision(this);






			// y - collision;



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
