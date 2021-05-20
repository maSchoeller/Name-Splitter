using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSplitter.Core.Models
{
    public class NameFillingWord
    {
        public NameFillingWord()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string FillingWord { get; set; }

    }
}
