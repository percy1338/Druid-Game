using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	public class Hitbox : Sprite
	{
		Player _player;
		public List<GameObject> _collisionSprites = new List<GameObject>();

		public float LastX;
		public float LastY;

		public Hitbox(Player player) : base("Sprites/colors.png")
		{
			_player = player;
			player.onShapeEvent += playerOnShapeEvent;
			this.SetOrigin(width / 2, height);
			this.alpha = 0.5f;
		}

		public void Update()
		{
			adjustPosition();

			LastX = _player.position.x;
			LastY = _player.position.y;

			step();

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
				this.SetScaleXY(0.75f, 0.75f);
			}

			if (shape == Player.Shape.Snake)
			{
				this.SetScaleXY(0.5f, 0.5f);
			}

			if (shape == Player.Shape.Bear)
			{
				this.SetScaleXY(2, 2);
			}
		}

		public void step()
		{
			GameObject tiledObject;
			tiledObject = CheckCollision(this);

			if (tiledObject != null)
			{
				
			}


			tiledObject = CheckCollision(this);
		}


		public GameObject CheckCollision(GameObject other)
		{
			GameObject tiledObject;
			for (int i = 0; i < _collisionSprites.Count; i++)
			{
				tiledObject = _collisionSprites[i];
				if (other.HitTest(tiledObject))
					return tiledObject;
			}
			return null;
		}




	}
}
