using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class ForeGround : Sprite
    {
        public ForeGround(Map map) : base("Level/" + map.background[1].background.source)
        {
             this.x += map.background[1].offsetx;
             this.y += map.background[1].offsety;
        }
    }
}
