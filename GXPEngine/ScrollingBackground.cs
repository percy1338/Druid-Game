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

        private float offset = 0;


        public ScrollingBackground(Map map, int i, Player player) : base("Level/" + map.background[i].background.source)
        {
            _player = player;
            _map = map;
            _index = i;
            this.x = _player.position.x;
            this.y = _player.position.y;
        }

        public void Update()
        {
            if(!(_player._velocity.x == 0.0000000000001f))
            {
                this.x = float.Parse((_player.position.x - this.width * 0.5).ToString());
                this.y = float.Parse((_player.position.y - this.height * 0.5).ToString());
            }

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
