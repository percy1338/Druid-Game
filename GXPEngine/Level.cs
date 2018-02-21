using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
	//
	public class Level : GameObject
	{
		List<GameObject> children = new List<GameObject>();
		public List<GameObject> _collisionSprites = new List<GameObject>();

		private static Level _lvl;
        private MyGame _mygame;
        private Player _player;
        private Map _map;

        private int _screenWidth;
        private int _screenHeight;

        float playerSpawnX;
        float playerSpawnY;

		public Level(MyGame mygame, int width, int height) : base()
		{
            _mygame = mygame;
			this.x = 0;
			this.y = 0;

            _screenHeight = height;
            _screenWidth = width;

			_lvl = this;
		}

        public void Update()
        {
            MoveCamera();
        }

		public void DrawLevel(Map map, int[,] TileGids)
		{
            _map = map;
            _player = new Player(this, playerSpawnX, playerSpawnY);


            if (HasChild(this))
			{
				this.GetChildren().Clear();
			}
			///////////background
			if (_map.background != null)
			{
                for(int i = 0; i < map.background.Length; i++)
                {
                    if(map.background[i].name == "backgroundlayer")
                    {
                        Background background = new Background(map, i);
                        AddChild(background);
                    }
                    if(map.background[i].name == "scrollingbackgroundlayer")
                    {
                        ScrollingBackground sb = new ScrollingBackground(_map, i, _player);
                        AddChild(sb);
                    }
                }
			}

			/////// tiles
			for (int y = 0; y < TileGids.GetLength(0); y++)
			{
				for (int x = 0; x < TileGids.GetLength(0); x++)
				{
					if (TileGids[y, x] != 0)
					{
						Tiles tile = new Tiles(TileGids[y, x], _map);
						tile.x = x * map.tileWidth;
						tile.y = y * map.tileHeight;
						_collisionSprites.Add(tile);

						this.AddChild(tile);
					}
				}
			}
			////// objects layer
			if (_map.objGroup.TiledObject != null)
			{
				for (int i = 0; i < map.objGroup.TiledObject.Count(); i++)
				{
                    if (_map.objGroup.TiledObject[i].properties != null)
                    {
                        if (_map.objGroup.TiledObject[i].properties.property[0].name == "Type" && _map.objGroup.TiledObject[i].properties.property[0].value == "PICKUP")
                        {
                            Pickup pickup = new Pickup(map.objGroup.TiledObject[i].gid, _map, i);
                            pickup.x = _map.objGroup.TiledObject[i].x;
                            pickup.y = _map.objGroup.TiledObject[i].y - _map.objGroup.TiledObject[i].height;
                            AddChild(pickup);
                        }
                        if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && _map.objGroup.TiledObject[i].properties.property[0].value == "DESTRUCTABLE")
                        {
                            Destructable destructable = new Destructable(_map.objGroup.TiledObject[i].gid, map, i);
                            destructable.x = map.objGroup.TiledObject[i].x;
                            destructable.y = map.objGroup.TiledObject[i].y - _map.objGroup.TiledObject[i].height;
                            AddChild(destructable);
                        }
                        if (_map.objGroup.TiledObject[i].properties.property[0].name == "Type" && _map.objGroup.TiledObject[i].properties.property[0].value == "BUTTON")
                        {
                            Button btn = new Button(_map.objGroup.TiledObject[i].gid, _map, i);
                            btn.x = _map.objGroup.TiledObject[i].x;
                            btn.y = _map.objGroup.TiledObject[i].y - _map.objGroup.TiledObject[i].height;
                            AddChild(btn);
                        }
                        if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && map.objGroup.TiledObject[i].properties.property[0].value == "NOTRANSZONE")
                        {
                            NoZone zone = new NoZone(map.objGroup.TiledObject[i].gid, _map, i);
                            zone.x = _map.objGroup.TiledObject[i].x;
                            zone.y = _map.objGroup.TiledObject[i].y - _map.objGroup.TiledObject[i].height;
                            AddChild(zone);
                        }
                        if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && _map.objGroup.TiledObject[i].properties.property[0].value == "TRANSZONE")
                        {
                            YesZone yZone = new YesZone(map.objGroup.TiledObject[i].gid, map, i);
                            yZone.x = _map.objGroup.TiledObject[i].x;
                            yZone.y = _map.objGroup.TiledObject[i].y - _map.objGroup.TiledObject[i].height;
                            AddChild(yZone);
                        }
                        if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && _map.objGroup.TiledObject[i].properties.property[0].value == "ENEMY")
                        {
                            Enemy enemy = new Enemy(map.objGroup.TiledObject[i].gid, map, i);
                            enemy.x = _map.objGroup.TiledObject[i].x;
                            enemy.y = _map.objGroup.TiledObject[i].y - _map.objGroup.TiledObject[i].height;
                            AddChild(enemy);
                        }
                        if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && _map.objGroup.TiledObject[i].properties.property[0].value == "ENEMY2")
                        {
                            Enemy2 enemy2 = new Enemy2(map.objGroup.TiledObject[i].gid, map, i, _player);
                            enemy2.x = _map.objGroup.TiledObject[i].x;
                            enemy2.y = _map.objGroup.TiledObject[i].y - _map.objGroup.TiledObject[i].height;
                            AddChild(enemy2);
                        }
                        if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && _map.objGroup.TiledObject[i].properties.property[0].value == "DAMAGE")
                        {
                            Thornes thornes = new Thornes(map.objGroup.TiledObject[i].gid, map, i);
                            thornes.x = _map.objGroup.TiledObject[i].x;
                            thornes.y = _map.objGroup.TiledObject[i].y - _map.objGroup.TiledObject[i].height;
                            AddChild(thornes);
                        }
                        if (map.objGroup.TiledObject[i].properties.property[0].name == "Type" && _map.objGroup.TiledObject[i].properties.property[0].value == "SPAWN")
                        {
                            playerSpawnX = map.objGroup.TiledObject[i].x;
                            playerSpawnY = map.objGroup.TiledObject[i].y;
                        }
                    }
                }
			}
            //////////////////////player
            //player
            AddChild(_player);

            ///////// foreground
            if (_map.background != null)
			{
                for(int i = 0; i < map.background.Length; i++)
                {
                    if(_map.background[i].name == "foreground layer")
                    {
                        ForeGround foreground = new ForeGround(_map, i);
                        AddChild(foreground);
                    }
                }
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

        void MoveCamera()
        {
            if (_player._hitbox.x >= this._screenWidth * 0.5f && _player._hitbox.x <= _map.width * _map.tileWidth - this._screenWidth*0.5)
            {
                this.x = -_player._hitbox.x + this._screenWidth * 0.5f;
            }
            if (_player._hitbox.y >= this._screenHeight * 0.5f && _player._hitbox.y <= _map.height * _map.tileHeight - this._screenHeight * 0.5f)
            {
                this.y = -_player._hitbox.y + this._screenHeight * 0.5f;
            }
        }

        public static Level Return()
		{
			return _lvl;
		}

	}
}
