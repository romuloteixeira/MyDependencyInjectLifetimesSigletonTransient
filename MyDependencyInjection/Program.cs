using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            //var t = typeof(ServiceConsumer)
            //    .GetConstructors()
            //    .Select(constructor => constructor.GetParameters());

            //return;

            MessageService messageService = new MessageService();
            var service = new HelloService(messageService);
            var consumer = new ServiceConsumer(service);
            service.Print(nameof(service));
            consumer.Print(nameof(consumer));

            var service12 = (HelloService)Activator.CreateInstance(typeof(HelloService), messageService);
            var consumer12 = (ServiceConsumer)Activator.CreateInstance(typeof(ServiceConsumer), service12);
            service12.Print(nameof(service12));
            consumer12.Print(nameof(consumer12));

            var container = new DependencyContainer();
            container.AddDependency(typeof(HelloService));
            container.AddDependency<ServiceConsumer>();
            container.AddDependency<MessageService>();
            var resolver = new DependencyResolver(container);
            var service13 = resolver.GetService<ServiceConsumer>();
            service13.Print(nameof(service13));

            container.AddTransient<ServiceConsumer>();

            container.AddSingleton<HelloService>();
            //container.AddTransient<HelloService>();
            
            container.AddSingleton<MessageService>();

            var service2 = resolver.GetServiceLifetime<ServiceConsumer>();
            service2.Print(nameof(service2));
            var service3 = resolver.GetServiceLifetime<ServiceConsumer>();
            service3.Print(nameof(service3));
            service3.Print(nameof(service3));
            var service4 = resolver.GetServiceLifetime<ServiceConsumer>();
            service4.Print(nameof(service4));
        }
    }
}
