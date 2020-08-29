using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDependencyInjection
{
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

        public T GetServiceLifetime<T>()
        {
            return (T)GetServiceLifetime(typeof(T));
        }

        public object GetServiceLifetime(Type type)
        {
            var dependency = container.GetDependencyLifetime(type);
            var constructor = dependency.Type.GetConstructors().Single();
            var parameters = constructor.GetParameters();
            var parameterImplementations = new object[parameters.Length];

            if (parameters.Any())
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameterImplementations[i] = GetServiceLifetime(parameters[i].ParameterType);
                }

                //return Activator.CreateInstance(dependency.Type, parameterImplementation);
                return CreateImplementation(dependency, t => Activator.CreateInstance(t, parameterImplementations));
            }

            var implementation = CreateImplementation(dependency, t => Activator.CreateInstance(t));

            return implementation;
        }

        private static object CreateImplementation(Dependency dependency, Func<Type, object> factory)
        {
            if (dependency.IsImplemented)
            {
                return dependency.Implementation;
            }

            var implementation = factory(dependency.Type);
            if (dependency.DependencyLifetime == DependencyLifetimeEnum.Singleton)
            {
                dependency.AddImplementation(implementation);
            }

            return implementation;
        }
    }
}
