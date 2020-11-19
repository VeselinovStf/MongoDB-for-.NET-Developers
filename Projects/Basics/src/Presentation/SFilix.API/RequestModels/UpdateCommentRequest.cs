using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.RequestModels
{
    public class UpdateCommentRequest
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public string Author { get; set; }

        public bool InsertIfNotExists { get; set; }
    }
}
