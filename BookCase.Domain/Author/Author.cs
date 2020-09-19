using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookCase.Domain.Author
{
    public class Author
    {
        public Guid Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Display(Name = "Mail")]
        public string Mail { get; set; }
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }
        public virtual IList<Domain.Book.Book> Books { get; set; }
    }
}
