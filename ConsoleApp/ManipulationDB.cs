using Npgsql;

namespace ConsoleApp
{
    public class ManipulationDB : Database
    {
        private readonly RandomText randomGenerate = new RandomText();
        private readonly Random rnd = new Random();
        public ManipulationDB(string dbName) : base(dbName)
        {
        }

        public void TablesInsertion(int numberOfUsers)
        {
            for (var i = 0; i < numberOfUsers; ++i)
            {
                UsersInsert();
                WordsInsert();
                SentencesInsert();
                BookInsert();
                AdviceInsert();
            }

        }

        public void TablesDeletion()
        {
            Deletion("users");
            Deletion("words");
            Deletion("books");
            Deletion("sentences");
            Deletion("advice");
        }

        public void Deletion(string tableName)
        {
            Command = new NpgsqlCommand($"DELETE FROM {tableName};", Connection);
            Command.ExecuteNonQuery();
        }

        public void UsersInsert()
        {
            string username = randomGenerate.GenerateText(rnd.Next(1, 48));
            string user_password = randomGenerate.GeneratePassword(rnd.Next(6, 36));
            var profession = Professions.professions[rnd.Next(0, Professions.professions.Count)];
            try
            {
                Command = new NpgsqlCommand($"INSERT INTO users(username, user_password, profession) VALUES ('{username}', '{user_password}', '{profession}');", Connection);
                Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WordsInsert()
        {
            string word = randomGenerate.GenerateText(rnd.Next(1, 38));
            string word_translation = randomGenerate.GenerateText(rnd.Next(1, 38), "cyrillic");
            string image_src = randomGenerate.GenerateText(rnd.Next(1, 38), "cyrillic");
            string date = new DateTime(DateTime.Now.Year, rnd.Next(1, DateTime.Now.Month), rnd.Next(1, DateTime.Now.Day)).ToString("yyyy-MM-dd");
            bool is_correct_inputed = Convert.ToBoolean(rnd.Next(0, 2));
            int correct_times_inputed = rnd.Next(0, 12);
            int incorrect_times_inputed = rnd.Next(0, 12);
            try
            {
                Command = new NpgsqlCommand($"INSERT INTO words(word," +
                    $" word_translation, image_src, date, is_correct_inputed, correct_times_inputed, incorrect_times_inputed) VALUES ('{word}', '{word_translation}', '{image_src}', '{date}', '{is_correct_inputed}', '{correct_times_inputed}', '{incorrect_times_inputed}');",
                    Connection);
                Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SentencesInsert()
        {
            string sentence = randomGenerate.GenerateText(rnd.Next(1, 120));
            try
            {
                Command = new NpgsqlCommand($"INSERT INTO sentences(sentence) VALUES ('{sentence}')", Connection);
                Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void BookInsert()
        {
            string title = randomGenerate.GenerateText(rnd.Next(1, 30));
            string description = randomGenerate.GenerateText(rnd.Next(10, 120), "cyrillic");
            string book_content = randomGenerate.GenerateText(rnd.Next(200, 10000));
            string book_cover_img = randomGenerate.GenerateText(rnd.Next(1, 100));
            string author = randomGenerate.GenerateText(rnd.Next(1, 30));
            try
            {
                Command = new NpgsqlCommand($"INSERT INTO books(title, description, book_content," +
                    $" book_cover_img, author) VALUES ('{title}', '{description}', '{book_content}'," +
                    $"'{book_cover_img}', '{author}')", Connection);
                Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AdviceInsert()
        {
            string advice = randomGenerate.GenerateText(rnd.Next(1, 50));
            try
            {
                Command = new NpgsqlCommand($"INSERT INTO advice(advice_name) VALUES ('{advice}')", Connection);
                Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private List<string> get_cols(string table_name)
        {
            Execute($"SELECT column_name FROM information_schema.columns WHERE table_name = '{table_name}'");

            var lst = new List<string>();

            while (Reader.Read())
            {
                lst.Add((string)Reader["column_name"]);
            }

            Reader.Close();

            return lst;
        }

        private List<string> get_tables()
        {
            Execute($"SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type='BASE TABLE' ORDER BY table_name ");

            var lst = new List<string>();

            while (Reader.Read())
            {
                lst.Add((string)Reader["table_name"]);
            }

            Reader.Close();

            return lst;
        }

        public void Print()
        {
            var tables = get_tables();

            foreach (var table in tables)
            {
                var cols = get_cols(table);

                var query = $"SELECT * FROM {table}";

                Execute(query);

                Console.WriteLine($"\nTable \"{table}\" :");

                Console.WriteLine(String.Join("\t\t", cols.ToArray()));
                Console.WriteLine("======================================================================");

                while (Reader.Read())
                {
                    var row = "";

                    foreach (var column in cols)
                    {
                        row += Reader[column] + "\t\t";
                    }

                    Console.WriteLine(row);
                }
                Console.WriteLine("======================================================================");
                Reader.Close();
            }
        }
    }
}