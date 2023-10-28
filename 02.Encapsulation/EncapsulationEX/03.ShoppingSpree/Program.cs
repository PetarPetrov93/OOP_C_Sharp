namespace _03.ShoppingSpree
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            string[] personData = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < personData.Length; i++)
            {
                string[] personAndMoney = personData[i].Split("=", StringSplitOptions.RemoveEmptyEntries);
                string name = personAndMoney[0];
                int money = int.Parse(personAndMoney[1]);

                try
                {
                    Person person = new Person(name, money);
                    people.Add(person);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return;
                }
                
            }

            List<Product> products = new List<Product>();
            string[] productData = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < productData.Length; i++)
            {
                string[] productAndCost = productData[i].Split("=", StringSplitOptions.RemoveEmptyEntries);
                string productName = productAndCost[0];
                int cost = int.Parse(productAndCost[1]);

                try
                {
                    Product product = new Product(productName, cost);
                    products.Add(product);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return;
                }
                
            }

            string cmd;

            while ((cmd = Console.ReadLine()) != "END")
            {
                string buyersName = cmd.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
                string productToBuy = cmd.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];

                people.First(p => p.Name == buyersName).BuyProduct(products.First(p => p.Name == productToBuy));
            }

            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }

        }
    }
}