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
		private float _speed = 10;
		private float _weight = 0.05f;

		public Projectile(Player player) : base("Sprites/checkers.png")
		{
			_velocity = Vec2.zero;
			_position = Vec2.zero;
			_gravity = Vec2.zero;

			_player = player;

			this.SetOrigin(width / 2, height / 2);
			this.SetScaleXY(0.5f, 0.5f);

			this.x = _player.position.x + 48;
			this.y = _player.position.y - 32;
			_position.x += _player.position.x + 48;
			_position.y += _player.position.y - 32;


		}
		public void Update()
		{
			float gravityForce = _weight * 0.981f;

			_gravity.y += gravityForce;

			_speed *= 0.99f;
			_velocity.x = _speed;
			_velocity.y = 0;


			_position.Add(_velocity);
			_position.Add(_gravity);

			this.x = _position.x;
			this.y = _position.y;
		}
	}
}
