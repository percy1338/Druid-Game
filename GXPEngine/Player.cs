using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	public class Player : AnimSprite
	{
		public Vec2 _position;
		public Vec2 _velocity;
		public Vec2 _gravity;

		MyGame Game;

		private float _weight = 1;
		private float _speed = 0.5f;
		private float _gravityForce;

		private bool _landed;

		public delegate void OnShapeEvent(Shape shape);
		public event OnShapeEvent onShapeEvent;

		Shape currentShape;

		public enum Shape
		{
			Human,
			Bird,
			Bear,
			Snake
		}

		public Player(MyGame game) : base("Sprites/testSheet.png", 4,1,-1)
		{
			currentShape = Shape.Human;
			shapeEvent(Shape.Human);
			SetFrame(0);
			this.SetOrigin(width / 2, height);


			Game = game;

			Hitbox _hitbox = new Hitbox(this);
			Game.AddChild(_hitbox);


			_position = Vec2.zero;
			_velocity = Vec2.zero;
			_gravity = Vec2.zero;
		}

		public void Update()
		{
            handleShapeShift();

			if (currentShape == Shape.Human) handleMovementHuman();
			if (currentShape == Shape.Bird) handleMovementBird();
			if (currentShape == Shape.Snake) handleMovementSnake();
			if (currentShape == Shape.Bear) handleMovementBear();

			handlePhysics();
		}

		private void handleShapeShift()
		{
			if (Input.GetKey(Key.LEFT_SHIFT))
			{

				if ((Input.GetKeyDown(Key.W)) && currentShape != Shape.Bird)
				{
					//Shapeshift into bird.
					onShapeEvent(Shape.Bird);
					currentShape = Shape.Bird;
					shapeEvent(Shape.Bird);
					this.SetScaleXY(0.75f, 0.75f);
				}

				if ((Input.GetKeyDown(Key.S)) && currentShape != Shape.Human)
				{
					//Shapeshift human
                    onShapeEvent(Shape.Human);
					currentShape = Shape.Human;
					shapeEvent(Shape.Human);
                    this.SetScaleXY(1, 1);

				}

				if ((Input.GetKeyDown(Key.A)) && currentShape != Shape.Snake)
				{
					//Shapeshift into snake
                    onShapeEvent(Shape.Snake);
					currentShape = Shape.Snake;
					shapeEvent(Shape.Snake);
                    this.SetScaleXY(0.5f, 0.5f);
				}

				if ((Input.GetKeyDown(Key.D)) && currentShape != Shape.Bear)
				{
					//shapeshift into bear
                    onShapeEvent(Shape.Bear);
					currentShape = Shape.Bear;
					shapeEvent(Shape.Bear);
                    this.SetScaleXY(2, 2);
				}
			}
		}

		private void shapeEvent(Shape shape)
		{
			if (shape == Shape.Human)
			{
				Console.WriteLine("human");
				_weight = 1;
				SetFrame(0);
			}

			if (shape == Shape.Bird)
			{
				Console.WriteLine("bird");
				_weight = 0.5f;
				SetFrame(1);
			}

			if (shape == Shape.Snake)
			{
				Console.WriteLine("snake");
				_weight = 0.25f;
				SetFrame(2);
			}

			if (shape == Shape.Bear)
			{
				Console.WriteLine("bear");
				_weight = 2f;
				SetFrame(3);
			}
		}



		private void handlePhysics()
		{

			// SHOULD HAPPEN BEFORE IT DETECTS IF IT IS ON THE GROUND, ELSE YOU CAN DOUBLE JUMP.
			_gravityForce = _weight * 0.981f;


			if (this.y > 450)
			{
				_gravityForce = 0;
				_gravity.y = 0;
				_landed = true;
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


		private void handleMovementHuman()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if ((Input.GetKeyDown(Key.W)) && _landed == true)
				{
					_velocity.y -= 25;
					_landed = false;
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

		private void handleMovementBird()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if (Input.GetKeyDown(Key.W))
				{
					_velocity.y -= 10;
					_gravityForce = 0;
					_gravity.y = 0;
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

		private void handleMovementSnake()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
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

		private void handleMovementBear()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if ((Input.GetKeyDown(Key.W)) && _landed == true)
				{
					_velocity.y -= 10;
					_landed = false;
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
