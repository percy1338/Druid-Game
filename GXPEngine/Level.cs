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
                        Objects obj = new Objects(TileGids[y, x], map, map.tileSet.tilecount / map.tileSet.columns);
                        obj.x = x * map.tileWidth;
                        obj.y = y * map.tileHeight;
                        this.AddChild(obj);
                    }
                }
            }
        }
    }
}
