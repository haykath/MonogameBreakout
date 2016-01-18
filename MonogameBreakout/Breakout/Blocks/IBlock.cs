using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout.Breakout
{
    public interface IBlock
    {
        bool IsActive { get; set; }
        Sprite BlockSprite { get; }
        Rectangle Bounds { get; }

        void Break();
        void Show();
        void LoadSprite();
        void DrawSelf(SpriteBatch sb);
    }
}
