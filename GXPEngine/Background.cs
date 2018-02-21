using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
    public class Background : Sprite
    {
        public Background(Map map, int i):base("Level/" + map.background[i].background.source)
        {
           // this.x += map.background[i].offsetx;
          //  this.y += map.background[i].offsety;
        }
    }
}
