using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDependencyInjection
{
    public class HelloService
    {
        private readonly MessageService messageService;
        int random;

        public HelloService(MessageService messageService)
        {
            this.messageService = messageService;
            random = new Random().Next();
        }

        public void Print(string objectName)
        {
            Console.WriteLine($"Hello World #{random}! ({objectName}) {messageService.Message()}");
        }
    }
}
