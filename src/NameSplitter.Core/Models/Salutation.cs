using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSplitter.Core.Models
{
    public class Salutation
    {
        public Salutation()
        {
            Id = Guid.NewGuid().ToString();
            Content = string.Empty;
        }

        public string Id { get; set; }
        public string Content { get; set; }
        public Language Language { get; set; }
        public Gender Gender { get; set; }
        public SalutationForm Form { get; set; }
    }
}
