using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
	public class Destructable : AnimationSprite, IActivatable
	{
		private int _timer;
		private int _health = 0;
		private Level _level;

		public Destructable(int frame, Map map, int index, Level level) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
		{
			_level = level;

			SetFrame(frame - map.tileSet.firstGid);
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

			if (_timer > 20)
			{
				_level._collisionSprites.Remove(this);
				this.Destroy();
			}
		}
	}
}
