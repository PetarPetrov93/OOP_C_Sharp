namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            StackOfStrings strings = new StackOfStrings();

            Console.WriteLine(strings.IsEmpty());

            strings.Push("asd");
            strings.Push("dfg");
            strings.Push("xcv");

            Console.WriteLine(strings.IsEmpty());

            Stack<string> defaultStack = new Stack<string>();
            defaultStack.Push("aaaaa");
            defaultStack.Push("bbbbb");
            defaultStack.Push("ccccc");

            strings.AddRange(defaultStack);

            Console.WriteLine(strings.IsEmpty());
            
        }
    }
}