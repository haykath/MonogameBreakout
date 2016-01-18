using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameLibrary.Physics
{
    public interface ICollideable
    {
        Rectangle Bounds { get; }

        void OnCollision(CollisionInfo collision);
        void OnCollisionEnter(CollisionInfo collision);
        void OnCollisionExit(ICollideable obj);
    }
}
