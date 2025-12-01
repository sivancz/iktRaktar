using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iktraktar.Models.Interfaces
{
    internal interface IStoreable
    {
        string Name { get; }
        int Quantity { get; set; }
    }
}
