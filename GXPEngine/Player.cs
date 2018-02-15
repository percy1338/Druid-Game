using System;
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
		Hitbox _hitbox;
		MyGame Game;



		//values from all the animals.
		private float _weight;
		private float _speed;
		private float _topSpeed;
		private float _jump;

		//calculated floats.
		public float _gravityForce;

		public bool _landed;
		public bool _hitTop;

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

		private Map _map;

		public Player(MyGame game, Map map) : base("Sprites/testSheet.png", 4, 1, -1)
		{
			_position = Vec2.zero;
			_velocity = Vec2.zero;
			_gravity = Vec2.zero;

			this.SetOrigin(width / 2, height);
			currentShape = Shape.Human;
			shapeEvent(Shape.Human);

			_velocity.y = -1;

			Game = game;
			_hitbox = new Hitbox(this);
			Game.AddChild(_hitbox);

			// find player spawn
			_map = map;
			for (int i = 0; i < map.objGroup.TiledObject.Length; i++)
			{
				if (map.objGroup.TiledObject[i].properties != null)
				{
					for (int p = 0; p < map.objGroup.TiledObject[i].properties.property.Length; p++)
					{
						if (map.objGroup.TiledObject[i].properties.property[p].name == "spawn" || map.objGroup.TiledObject[i].properties.property[p].value == "true")
						{
							_position.Set(map.objGroup.TiledObject[i].x, map.objGroup.TiledObject[i].y);
						}
					}
				}
			}
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
			_gravity.y = _weight * 0.981f;



			if (_landed == true)
			{
				_velocity.Set(0, 0);
				position.Add(velocity);
			}

			if (_landed == false)
			{
				_velocity.Multiply(0.95f);
				_velocity.Add(_gravity);
				position.Add(velocity);
			}



			if (_hitTop == true)
			{
				_hitTop = false;
			}

			this.x = _position.x;
			this.y = _position.y;


			Console.WriteLine(_landed);
			Console.WriteLine(_velocity);



			//_gravity.y = _weight * 0.981f;


			//if (_landed == true)
			//{
			//	_gravity.y = 0;
			//}

			//if (_landed == false)
			//{
			//	_velocity.Add(_gravity);
			//}


			//_velocity.Multiply(0.95f);
			//_position.Add(velocity);

			//Console.WriteLine(_landed);
			//Console.WriteLine(_velocity);

			//this.x = _position.x;
			//this.y = _position.y;
		}

		private void handleInputHuman()
		{
			if (!Input.GetKey(Key.LEFT_SHIFT))
			{
				if ((Input.GetKeyDown(Key.W)) && _landed == true)
				{
					_velocity.y -= _jump;
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
