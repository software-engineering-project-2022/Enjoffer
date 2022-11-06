using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public partial class Word
    {
        public Word()
        {
            Sentences = new HashSet<Sentence>();
            Users = new HashSet<User>();
        }

        public int WordId { get; set; }
        public string Word1 { get; set; } = null!;
        public string WordTranslation { get; set; } = null!;
        public string? ImageSrc { get; set; }
        public DateOnly? Date { get; set; }
        public bool? IsCorrectInputed { get; set; }
        public int? CorrectTimesInputed { get; set; }
        public int? IncorrectTimesInputed { get; set; }

        public virtual ICollection<Sentence> Sentences { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
