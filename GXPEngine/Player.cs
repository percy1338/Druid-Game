﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	public class Player : AnimSprite
	{

		//vec2 fields.
		public Vec2 _position;
		public Vec2 _velocity;
		public Vec2 _gravity;

		MyGame Game;

		//values from all the animals.
		private float _weight;
		private float _speed;
		private float _topSpeed;
		private float _jump;

		//calculated floats.
		private float _gravityForce;


		private bool _landed;
		public bool left;
		public bool right;

		public delegate void OnShapeEvent(Shape shape); //Event for when shapeshifting happens.
		public event OnShapeEvent onShapeEvent; //So other classes can hook into this event.

		Shape currentShape; // setting currentshape, so i can keep track of it.

		public enum Shape
		{
			Human,
			Bird,
			Bear,
			Snake
		}

		public Player(MyGame game) : base("Sprites/testSheet.png", 4, 1, -1)
		{
			this.SetOrigin(width / 2, height);
			currentShape = Shape.Human;
			shapeEvent(Shape.Human);

			Game = game;
			Hitbox _hitbox = new Hitbox(this);
			Game.AddChild(_hitbox);

			_position = Vec2.zero;
			_velocity = Vec2.zero;
			_gravity = Vec2.zero;
		}

		public void Update()
		{
			handleShapeShift(); // checks if the player (wants) to change shape.

			if (currentShape == Shape.Human) handleInputHuman(); // better solution out there probaly.
			if (currentShape == Shape.Bird) handleInputBird(); // better solution out there probaly.
			if (currentShape == Shape.Snake) handleInputSnake(); // better solution out there probaly.
			if (currentShape == Shape.Bear) handleInputBear(); // better solution out there probaly.

			handlePhysics(); // handles all the physics and mechanics.
		}

		private void handleShapeShift()
		{
			if (Input.GetKey(Key.LEFT_SHIFT))
			{
				if ((Input.GetKeyDown(Key.W)) && currentShape != Shape.Bird)
				{
					//Shapeshift into bird.
					onShapeEvent(Shape.Bird); // does the event.
					shapeEvent(Shape.Bird); // Does the private void shapeEvent.
				}

				if ((Input.GetKeyDown(Key.S)) && currentShape != Shape.Human)
				{
					//Shapeshift human
					onShapeEvent(Shape.Human); // does the event.
					shapeEvent(Shape.Human); // Does the private void shapeEvent.
				}

				if ((Input.GetKeyDown(Key.A)) && currentShape != Shape.Snake)
				{
					//Shapeshift into snake
					onShapeEvent(Shape.Snake); // does the event.
					shapeEvent(Shape.Snake); // Does the private void shapeEvent.
				}

				if ((Input.GetKeyDown(Key.D)) && currentShape != Shape.Bear)
				{
					//shapeshift into bear
					onShapeEvent(Shape.Bear); // does the event.
					shapeEvent(Shape.Bear); // Does the private void shapeEvent.
				}
			}
		}

		private void shapeEvent(Shape shape)
		{
			if (shape == Shape.Human)
			{
				//Sets all the values and sizes to that of the human.
				currentShape = Shape.Human;

				//sizes:
				this.SetScaleXY(1, 1);
				SetFrame(0);

				//values:
				_weight = 1;
				_speed = 0.5f;
				_topSpeed = 0f;
				_jump = 25;
			}

			if (shape == Shape.Bird)
			{
				//Sets all the values and sizes to that of the bird.
				currentShape = Shape.Bird;

				//sizes:
				this.SetScaleXY(0.75f, 0.75f);
				SetFrame(1);

				//values:
				_weight = 0.5f;
				_speed = 0.5f;
				_topSpeed = 0f;
				_jump = 10;
			}

			if (shape == Shape.Snake)
			{
				//Sets all the values and sizes to that of the snake.
				currentShape = Shape.Snake;

				//sizes:
				this.SetScaleXY(0.5f, 0.5f);
				SetFrame(2);

				//values:
				_weight = 0.25f;
				_speed = 0.5f;
				_topSpeed = 2f;
				_jump = 0;
			}

			if (shape == Shape.Bear)
			{
				//Sets all the values and sizes to that of the bear.
				currentShape = Shape.Bear;

				//sizes:
				this.SetScaleXY(2, 2);
				SetFrame(3);

				//values:
				_weight = 1.5f;
				_speed = 0.5f;
				_topSpeed = -2f;
				_jump = 25;
			}
		}

		private void handlePhysics()
		{

			// SHOULD HAPPEN BEFORE IT DETECTS IF IT IS ON THE GROUND, ELSE YOU CAN DOUBLE JUMP.
			_gravityForce = _weight * 0.981f; // creates gravityforce, depending on the weight.


			if (this.y > 450) // (NEEDS TO BE REMOVED, invisible border for testing purposes)
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

			_position.Add(_velocity);
			_position.Add(_gravity);

			this.x = _position.x;
			this.y = _position.y;
		}


		private void handleInputHuman()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if ((Input.GetKeyDown(Key.W)) && _landed == true)
				{
					_velocity.y -= _jump;
					_landed = false;
				}

				if (Input.GetKey(Key.A))
				{
					_velocity.x = Utils.Clamp(_velocity.x - _speed, -5 - _topSpeed, 5 + _topSpeed);
				}

				if (Input.GetKey(Key.D))
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5 - _topSpeed, 5 + _topSpeed);
				}

				if (Input.GetKeyDown(Key.SPACE))
				{
					//Shoot
					Projectile fireball = new Projectile(this);
					Game.AddChild(fireball);

				}

			}
		}

		private void handleInputBird()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if (Input.GetKeyDown(Key.W))
				{
					_velocity.y -= _jump;
					_gravityForce = 0;
					_gravity.y = 0;
				}

				if (Input.GetKey(Key.A))
				{
					_velocity.x = Utils.Clamp(_velocity.x - _speed, -5 - _topSpeed, 5 + _topSpeed);
				}

				if (Input.GetKey(Key.D))
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5 - _topSpeed, 5 + _topSpeed);
				}

			}
		}

		private void handleInputSnake()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if (Input.GetKey(Key.A))
				{
					_velocity.x = Utils.Clamp(_velocity.x - _speed, -5 - _topSpeed, 5 + _topSpeed);
				}

				if (Input.GetKey(Key.D))
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5 - _topSpeed, 5 + _topSpeed);
				}
				if (Input.GetKey(Key.SPACE))
				{
					//dash
				}
			}
		}

		private void handleInputBear()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if ((Input.GetKeyDown(Key.W)) && _landed == true)
				{
					_velocity.y -= _jump;
					_landed = false;
				}

				if (Input.GetKey(Key.A))
				{
					_velocity.x = Utils.Clamp(_velocity.x - _speed, -5 - _topSpeed, 5 + _topSpeed);
				}

				if (Input.GetKey(Key.D))
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5 - _topSpeed, 5 + _topSpeed);
				}

				if (Input.GetKey(Key.SPACE))
				{
					//Slash in front of itself
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
