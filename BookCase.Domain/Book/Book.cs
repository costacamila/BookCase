﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

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
        [Display(Name = "Author Name")]
        public string authorName { get; set; }
        [JsonIgnore]
        public virtual Domain.Author.Author Author { get; set; }

    }
}
