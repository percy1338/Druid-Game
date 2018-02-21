using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class ScrollingBackground : Sprite
    {
        private Player _player;
        private Map _map;
        private int _index;

        public ScrollingBackground(Map map, int i, Player player) : base("Level/" + map.background[i].background.source)
        {
            _player = player;
            _map = map;
            _index = i;
            this.x = _player.x;
            this.y = _player.y;
            // this.x += _map.background[_index].offsetx;
            //  this.y += _map.background[_index].offsety;
        }

        public void Update()
        {
            this.x = float.Parse((_player.x - this.width * 0.5).ToString());
            this.y = float.Parse((_player.y - this.height * 0.5).ToString());

            if(this.x < 0)
            {
                this.x = 0;
            }
            if (this.y < 0)
            {
                this.y = 0;
            }
        }
    }
}
