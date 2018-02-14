using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class SolidObjects : AnimationSprite
    {
        public SolidObjects(int frame, Map map) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount / map.tileSet.columns, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);
        }

    }
}
