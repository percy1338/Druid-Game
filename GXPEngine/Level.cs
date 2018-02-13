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

        public void DrawLevel(Map map, int[,] TileGids)//, int tileHeight, int tileWidth)
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
                        Tile tile = new Tile(TileGids[y, x], map);
                        tile.x = x * map.tileWidth;
                        tile.y = y * map.tileHeight;
                        this.AddChild(tile);
                    }
                }
            }
        }
    }
}
