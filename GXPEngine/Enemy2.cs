using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Enemy2 : AnimationSprite
    {
        //
        private Sound _backgroundMusic = new Sound("audio/223611__ctcollab__fire-ball-release.wav", false, true);
        private SoundChannel _backgroundChanel;

        private float _range = 600;
        private float _cooldown = 60;
        private int _dmg;
        private bool _shootLeft;
        private int _step;
       

        EnemyBullet bullet;
        private Player _player;
        

        public Enemy2(int frame, Map map, int index, Player player) : base("sprites/EnemySprite.png", 3, 1, -1)
        {
            SetFrame(0);
			SetOrigin(width / 2, height / 2);
			SetScaleXY(1.75f, 1.75f);
            _player = player;

            for (int i = 0; i < map.objGroup.TiledObject[index].properties.property.Length; i++)
            {
                if (map.objGroup.TiledObject[index].properties.property[i].name == "damage")
                {
                    _dmg = int.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
                if (map.objGroup.TiledObject[index].properties.property[i].name == "shootLeft")
                {
                    _shootLeft = bool.Parse(map.objGroup.TiledObject[index].properties.property[i].value);
                }
            }
        }

        public void Update()
        {
			_step += 1;
            if (_step > 15)
            {
                NextFrame();
                _step = 0;

                if (currentFrame == 3)
                {
                    SetFrame(0);
                }
            }
            ShootPlayer();
            _cooldown--;
        }

        void ShootPlayer()
        {
          //  float deltaX = _player.position.x - this.x;
          //  float deltaY = _player.position.y - this.y;
          //  float length = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
          //  if(length <= _range)
          //  {
                if (_cooldown <= 0)
                {
                    bullet = new EnemyBullet(this.x, this.y, _shootLeft);
                    _backgroundChanel = _backgroundMusic.Play();
                    _backgroundChanel.Volume = 0.2f;
                    parent.AddChild(bullet);

                    _cooldown = 120;
                }
           // }

        }
    }
}
