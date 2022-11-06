using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConsoleApp
{
    public partial class enjofferContext : DbContext
    {
        public enjofferContext()
        {
        }

        public enjofferContext(DbContextOptions<enjofferContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advice> Advices { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Sentence> Sentences { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Word> Words { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=enjoffer;Username=postgres;Password=yari2233");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("professions_type", new[] { "Розробник", "Терапевт", "Математик" });

            modelBuilder.Entity<Advice>(entity =>
            {
                entity.ToTable("advice");

                entity.HasIndex(e => e.AdviceName, "advice_unique")
                    .IsUnique();

                entity.Property(e => e.AdviceId)
                    .HasColumnName("advice_id")
                    .HasDefaultValueSql("nextval('advices_advice_id_seq'::regclass)");

                entity.Property(e => e.AdviceName).HasColumnName("advice_name");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.HasIndex(e => e.Title, "book_unique")
                    .IsUnique();

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .HasColumnName("author");

                entity.Property(e => e.BookContent).HasColumnName("book_content");

                entity.Property(e => e.BookCoverImg)
                    .HasMaxLength(255)
                    .HasColumnName("book_cover_img");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LastViewedPage)
                    .HasColumnName("last_viewed_page")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.NumberOfPages)
                    .HasColumnName("number_of_pages")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Sentence>(entity =>
            {
                entity.ToTable("sentences");

                entity.HasIndex(e => e.Sentence1, "sentence_unique")
                    .IsUnique();

                entity.Property(e => e.SentenceId).HasColumnName("sentence_id");

                entity.Property(e => e.FkBookId).HasColumnName("fk_book_id");

                entity.Property(e => e.Sentence1).HasColumnName("sentence");

                entity.HasOne(d => d.FkBook)
                    .WithMany(p => p.Sentences)
                    .HasForeignKey(d => d.FkBookId)
                    .HasConstraintName("sentences_fk_book_id_fkey");

                entity.HasMany(d => d.Words)
                    .WithMany(p => p.Sentences)
                    .UsingEntity<Dictionary<string, object>>(
                        "SentenceWord",
                        l => l.HasOne<Word>().WithMany().HasForeignKey("WordId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("sentence_word_word_id_fkey"),
                        r => r.HasOne<Sentence>().WithMany().HasForeignKey("SentenceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("sentence_word_sentence_id_fkey"),
                        j =>
                        {
                            j.HasKey("SentenceId", "WordId").HasName("sentence_word_pkey");

                            j.ToTable("sentence_word");

                            j.IndexerProperty<int>("SentenceId").HasColumnName("sentence_id");

                            j.IndexerProperty<int>("WordId").HasColumnName("word_id");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "users_username_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .HasColumnName("user_password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasMany(d => d.Advices)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserAdvice",
                        l => l.HasOne<Advice>().WithMany().HasForeignKey("AdviceId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("user_advice_advice_id_fkey"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("user_advice_user_id_fkey"),
                        j =>
                        {
                            j.HasKey("UserId", "AdviceId").HasName("user_advice_pkey");

                            j.ToTable("user_advice");

                            j.IndexerProperty<int>("UserId").HasColumnName("user_id");

                            j.IndexerProperty<int>("AdviceId").HasColumnName("advice_id");
                        });

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("user_book_book_id_fkey"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("user_book_user_id_fkey"),
                        j =>
                        {
                            j.HasKey("UserId", "BookId").HasName("user_book_pkey");

                            j.ToTable("user_book");

                            j.IndexerProperty<int>("UserId").HasColumnName("user_id");

                            j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        });

                entity.HasMany(d => d.Words)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserWord",
                        l => l.HasOne<Word>().WithMany().HasForeignKey("WordId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("user_word_word_id_fkey"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("user_word_user_id_fkey"),
                        j =>
                        {
                            j.HasKey("UserId", "WordId").HasName("user_word_pkey");

                            j.ToTable("user_word");

                            j.IndexerProperty<int>("UserId").HasColumnName("user_id");

                            j.IndexerProperty<int>("WordId").HasColumnName("word_id");
                        });
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.ToTable("words");

                entity.HasIndex(e => e.Word1, "word_unique")
                    .IsUnique();

                entity.Property(e => e.WordId).HasColumnName("word_id");

                entity.Property(e => e.CorrectTimesInputed).HasColumnName("correct_times_inputed");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ImageSrc)
                    .HasMaxLength(255)
                    .HasColumnName("image_src");

                entity.Property(e => e.IncorrectTimesInputed).HasColumnName("incorrect_times_inputed");

                entity.Property(e => e.IsCorrectInputed).HasColumnName("is_correct_inputed");

                entity.Property(e => e.Word1)
                    .HasMaxLength(255)
                    .HasColumnName("word");

                entity.Property(e => e.WordTranslation)
                    .HasMaxLength(255)
                    .HasColumnName("word_translation");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
