using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout
{
    public class RandomController : GameComponent, IActor2DController
    {
        public Vector2 direction { get; private set; }
        public float rotation { get; private set; }

        public float randomTime;
        private float elapsedTime;
        private Random randomGenerator;

        public RandomController(Game game, float randomTime)
            : base(game)
        {
            this.randomTime = randomTime;
            elapsedTime = 0f;

            randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            direction = new Vector2((float)randomGenerator.NextDouble(), (float)randomGenerator.NextDouble());
            if (direction.Length() > 0f)
                direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (elapsedTime >= randomTime)
            {
                direction = new Vector2((float)(2 * randomGenerator.NextDouble() - 1), (float)(2 * randomGenerator.NextDouble() - 1));
                if (direction.Length() > 0f)
                    direction.Normalize();
                rotation = (float)Math.Atan2(direction.Y, direction.X);
                elapsedTime = 0f;
            }

            base.Update(gameTime);
        }
    }
}
