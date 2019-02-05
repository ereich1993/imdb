using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
