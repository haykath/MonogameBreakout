using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameLibrary.Utilities
{
    public interface IDamageable
    {
        float Health { get; }

        void Damage(float amount);
    }
}
