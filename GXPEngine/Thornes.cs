using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Thornes : AnimationSprite, IActivatable
    {
        private int _dmg = 0;

        public Thornes(int frame, Map map, int index) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);

            this.width = int.Parse(map.objGroup.TiledObject[index].width.ToString());
            this.height = int.Parse(map.objGroup.TiledObject[index].height.ToString());

            for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
            {
                if (map.objGroup.TiledObject[index].properties.property[i].name == "damage")
                {
                    _dmg = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
            }
        }

        public void Activateble(Player player)
        {
            Console.WriteLine("player take damage");
            //player.hp -= _dmg;
        }
    }
}
