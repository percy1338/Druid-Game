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
			player.onEvent += playerOnShapeEvent;
		}

		public void Update()
		{
			adjustPosition();
		}
		private void adjustPosition()
		{
            this.x = _player._position.x;
			this.y = _player._position.y;
		}

		private void playerOnShapeEvent(Player.Shape shape)
		{
			
			if (shape == Player.Shape.Human)
			{
				this.SetScaleXY(1, 1);
			}

			if (shape == Player.Shape.Bird)
			{
                this.SetScaleXY(0.5f, 0.5f);
			}

			if (shape == Player.Shape.Snake)
			{
                this.SetScaleXY(0.25f, 0.25f);
			}

			if (shape == Player.Shape.Bear)
			{
             this.SetScaleXY(2, 2);
			}

		}


    }
}
