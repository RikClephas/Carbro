using Carbro.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbro.Core
{
    class Cocktail
    {
        public string Name { get; set; }
        public List<KeyValuePair<string, int>> Liquids { get; set; }

        public Cocktail()
        {
            Liquids = new List<KeyValuePair<string, int>>();
        }
    }
}
