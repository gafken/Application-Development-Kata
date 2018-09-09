using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutKata.Models
{
    public class Markdown  //This should probably inherit from a base class but I want
                           //To keep this simple for demonstration purposes
    {
        public string Identifier { get; set; }

        public string MarkdownPrice { get; set; }
    }
}