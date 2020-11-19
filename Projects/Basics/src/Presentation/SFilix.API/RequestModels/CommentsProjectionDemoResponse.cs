using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFilix.API.RequestModels
{
    // Used for Demo of Projection
    public class CommentsProjectionDemoResponse
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public string Author { get; set; }
    }
}
