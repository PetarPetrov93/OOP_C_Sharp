using System.Diagnostics;

namespace _04.PizzaCalories
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] pizzaInfo = Console.ReadLine().Split(" ");
            Pizza pizza = null;
            try
            {
                pizza = new Pizza(pizzaInfo[1]);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return;
            }

            string[] inputDough = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            try
            {
                Dough dough = new Dough(inputDough[1], inputDough[2], int.Parse(inputDough[3]));
                pizza.Dough = dough;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return;
            }

            string cmd;
            while ((cmd = Console.ReadLine()) != "END")
            {
                string[] inputTopping = cmd.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Topping topping = null;
                try
                {
                    topping = new Topping(inputTopping[1], int.Parse(inputTopping[2]));
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return;
                }
                try
                {
                    pizza.AddTopping(topping);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return;
                }
            }
            
            Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories} Calories.");
        }
    }
}