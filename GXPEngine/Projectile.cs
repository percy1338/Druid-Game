﻿using System;
namespace GXPEngine
{
	public class Projectile : Sprite
	{
		public Vec2 _position;
		public Vec2 _velocity;
		public Vec2 _gravity;
		private Player _player;

		//values
		private float _speed = 5;
		private float _weight = 0.01f;

		public Projectile(Player player) : base("Sprites/checkers.png")
		{
			_velocity = Vec2.zero;
			_position = Vec2.zero;
			_gravity = Vec2.zero;

			_player = player;

			this.x = _player.position.x;
			this.y = _player.position.y;
			_position.x += _player.position.x;
			_position.y += _player.position.y;

		}
		public void Update()
		{
			float gravityForce = _weight * 0.981f;

			_speed *= 0.98f;
			_velocity.x = _speed;
			_velocity.y = 0;


			_position.Add(_velocity);
			_position.Add(_gravity);

			this.x = _position.x;
			this.y = _position.y;
		}
	}
}
