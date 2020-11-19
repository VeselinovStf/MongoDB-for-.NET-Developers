using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.RequestModels
{
    public class UpdateMovieRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Plot { get; set; }

        public int Year { get; set; }

        public string ImdbId { get; set; }
        public bool InsertIfNotExist { get; set; }
    }
}
