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

		private float _weight = 1;
		private float _speed = 0.5f;
		private float _gravityForce;

		Shape currentShape;

		public enum Shape
		{
			Human,
			Bird,
			Bear,
			Snake
		}




		public Player() : base("Sprites/colors.png")
		{
			_position = Vec2.zero;
			_velocity = Vec2.zero;
			_gravity = Vec2.zero;
		}

		public void Update()
		{
			handleMovement();
			handleActionInput();
			handlePhysics();
		}

		private void handleMovement()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if (currentShape == Shape.Bird)
				{
					if (Input.GetKeyDown(Key.W))
					{
						_velocity.y -= 25;
						Console.Write("IM A BIRD!!!!");
					}
				}
				else
				{
					if (Input.GetKeyDown(Key.W))
					{
						_velocity.y -= 25;
					}
				}


				if (Input.GetKey(Key.A))
				{
					_velocity.x = Utils.Clamp(_velocity.x - _speed, -5, 5);
				}

				if (Input.GetKey(Key.D))
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5, 5);
				}
			}
		}

		private void handleActionInput()
		{
			if (Input.GetKey(Key.LEFT_SHIFT))
			{

				if (Input.GetKeyDown(Key.W))
				{
					//Shapeshift into bird.
					currentShape = Shape.Bird;
				}

				if (Input.GetKeyDown(Key.S))
				{
					//Shapeshift human
					currentShape = Shape.Human;
				}

				if (Input.GetKeyDown(Key.A))
				{
					//Shapeshift into snake
					currentShape = Shape.Snake;
				}

				if (Input.GetKeyDown(Key.D))
				{
					//shapeshift into bear
					currentShape = Shape.Bear;
				}
			}
		}

		private void handlePhysics()
		{


			_gravityForce = _weight * 0.981f;

			if (this.y >= 450)
			{
				_gravityForce = 0;
				_gravity.y = 0;
			}
			else
			{
				_gravity.y += _gravityForce;
			}





			_velocity.Multiply(0.95f);

			_position.Add(_gravity);
			_position.Add(_velocity);

			this.x = _position.x;
			this.y = _position.y;
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
