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
			this.alpha = 0.0F;
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
				this.SetScaleXY(1, 1.25f);
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
	}
}
