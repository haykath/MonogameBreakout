using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameLibrary.Utilities
{
    public class CollideableDrawableSprite : DrawableSprite, ICollideable
    {
        Rectangle AABB;

        public virtual Rectangle Bounds
        {
            get
            {
                if (this.enabled)
                {
                    AABB.X = (int)(transform.position.X - transform.scale.X * spriteData.Width / 2f);
                    AABB.Y = (int)(transform.position.Y - transform.scale.Y * spriteData.Height / 2f);
                    AABB.Width = (int)(transform.scale.X * spriteData.Width);
                    AABB.Height = (int)(transform.scale.Y * spriteData.Height);

                    return AABB;
                }

                return Rectangle.Empty;
            }
        }

        public CollideableDrawableSprite(Game game, string spriteName, SpriteBatch sBatch = null, Nullable<Vector2> pos = null, Nullable<Color> col = null)
            : base(game, spriteName, sBatch, pos, col)
        {
            AABB = new Rectangle();
            CollisionManager.AddObject(this);
        }

        public virtual void OnCollision(CollisionInfo collision)
        {
        }
        public virtual void OnCollisionEnter(CollisionInfo collision)
        {
        }
        public virtual void OnCollisionExit(ICollideable obj)
        {
        }
    }
}
