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

            var service2 = (HelloService)Activator.CreateInstance(typeof(HelloService), messageService);
            var consumer2 = (ServiceConsumer)Activator.CreateInstance(typeof(ServiceConsumer), service2);
            service2.Print(nameof(service2));
            consumer2.Print(nameof(consumer2));

            var container = new DependencyContainer();
            container.AddDependency(typeof(HelloService));
            container.AddDependency<ServiceConsumer>();
            container.AddDependency<MessageService>();
            var resolver = new DependencyResolver(container);
            var service3 = resolver.GetService<ServiceConsumer>();
            service3.Print(nameof(service3));

        }
    }

    public class DependencyResolver
    {
        private readonly DependencyContainer container;

        public DependencyResolver(DependencyContainer container)
        {
            this.container = container;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type type)
        {
            var dependency = container.GetDependency(type);
            var constructor = dependency.GetConstructors().Single();
            var parameters = constructor.GetParameters();
            var parameterImplementation = new object[parameters.Length];

            if (parameters.Any())
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    parameterImplementation[i] = GetService(parameters[i].ParameterType);
                    //parameterImplementation[i] = Activator
                    //               .CreateInstance(parameters[i].ParameterType);
                }

                return Activator.CreateInstance(dependency, parameterImplementation);
            }

            return Activator.CreateInstance(dependency);
        }
    }

    public class DependencyContainer
    {
        List<Type> dependencies = new List<Type>();

        public void AddDependency(Type type)
        {
            dependencies.Add(type);
        }

        public void AddDependency<T>()
        {
            dependencies.Add(typeof(T));
        }

        public Type GetDependency(Type type)
        {
            return dependencies.First(t => t.Name == type.Name);
        }

        public Type GetDependency<T>()
        {
            return GetDependency(typeof(T));
        }
    }

    public enum DependencyLifetime
    {
        Singleton = 0,
        Transient = 1,
    }

    class ServiceConsumer
    {
        private readonly HelloService service;

        public ServiceConsumer(HelloService service)
        {
            this.service = service;
        }

        public void Print(string objectName)
        {
            service.Print(objectName);
        }
    }

    class HelloService
    {
        private readonly MessageService messageService;

        public HelloService(MessageService messageService)
        {
            this.messageService = messageService;
        }

        public void Print(string objectName)
        {
            Console.WriteLine($"Hello World! ({objectName}) {messageService.Message()}");
        }
    }

    public class MessageService
    {
        public string Message()
        {
            return "Yo";
        }
    }
}
