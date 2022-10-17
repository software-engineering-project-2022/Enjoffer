namespace ConsoleApp
{
    public class RandomText
    {
        private string symbolsLatyn = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,~`!@#$%^&*()_+[]{}/|<>";
        private string symbolsCyrillic = "ЙЦУКЕНГШЩЗХЇФІВАПРОЛДЖЄЯЧСМИТЬБЮйцукенгшщзхїфівапролджєячсмитьбю.,~`!@#$%^&*()_+[]{}/|<>";

        public string GenerateText(int size, string alpabet = "latyn")
        {
            Random random = new Random();
            string str = "";

            for (int i = 0; i < size; i++)
            {
                if (alpabet == "latyn")
                {
                    char symbol = symbolsLatyn[random.Next(0, symbolsLatyn.Length - 1)];
                    str += symbol;
                }
                else if (alpabet == "cyrillic")
                {
                    char symbol = symbolsCyrillic[random.Next(0, symbolsCyrillic.Length - 1)];
                    str += symbol;
                }
            }

            return str;
        }

        public string GeneratePassword(int size)
        {
            return BCrypt.Net.BCrypt.HashPassword(GenerateText(size));
        }
    }
}
