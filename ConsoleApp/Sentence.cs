using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public partial class Sentence
    {
        public Sentence()
        {
            Words = new HashSet<Word>();
        }

        public int SentenceId { get; set; }
        public string Sentence1 { get; set; } = null!;
        public int? FkBookId { get; set; }

        public virtual Book? FkBook { get; set; }

        public virtual ICollection<Word> Words { get; set; }
    }
}
