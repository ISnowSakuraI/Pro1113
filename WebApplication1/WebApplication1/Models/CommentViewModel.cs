using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CommentViewModel
    {
        public Img img { get; set; }
        public IEnumerable<Comment> comments { get; set; }
    }
}