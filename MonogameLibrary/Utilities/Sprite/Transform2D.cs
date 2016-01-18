using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameLibrary.Utilities
{
    public class Transform2D
    {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;
        public Vector2 origin;

        public Transform2D()
        {
            position = Vector2.Zero;
            rotation = 0f;
            scale = Vector2.One;
            origin = new Vector2(0.5f, 0.5f);
        }

        public Transform2D(Vector2 pos, float rot, Vector2 sca, Vector2 orig)
        {
            position = pos;
            rotation = rot;
            scale = sca;
            origin = orig;
        }

        public void Translate(Vector2 translation){
            position += translation;
        }
    }
}
