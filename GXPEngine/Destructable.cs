using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Destructable : AnimationSprite, IActivatable
    {
        private int _health = 0;

        public Destructable(int frame, Map map, int index) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
        {
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
            _health--;
            if(_health <= 0)
            {
                //play animation
                this.Destroy();
            }
        }
    }
}
