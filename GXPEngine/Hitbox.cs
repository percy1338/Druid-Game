using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	public class Hitbox : Sprite
	{
		Player _player;


		public Hitbox(Player player) : base("Sprites/colors.png")
		{
			_player = player;
			player.onShapeEvent += playerOnShapeEvent;
			this.SetOrigin(width / 2, height);
			this.alpha = 0.5F;
			playerOnShapeEvent(Player.Shape.Human);

		}

		public void Update()
		{
			this.x = _player._position.x - _player._velocity.x;
			this.y = _player._position.y - _player._velocity.y;
		}

		private void playerOnShapeEvent(Player.Shape shape)
		{

			if (shape == Player.Shape.Human)
			{
				this.SetScaleXY(1, 1.5f);
			}

			if (shape == Player.Shape.Bird)
			{
				this.SetScaleXY(1, 1f);
			}

			if (shape == Player.Shape.Snake)
			{
				this.SetScaleXY(1f, 1f);
			}

			if (shape == Player.Shape.Bear)
			{
				this.SetScaleXY(2, 2);
			}
		}


		//
		// Below here everything is about collision detection.
		//

		public void Step()
		{
			int direction;
			GameObject TiledObject;

			//X-COLLISION
			_player.position.x += _player._velocity.x;
			this.x = _player.position.x;

			TiledObject = Level.Return().CheckCollision(this);

			if (TiledObject != null)
			{
				direction = _player._velocity.x > 0 ? -1 : 1;

				if (direction == -1)
				{
					_player._position.x = TiledObject.x - width / 2f;
					_player._velocity.x = 0;

				}

				if (direction == 1)
				{
					_player._position.x = TiledObject.x + 64 + width / 2f;
					_player._velocity.x = 0;
				}

			}
			x = _player._position.x - _player.velocity.x;


			//Y-COLLISION
			_player._velocity.Add(_player._gravity);
			_player.position.y += _player._velocity.y;
			this.y = _player.position.y;

			TiledObject = Level.Return().CheckCollision(this);

			if (TiledObject != null)
			{
				direction = _player._velocity.y < 0 ? -1 : 1;

				if (direction == 1)
				{
					_player.position.y = TiledObject.y;
					_player._landed = true;
					_player._landedBird = true;
				}

				if (direction == -1)
				{
					_player.position.y = TiledObject.y + height + 64;
					_player._velocity.y = 0;
				}

				_player._velocity.y = 0;
				_player._gravity.y = 0;

			}
			else
			{
				_player._landedBird = false;
				//_player._landed = false; // if we want to check collision if you land instead of jump.
			}
			y = _player.position.y - _player.velocity.y;

		}

		public void OnCollision(GameObject other)
		{
			if (other is IActivatable)
			{
				(other as IActivatable).Activateble(_player);
				Console.WriteLine(other);
			}
		}
	}
}