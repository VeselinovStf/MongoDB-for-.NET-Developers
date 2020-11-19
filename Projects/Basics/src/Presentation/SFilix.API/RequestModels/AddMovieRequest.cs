using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.RequestModels
{
    public class AddMovieRequest
    {
        public string Title { get; set; }

        public string Plot { get; set; }

        public int Year { get; set; }
    }
}
