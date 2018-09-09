using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutKata.Models
{
    public class Markdown  //This should probably inherit from a base class but I want
                           //To keep this simple for demonstration purposes
    {
        public Markdown(string identifier, decimal markdownPrice)
        {
            Identifier = identifier;
            MarkdownPrice = markdownPrice;
        }

        public string Identifier { get; set; }

        public decimal MarkdownPrice { get; set; }
    }
}