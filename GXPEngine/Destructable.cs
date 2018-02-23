using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
	public class Destructable : Sprite, IActivatable
	{
		private int _timer;
		private int _health = 0;
		private Level _level;

		public Destructable(int frame, Map map, int index, Level level) : base("Level/wall (1).png")

        {
			_level = level;

			for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
			{
				if (map.objGroup.TiledObject[index].properties.property[i].name == "hp")
				{
					_health = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
				}
			}
		}

		public void Activateble(Player player)
		{
			_timer++;

			if (_timer > 36)
			{
				_level._collisionSprites.Remove(this);
				this.Destroy();
			}
		}
	}
}
