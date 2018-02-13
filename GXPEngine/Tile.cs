using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Tile : AnimSprite
    {
        public Tile(int frame, Map map) : base("Level/"+ map.tileSet.image.source, 44, 24, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);
        }

    }
}
