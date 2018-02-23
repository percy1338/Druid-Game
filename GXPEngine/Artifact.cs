using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Artifact: AnimationSprite, IActivatable
    {
        public Artifact() : base("Level/Arc_4.png", 1,1,-1)
        {
            SetFrame(3);
        }

        public void Activateble(Player player)
        {
            WinScreen win = new WinScreen();
            game.AddChild(win);

            this.Destroy();
        }
    }
}
