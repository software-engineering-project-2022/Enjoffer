using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public partial class User
    {
        public User()
        {
            Advices = new HashSet<Advice>();
            Books = new HashSet<Book>();
            Words = new HashSet<Word>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string UserPassword { get; set; } = null!;

        public virtual ICollection<Advice> Advices { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Word> Words { get; set; }
    }
}
