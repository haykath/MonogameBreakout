using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameLibrary.Utilities
{
    public interface ISprite
    {
        Texture2D spriteData { get; set; }
        Transform2D Transform { get; }
        bool IsEnabled { get; set; }
        Color Color { get; set; }
    }
}
