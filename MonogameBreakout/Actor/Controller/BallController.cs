using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout
{
    public class BallController : GameComponent, IActor2DController
    {
        private Vector2 dir;
        public bool flippedThisFrameX;
        public bool flippedThisFrameY;
        public Vector2 direction
        {
            get
            {
                return dir;
            }
            set
            {
                dir = value;
            }
        }
        public float rotation { get { return 0; } }

        private Random randomGenerator;

        public BallController(Game game)
            : base(game)
        {
            randomGenerator = new Random(Guid.NewGuid().GetHashCode());

            this.flippedThisFrameX = false;
            this.flippedThisFrameY = false;
        }

        public void Launch()
        {
            float randomAngle = 0.785398f + (float)(randomGenerator.NextDouble() * Math.PI / 2f);
            direction = new Vector2((float)Math.Cos(randomAngle), -(float)Math.Sin(randomAngle));
            if (dir.Length() > 0f)
                dir.Normalize();
        }

        public void FlipX()
        {
            if (!flippedThisFrameX)
            {
                flippedThisFrameX = true;
                dir.X *= -1;
            }
        }

        public void FlipY()
        {
            if (!flippedThisFrameY)
            {
                flippedThisFrameY = true;
                dir.Y *= -1;
            }
        }

        public void ScaleX(float scale)
        {
            dir.X *= MathHelper.Clamp(scale, -1f, 1f);
        }
    }
}
