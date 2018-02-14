using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Level : GameObject
    {
        List<GameObject> children = new List<GameObject>();

        public Level() : base()
        {

        }

        public void DrawLevel(Map map, int[,] TileGids)
        {
            if (HasChild(this))
            {
                this.GetChildren().Clear();
            }

            for (int y = 0; y < TileGids.GetLength(0); y++)
            {
                for (int x = 0; x < TileGids.GetLength(0); x++)
                {
                    if (TileGids[y, x] != 0)
                    {
                        Tiles tile = new Tiles(TileGids[y, x], map);
                        tile.x = x * map.tileWidth;
                        tile.y = y * map.tileHeight;
                        this.AddChild(tile);
                    }
                }
            }

            if(map.objGroup.TiledObject != null)
            {
                for(int i = 0; i< map.objGroup.TiledObject.Count(); i++)
                {
                    Objects obj = new Objects(map.objGroup.TiledObject[i].gid,map);
                    obj.x = map.objGroup.TiledObject[i].x;
                    obj.y = map.objGroup.TiledObject[i].y;
                    AddChild(obj);
                }
            }
        }
    }
}
