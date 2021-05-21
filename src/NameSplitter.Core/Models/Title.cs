using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSplitter.Core.Models
{
    public class Title
    {
        public Title()
        {
            Id = Guid.NewGuid().ToString();
            Content = string.Empty;
        }
        
        public string Id { get; set; }
        public string Content { get; set; }
        public AcademicDegree Degree { get; set; }
    }
}
