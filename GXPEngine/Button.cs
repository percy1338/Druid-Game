using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
    public class Button : AnimationSprite, IActivatable
    {
        private int _doorId = 0;
        public Button(int frame, Map map, int index) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);
            for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
            {
                if (map.objGroup.TiledObject[index].properties.property[i].name == "doorID")
                {
                    _doorId = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
            }
            
        }

        public void Activateble(Player player)
        {
            
        }
    }
}
