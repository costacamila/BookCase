using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookCase.Domain.Book
{
   public class Book
    {
        public Guid Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }
        [Display(Name = "Year")]
        public string Year { get; set; }
    }
}
