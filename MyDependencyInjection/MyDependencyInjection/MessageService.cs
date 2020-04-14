using System;

namespace MyDependencyInjection
{
    public class MessageService
    {
        int random;
        public MessageService()
        {
            random = new Random().Next();
        }
        public string Message()
        {
            return $"Yo {random}";
        }
    }
}
