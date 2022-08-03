using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport.Model
{
    public class PersonList
    {
        public string Title { get; set; }
        public int Total { get; set; }
        public List<Person> Persons { get; set; }
    }
}
