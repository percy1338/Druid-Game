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
		public Hitbox _hitbox;
		Level _level;

		//values from all the animals.
		private float _weight;
		private float _speed;
		private float _topSpeed;
		private float _jump;
		private float _health = 10;
		private int frameTimer;
		private int idleTimer;
		private int _cooldown;

		private bool _attackAnimation;

		//calculated floats.

		public bool _landed;
		public bool _landedBird;
		public bool Changeable;
		public bool left;
		public bool right;
		public bool CanTransform = true;

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

		public Player(Level level, float SpawnX, float SpawnY) : base("Sprites/PlayerSheet2.png", 5, 9, -1)
		{
			_position = Vec2.zero;
			_velocity = Vec2.zero;
			_gravity = Vec2.zero;

			this.SetOrigin(width / 2, height);
			currentShape = Shape.Human;
			shapeEvent(Shape.Human);
			_level = level;

			_hitbox = new Hitbox(this, SpawnX, SpawnY);
			_level.AddChild(_hitbox);
		}

		public void Update()
		{
			handleShapeShift(); // checks if the player (wants) to change shape.

			if (currentShape == Shape.Human) handleInputHuman(); // better solution out there probaly.
			if (currentShape == Shape.Bird) handleInputBird(); // better solution out there probaly.
			if (currentShape == Shape.Snake) handleInputSnake(); // better solution out there probaly.
			if (currentShape == Shape.Bear) handleInputBear(); // better solution out there probaly.

			_hitbox.Step();

			this.x = _hitbox.x;
			this.y = _hitbox.y;

			handlePhysics(); // handles all the physics and mechanics.
		}

		private void handleShapeShift()
		{
			if (Input.GetKey(Key.LEFT_SHIFT))
			{
				if ((Input.GetKeyDown(Key.W)) && currentShape != Shape.Bird && CanTransform)
				{
					//Shapeshift into bird.
					onShapeEvent(Shape.Bird); // does the event.
					shapeEvent(Shape.Bird); // Does the private void shapeEvent.
				}

				if ((Input.GetKeyDown(Key.S)) && currentShape != Shape.Human && CanTransform)
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

				if ((Input.GetKeyDown(Key.D)) && currentShape != Shape.Bear && CanTransform)
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
				SetScaleXY(1, 1);
				SetFrame(35);

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
				SetFrame(16);
                SetScaleXY(1.75f, 1.75f);

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
				SetFrame(23);
                SetScaleXY(1.75f, 1.75f);

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
				SetFrame(0);
				SetScaleXY(1, 1);

				//values:
				_weight = 1.5f;
				_speed = 0.5f;
				_topSpeed = -2f;
				_jump = 20;
			}
		}

		private void handlePhysics()
		{
			_gravity.y = _weight * 0.981f;
			_velocity.Multiply(0.95f);
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
					this.Mirror(true, false);
					walkAnimationHuman();

				}

				if (Input.GetKey(Key.D))
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5 - _topSpeed, 5 + _topSpeed);
					this.Mirror(false, false);
					walkAnimationHuman();
				}

				if ((!Input.GetKey(Key.A)) && (!Input.GetKey(Key.D)) && (!Input.GetKey(Key.W)) && (!Input.GetKey(Key.SPACE)) && _attackAnimation == false)
				{
					idleTimer++;
					if (idleTimer > 10)
					{
						idleHuman();
					}
				}
				else
				{
					idleTimer = 0;
				}

				if ((Input.GetKeyDown(Key.SPACE)) && _cooldown == 0)
				{
					Projectile fireball = new Projectile(this);
					_level.AddChild(fireball);

					_cooldown = 120;

					if ((!Input.GetKey(Key.A)) && (!Input.GetKey(Key.D)))
					{
						_attackAnimation = true;
					}

				}
				else
				{
					if (_cooldown != 0)
					{
						_cooldown--;
					}
				}

				if (_attackAnimation == true)
				{
					HumanAttack();
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
					_gravity.y = 0;
					//Extra animation for in the air.

				}

				if ((Input.GetKey(Key.A)) && _landedBird == false)
				{
					_velocity.x = Utils.Clamp(_velocity.x - _speed, -5 - _topSpeed, 5 + _topSpeed);
					this.Mirror(true, false);
					walkAnimationBird();
				}

				if ((Input.GetKey(Key.D)) && _landedBird == false)
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5 - _topSpeed, 5 + _topSpeed);
					this.Mirror(false, false);
					walkAnimationBird();
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
					this.Mirror(true, false);
					walkAnimationSnake();
				}

				if (Input.GetKey(Key.D))
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5 - _topSpeed, 5 + _topSpeed);
					this.Mirror(false, false);
					walkAnimationSnake();
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
					this.Mirror(true, false);
					if (_attackAnimation == false)
					{
						walkAnimationBear();
					}
				}

				if (Input.GetKey(Key.D))
				{
					_velocity.x = Utils.Clamp(_velocity.x + _speed, -5 - _topSpeed, 5 + _topSpeed);
					this.Mirror(false, false);
					if (_attackAnimation == false)
					{
						walkAnimationBear();
					}
				}

				if ((!Input.GetKey(Key.D)) && (!Input.GetKey(Key.A)) && (!Input.GetKey(Key.W)) && _attackAnimation == false)
				{
					idleTimer++;
					if (idleTimer > 10)
					{
						idleBear();
					}
				}
				else
				{
					idleTimer = 0;
				}

				if ((Input.GetKeyDown(Key.SPACE)) && _cooldown == 0)
				{
					BearAttack attack = new BearAttack(this);
					_level.AddChild(attack);
					_attackAnimation = true;
					_cooldown = 60;
				}
				else
				{
					if (_cooldown > 0)
					{
						_cooldown--;
					}
				}

				if (_attackAnimation == true)
				{
					BearAttack();
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

		public Shape GetShape()
		{
			return currentShape;
		}

		public void GetHit(int damage)
		{
			_health -= damage;
			checkHP();
            
		}

		private void checkHP()
		{
			if (_health <= 0)
			{
				this.Destroy();
			}
		}




		//Animations and shit.



		private void walkAnimationBear()
		{
			if (currentFrame > 6)
			{
				SetFrame(0);
			}

			frameTimer++;

			if (frameTimer > 12)
			{
				NextFrame();
				if (currentFrame == 5)
				{
					SetFrame(0);
				}
				frameTimer = 0;
			}
		}

		private void walkAnimationHuman()
		{
			if (currentFrame < 26 || currentFrame > 35)
			{
				SetFrame(26);
			}

			frameTimer++;
			if (frameTimer > 6)
			{
				NextFrame();
				if (currentFrame == 35)
				{
					SetFrame(26);
				}
				frameTimer = 0;
			}
		}

		private void walkAnimationSnake()
		{
			if (currentFrame < 23 || currentFrame > 26)
			{
				SetFrame(23);
			}
			frameTimer++;

			if (frameTimer > 12)
			{
				NextFrame();
				if (currentFrame == 26)
				{
					SetFrame(23);
				}
				frameTimer = 0;
			}
		}
		private void walkAnimationBird()
		{
			if (currentFrame < 16 || currentFrame > 23)
			{
				SetFrame(16);
			}

			frameTimer++;
			if (frameTimer > 2)
			{
				NextFrame();
				if (currentFrame == 23)
				{
					SetFrame(16);
				}
				frameTimer = 0;
			}
		}

		private void idleBear()
		{
			if (currentFrame < 6 || currentFrame > 9)
			{
				SetFrame(6);
			}

			frameTimer++;
			if (frameTimer > 6)
			{
				NextFrame();
				if (currentFrame == 9)
				{
					SetFrame(6);
				}
				frameTimer = 0;
			}
		}

		private void idleHuman()
		{
			if (currentFrame < 35 || currentFrame > 40)
			{
				SetFrame(35);
			}

			frameTimer++;
			if (frameTimer > 12)
			{
				NextFrame();
				if (currentFrame == 41)
				{
					SetFrame(35);
				}
				frameTimer = 0;
			}
		}

		private void HumanAttack()
		{
			if (currentFrame < 40)
			{
				SetFrame(40);
			}

			frameTimer++;
			if (frameTimer > 12)
			{
				NextFrame();

				if (currentFrame == 44)
				{
					_attackAnimation = false;
				}

				frameTimer = 0;
			}
		}

		private void BearAttack()
		{
			if (currentFrame < 9 || currentFrame > 15)
			{
				SetFrame(9);
			}

			frameTimer++;

			if (frameTimer > 6)
			{
				NextFrame();

				if (currentFrame == 15)
				{
					_attackAnimation = false;
				}
				frameTimer = 0;
			}
		}

	}
}

