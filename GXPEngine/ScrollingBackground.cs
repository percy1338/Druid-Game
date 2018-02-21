using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class ScrollingBackground : Sprite
    {
        public ScrollingBackground(Map map, int i, Player player) : base("Level/" + map.background[i].background.source)
        {
            this.x += map.background[i].offsetx;
            this.y += map.background[i].offsety;
        }
    }
}
