using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
     public class NoZone :AnimationSprite, IActivatable
    {
        private bool _CanTrans;
        public NoZone(int frame, Map map, int index) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);
            for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
            {
                if (map.objGroup.TiledObject[index].properties.property[i].name == "canTransform")
                {
                    _CanTrans = bool.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
            }
            this.alpha = 0;
        }

        public void Activateble(Player player)
        {
            player.CanTransform = _CanTrans;
        }
    }
}
