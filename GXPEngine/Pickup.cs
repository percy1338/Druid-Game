using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Pickup : AnimationSprite
    {
        public Pickup(int frame ,Map map, int index) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount/map.tileSet.columns, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);
        }
    }
}
