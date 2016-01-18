﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameLibrary.Physics
{
    public class CollisionManagerComponent : GameComponent
    {
        public CollisionManagerComponent(Game game) : base(game) { }

        public override void Update(GameTime gameTime)
        {
            CollisionManager.GetManager.TestAll();
            base.Update(gameTime);
        }
    }
}
