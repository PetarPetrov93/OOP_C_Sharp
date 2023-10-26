namespace Restaurant
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Cake cake = new Cake("Garash");

            Fish fish = new Fish("Catfish", 10);

            Soup soup = new Soup("Chiken", 5, 250);
        }
    }
}