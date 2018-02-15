using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Objects : AnimationSprite 
    {
        int points = 0;

        public Objects(int frame, Map map, int index) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1) 
        {
            SetFrame(frame - map.tileSet.firstGid);
            if(map.objGroup.TiledObject[index].properties != null)
            {
                for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
                {
                    if (map.objGroup.TiledObject[index].properties.property[i].name == "points")
                    {
                        points = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                    }
                }
            }
        }
    }
}
