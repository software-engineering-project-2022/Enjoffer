using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public partial class Advice
    {
        public Advice()
        {
            Users = new HashSet<User>();
        }

        public int AdviceId { get; set; }
        public string AdviceName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
