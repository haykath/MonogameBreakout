using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameLibrary.Physics
{
    public class CollisionInfo
    {
        public readonly ICollideable collidingObject;
        public readonly Rectangle collisionRect;
        public readonly Vector2 collisionPoint;

        public CollisionInfo(ICollideable obj, Rectangle intersection, Vector2 point)
        {
            this.collidingObject = obj;
            this.collisionRect = intersection;
            this.collisionPoint = point;
        }

    }
}
