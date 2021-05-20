using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSplitter.View.ViewModels
{
    public class NameViewModel : BindableBase
    {

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, Name);
        }


    }
}
