using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooShop.Enums
{
    [Flags]
    public enum SpecialRequirements
    {
        None = 0,
        Fragile = 1,
        Oxidizable = 2,
        Volatile = 4,
        Fragrant = 8,
        LightProtection = 16
    }
}
