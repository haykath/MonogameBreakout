using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout
{
    public interface IActor2DController
    {
        Vector2 direction { get; }
        float rotation { get; }
    }
}
