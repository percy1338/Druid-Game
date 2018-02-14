using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Objects : AnimSprite
    {
        string propertie = "";
        string propertieValue = "";

        public Objects(int frame, Map map, int row) : base("Level/" + map.tileSet.image.source, map.tileSet.columns, row, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);
        }
    }
}
