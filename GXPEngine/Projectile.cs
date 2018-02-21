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
		}

		private void ProjectileCollision()
		{

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