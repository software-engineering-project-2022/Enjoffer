namespace ConsoleApp
{
    public class Professions
    {
        public readonly string Developer;
        public readonly string Doctor;
        public readonly string Mathematician;

        public Professions()
        {
            Developer = "Розробник";
            Doctor = "Терапевт";
            Mathematician = "Математик";
        }
        public static readonly List<string> professions = new List<string>()
        {
            new Professions().Developer,
            new Professions().Doctor,
            new Professions().Mathematician
        };

    }
}