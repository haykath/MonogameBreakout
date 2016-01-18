using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonogameLibrary.Utilities
{
    public class DrawableSprite : DrawableGameComponent, ISprite
    {
        protected Transform2D transform;
        protected Color color;
        protected bool enabled;

        protected SpriteBatch spriteBatch;
        protected Texture2D sprite;
        protected string assetName;

        protected SpriteEffects effects;
        protected Vector2 spriteOrigin;

        public Texture2D spriteData { 
            get { return this.sprite; } 
            set { if (value != null) sprite = value; }
        }
        public Transform2D Transform { get { return transform; } }
        public bool IsEnabled { get { return enabled; } set { enabled = value; } }
        public Color Color { get { return this.color; } set { if (value != null) this.color = value; } }

        public DrawableSprite(Game game, string spriteName, SpriteBatch sBatch = null, Nullable<Vector2> pos = null, Nullable<Color> col = null)
            : base(game)
        {
            enabled = true;
            assetName = spriteName;
            transform = new Transform2D();
            spriteOrigin = new Vector2();

            if (sBatch != null)
                spriteBatch = sBatch;

            if (pos.HasValue)
                transform.position = pos.Value;

            if (col.HasValue)
                color = col.Value;
            else
                color = Color.White;
        }

        protected override void LoadContent()
        {
            if (spriteBatch == null)
                spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            sprite = Game.Content.Load<Texture2D>(assetName);

            effects = SpriteEffects.None;

            spriteOrigin.X = transform.origin.X * sprite.Width;
            spriteOrigin.Y = transform.origin.Y * sprite.Height;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw
            if (enabled)
            {
                spriteBatch.Begin(SpriteSortMode.Texture);
                spriteBatch.Draw(sprite, transform.position, null, null, spriteOrigin, transform.rotation, transform.scale, color, effects, 0f);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
