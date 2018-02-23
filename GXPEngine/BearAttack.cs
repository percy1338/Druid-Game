using System;
namespace GXPEngine
{
	public class BearAttack : Sprite
	{
		Player _player;
		private int _timer;
		private bool _lastLeft;
		public BearAttack(Player player) : base("Sprites/checkers.png")
		{
			_player = player;

			this.alpha = 0;
			this.SetScaleXY(2, 2);
			this.SetOrigin(width / 2, height / 2);

			if (player._velocity.x < 0 || _lastLeft == true)
			{
				this.x = _player._hitbox.x - 64;
				this.y = _player._hitbox.y;
				_lastLeft = true;
			}

			if (player._velocity.x > 0 || _lastLeft == false)
			{
				this.x = _player._hitbox.x + 192;
				this.y = _player._hitbox.y;
				_lastLeft = false;
			}


		}

		public void Update()
		{
			_timer++;

			if (_timer > 36)
			{
				this.Destroy();
			}
		}

		public void OnCollision(GameObject other)
		{
			if (other is IActivatable)
			{
				(other as IActivatable).Activateble(_player);
			}

		}
	}
}