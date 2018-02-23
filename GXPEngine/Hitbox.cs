using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	public class Hitbox : Sprite
	{
		Player _player;
		private bool _hitRight;

		public Hitbox(Player player, float spawnX, float spawnY) : base("Sprites/colors.png")
		{
			_player = player;
			player.onShapeEvent += playerOnShapeEvent;
			this.SetOrigin(width / 2, height);
			this.alpha = 0.00f;
			playerOnShapeEvent(Player.Shape.Human);

			_player.position.x = spawnX;
			_player.position.y = spawnY;

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
				this.SetScaleXY(0.75f, 1.75f);
			}

			if (shape == Player.Shape.Bird)
			{
				this.SetScaleXY(1, 1f);
			}

			if (shape == Player.Shape.Snake)
			{
				this.SetScaleXY(1f, 0.5f);
			}

			if (shape == Player.Shape.Bear)
			{
				this.SetScaleXY(3, 2);
			}

			if (shape == Player.Shape.Bear && _hitRight == true)
			{
				this.SetScaleXY(2, 2);
				_player.position.x -= 32;
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
					_player._position.x = TiledObject.x - width / 2;
					_player._velocity.x = 0.0000000000001f; //PRO-CODE ONLY PLZ DON'T COPY THIS. ITS MINE AND MINE ONLY.
					_hitRight = true;
				}

				if (direction == 1)
				{
					_player._position.x = TiledObject.x + 64 + width / 2;
					_player._velocity.x = 0;
				}

			}
			else
			{
				_hitRight = false;
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
				}

				_player._velocity.y = 0;
				_player._gravity.y = 0;

			}
			else
			{
				_player._landedBird = false;
			}
			y = _player.position.y - _player.velocity.y;

		}

		public void OnCollision(GameObject other)
		{
			if (other is IActivatable)
			{
				(other as IActivatable).Activateble(_player);
			}
		}

		public void GetHit(int hit)
		{
			_player.GetHit(hit);
		}

	}
}