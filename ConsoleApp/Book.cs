using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public partial class Book
    {
        public Book()
        {
            Sentences = new HashSet<Sentence>();
            Users = new HashSet<User>();
        }

        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string BookContent { get; set; } = null!;
        public int? NumberOfPages { get; set; }
        public string? BookCoverImg { get; set; }
        public int? LastViewedPage { get; set; }
        public string? Author { get; set; }

        public virtual ICollection<Sentence> Sentences { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
