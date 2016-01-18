using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.Physics;
using MonogameLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout.Breakout
{
    public class DefaultBlock : GameComponent, IBlock, IDamageable, ICollideable
    {
        private string[] blockAssetNames = { "block_blue", "block_yellow", "block_red" };
        private Texture2D[] blockTextureData;

        private bool enabled;
        private Sprite sprite;
        protected int health;

        private Rectangle AABB;

        private PlayerManager manager;

        public virtual int ScoreValue { get { return 3; } }

        #region Properties

        public float Health
        {
            get { return health; }
        }

        public bool IsActive
        {
            get { return this.enabled; }
            set { enabled = value; if (sprite != null) sprite.IsEnabled = value;}
        }

        public Sprite BlockSprite
        {
            get { return this.sprite; }
        }

        public Rectangle Bounds
        {
            get
            {
                if (this.enabled && sprite != null)
                {
                    AABB.X = (int)(sprite.Transform.position.X);
                    AABB.Y = (int)(sprite.Transform.position.Y);
                    AABB.Width = (int)(sprite.Transform.scale.X * sprite.spriteData.Width);
                    AABB.Height = (int)(sprite.Transform.scale.Y * sprite.spriteData.Height);

                    return AABB;
                }

                return Rectangle.Empty;
            }
        }

        #endregion

        public DefaultBlock(Game game)
            : base(game)
        {
            this.AABB = new Rectangle();
            this.enabled = true;
            this.health = 3;

            CollisionManager.AddObject(this);
            this.manager = Game.Services.GetService<PlayerManager>();
            manager.blocks++;
        }

        public virtual void Break()
        {
            this.IsActive = false;
        }

        public void Show()
        {
            this.IsActive = true;
        }

        public void LoadSprite()
        {
            this.blockTextureData = new Texture2D[blockAssetNames.Length];
            for (int iNames = 0; iNames < blockAssetNames.Length; iNames++)
                this.blockTextureData[iNames] = this.Game.Content.Load<Texture2D>("Sprites/Blocks/" + blockAssetNames[iNames]);

            this.sprite = new Sprite(this.Game, blockTextureData[0]);
            this.sprite.Transform.origin = Vector2.Zero;
            sprite.LoadContent();
        }

        public void DrawSelf(SpriteBatch sb)
        {
            this.sprite.spriteData = blockTextureData[(health > 0 && health <= blockAssetNames.Length) ? blockAssetNames.Length - health : 0];
            this.sprite.Draw(sb);
        }

        public void Damage(float amount)
        {
            health -= (int) amount;
            if (health <= 0) { 
                health = 0;
                this.IsActive = false;
                manager.blocks--;
                manager.AddScore(ScoreValue);
                Console.WriteLine("Block Count " + manager.blocks.ToString());
            }
            else if (health >= blockAssetNames.Length) health = blockAssetNames.Length - 1;
        }

        public virtual void OnCollision(CollisionInfo collision)
        {
            if(collision.collidingObject.GetType() == typeof(BreakoutBall))
                this.Damage(1);
        }
        public virtual void OnCollisionEnter(CollisionInfo collision)
        {
        }
        public virtual void OnCollisionExit(ICollideable obj)
        {
        }
    }
}
