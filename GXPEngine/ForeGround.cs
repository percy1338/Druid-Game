using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
    public class ForeGround : Sprite
    {
        private int _index = 0;
        public ForeGround(Map map, int i) : base("Level/" + map.background[i].background.source)
        {
            _index = i;

            //this.x = map.background[_index].offsetx;
           // this.y = map.background[_index].offsety;
        }
    }
}
