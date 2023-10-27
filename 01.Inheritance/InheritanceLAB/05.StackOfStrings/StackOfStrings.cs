using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty() => this.Count() == 0 ? true : false;

        public void AddRange(Stack<string> stack) 
        {
            foreach (var item in stack)
            {
                Push(item);
            }
        }
        
    }
}
