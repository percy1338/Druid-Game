using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
    public class Thornes : AnimationSprite, IActivatable
    {
        private int _dmg = 0;
        private int _cooldown = 0;

        public Thornes(int frame, Map map, int index) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);

            this.width = (int)map.objGroup.TiledObject[index].width;
            this.height = (int)map.objGroup.TiledObject[index].height;

            for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
            {
                if (map.objGroup.TiledObject[index].properties.property[i].name == "damage")
                {
                    _dmg = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
            }

            this.alpha = 0.5f;
            if (_cooldown >= 0)
            {
                _cooldown--;
            }
        }

        public void Activateble(Player player)
        {
            Console.WriteLine("player take damage");
            if (_cooldown <= 0)
            {
                Console.WriteLine("player got his shit kicked in");
                player.GetHit(_dmg);
                _cooldown = 120;
            }
        }
    }
}
