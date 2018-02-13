using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	public class Player : Sprite
	{
		public Vec2 _position;
		public Vec2 _velocity;
		public Vec2 _gravity;

		private float _weight = 10;

		public Player() : base("Sprites/colors.png")
		{
			_position = Vec2.zero;
			_velocity = Vec2.zero;
			_gravity = new Vec2(0, 1f);
		}

		public void Update()
		{
			handleMovement();
			handleActionInput();
			handlePhysics();
		}

		private void handleMovement()
		{
			if (Input.GetKey(Key.W))
			{
				
			}

			if (Input.GetKey(Key.S))
			{
				//move down.
			}

			if (Input.GetKey(Key.A))
			{
				// move left.
			}

			if (Input.GetKey(Key.D))
			{
				// move right.
			}
		}

		private void handleActionInput()
		{
			if (Input.GetKey(Key.LEFT_SHIFT))
			{
				
				if (Input.GetKeyDown(Key.W))
				{
					//Shapeshift bird
				}

				if (Input.GetKeyDown(Key.S))
				{
					//Shapeshift human
				}

				if (Input.GetKeyDown(Key.A))
				{
					//Shapeshift into snake
				}

				if (Input.GetKeyDown(Key.D))
				{
					//shapeshift into bear
				}
			}
		}

		private void handlePhysics()
		{

			float force = _weight * 9.81f;




			_position.Add(_velocity);
			_position.x = this.x;
			_position.y = this.y;
		}

		public Vec2 position
		{
			set { _position = value ?? Vec2.zero; }
			get { return _position; }
		}

		public Vec2 velocity
		{
			set { _velocity = value ?? Vec2.zero; }
			get { return _velocity; }
		}


	}
}
