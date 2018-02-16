﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	class Level : GameObject
	{
		List<GameObject> children = new List<GameObject>();
		public List<GameObject> _collisionSprites = new List<GameObject>();

		private static Level _lvl;

		public Level() : base()
		{
			this.x = 0;
			this.y = 0;

			_lvl = this;
		}

		public void DrawLevel(Map map, int[,] TileGids)
		{
			if (HasChild(this))
			{
				this.GetChildren().Clear();
			}
			///////////background
			if (map.background != null)
			{
				Background background = new Background(map);
				AddChild(background);
			}

			/////// tiles
			for (int y = 0; y < TileGids.GetLength(0); y++)
			{
				for (int x = 0; x < TileGids.GetLength(0); x++)
				{
					if (TileGids[y, x] != 0)
					{
						Tiles tile = new Tiles(TileGids[y, x], map);
						tile.x = x * map.tileWidth;
						tile.y = y * map.tileHeight;
						_collisionSprites.Add(tile);

						this.AddChild(tile);
					}
				}
			}
			////// objects layer
			if (map.objGroup.TiledObject != null)
			{
				for (int i = 0; i < map.objGroup.TiledObject.Count(); i++)
				{
					if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && map.objGroup.TiledObject[i].properties.property[0].value == "PICKUP")
					{
						Pickup pickup = new Pickup(map.objGroup.TiledObject[i].gid, map, i);
						pickup.x = map.objGroup.TiledObject[i].x;
						pickup.y = map.objGroup.TiledObject[i].y - map.objGroup.TiledObject[i].height;
						AddChild(pickup);
					}
					if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && map.objGroup.TiledObject[i].properties.property[0].value == "DESTRUCTABLE")
					{
						Destructable destructable = new Destructable(map.objGroup.TiledObject[i].gid, map, i);
						destructable.x = map.objGroup.TiledObject[i].x;
						destructable.y = map.objGroup.TiledObject[i].y - map.objGroup.TiledObject[i].height;
						AddChild(destructable);
					}
					if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && map.objGroup.TiledObject[i].properties.property[0].value == "BUTTON")
					{
						Button btn = new Button(map.objGroup.TiledObject[i].gid, map, i);
						btn.x = map.objGroup.TiledObject[i].x;
						btn.y = map.objGroup.TiledObject[i].y - map.objGroup.TiledObject[i].height;
						AddChild(btn);
					}
					else
					{
						Objects obj = new Objects(map.objGroup.TiledObject[i].gid, map, i);
						obj.x = map.objGroup.TiledObject[i].x;
						obj.y = map.objGroup.TiledObject[i].y - map.objGroup.TiledObject[i].height;
						AddChild(obj);
					}
				}
			}
			///////// foreground
			if (map.background != null)
			{
				ForeGround foreground = new ForeGround(map);
				AddChild(foreground);
			}
		}


		public GameObject CheckCollision(GameObject other)
		{
			GameObject tiledObject;
			for (int i = 0; i < _collisionSprites.Count; i++)
			{
				tiledObject = _collisionSprites[i];
				if (other.HitTest(tiledObject))
					return tiledObject;
			}
			return null;
		}

		public static Level Return()
		{
			return _lvl;
		}

	}
}
