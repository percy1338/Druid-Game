using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
    public class Pickup : AnimationSprite, IActivatable
    {
        private int _points = 0;

        public Pickup(int frame ,Map map, int index) : base("Level/Collectible1.png",1,1,-1)//("Level/" + map.tileSet.image.source, map.tileSet.columns, map.tileSet.tilecount/map.tileSet.columns, -1)
        {
            SetFrame(frame - map.tileSet.firstGid);
            for(int i =0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
            {
                if(map.objGroup.TiledObject[index].properties.property[i].name == "points")
                {
                    _points = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
            } 
        }

        public void Activateble(Player player)
        {
            // increse player score;
            //player.score += _points;
            this.alpha = 0;
            this.Destroy();
        }
    }
}
