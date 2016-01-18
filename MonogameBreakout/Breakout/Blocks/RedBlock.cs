using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout.Breakout
{
    public class RedBlock : DefaultBlock
    {
        public RedBlock(Game game) : base(game) { this.health = 1; }
        public override int ScoreValue { get { return 1; } }
    }
}
