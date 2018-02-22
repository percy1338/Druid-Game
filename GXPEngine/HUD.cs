using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class HUD :Sprite
    {
        private Player _player;
        private int offsetx = -50;
        private int offsety = -100;

        public HUD(Player player) : base("HUD/HUD.png")
        {
            _player = player;
            this.scale = 0.30f;
            this.x = offsetx;
            this.y = offsety;
        }
    }
}
