using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout.Breakout
{
    public class YellowBlock : DefaultBlock
    {
        public YellowBlock(Game game) : base(game) { this.health = 2; }
        public override int ScoreValue { get{ return 2; } }
    }
}