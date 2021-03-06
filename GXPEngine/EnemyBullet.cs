﻿using System;

namespace GXPEngine
{
	public class EnemyBullet : Sprite
	{
		private float _lifetime = 120;
		private bool _shootleft;

		public EnemyBullet(float x, float y, bool shootleft) : base("Sprites/EnemyProjectile.png")
		{
			this.x = x;
			this.y = y;
			_shootleft = shootleft;
		}

		public void Update()
		{
			if (_shootleft)
			{
				this.x -= 5;
			}
			else
			{
				this.x += 5;
			}

			GameObject[] others = GetCollisions();//get all objects player is coliding with and puts it in a list
			foreach (GameObject other in others)
			{
				if (other is Tiles)
				{
					this.Destroy();
				}
				if (other is Hitbox)// check if colliderd object is an player and if the enemy shot the bullet
				{
					((Hitbox)other).GetHit(1);
					this.Destroy();
				}
			}
			if (_lifetime <= 0)
			{
				this.Destroy();
			}
			_lifetime--;
		}

		public void Hit()
		{
			this.Destroy();
		}
	}
}
