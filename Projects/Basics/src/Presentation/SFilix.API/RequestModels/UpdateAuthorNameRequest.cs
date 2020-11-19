using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.RequestModels
{
    public class UpdateAuthorNameRequest
    {
        public string Id { get; set; }

        public string AuthorName { get; set; }
    }
}
