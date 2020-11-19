using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.RequestModels
{
    public class AddImdbRequest
    {
       
        public double Rating { get; set; }

       
        public int Votes { get; set; }

        public string MovieId { get; set; }
    }
}
