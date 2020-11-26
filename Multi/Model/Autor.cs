using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multi.Model
{
    public class Autor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; }
        

    }
}
