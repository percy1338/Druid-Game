using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
    public class Background : Sprite
    {
        public Background(Map map):base("Level/" + map.background[0].background.source)
        {
           // this.x += map.background[0].offsetx;
          //  this.y += map.background[0].offsety;

        }
    }
}
