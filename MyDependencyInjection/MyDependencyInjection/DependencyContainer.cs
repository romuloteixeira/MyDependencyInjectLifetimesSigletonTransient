using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDependencyInjection
{
    public class DependencyContainer
    {
        List<Type> dependencies;
        List<Dependency> dependenciesLifetime;
        public DependencyContainer()
        {
            dependencies = new List<Type>();
            dependenciesLifetime = new List<Dependency>();
        }

        public void AddDependency(Type type)
        {
            dependencies.Add(type);
        }

        public void AddDependency<T>()
        {
            dependencies.Add(typeof(T));
        }

        public void AddSingleton<T>()
        {
            var dependency = new Dependency(typeof(T), DependencyLifetimeEnum.Singleton);
            dependenciesLifetime.Add(dependency);
        }

        public void AddTransient<T>()
        {
            var dependency = new Dependency(typeof(T), DependencyLifetimeEnum.Transient);
            dependenciesLifetime.Add(dependency);
        }

        public Type GetDependency(Type type)
        {
            return dependencies.First(t => t.Name == type.Name);
        }

        public Type GetDependency<T>()
        {
            return GetDependency(typeof(T));
        }

        public Dependency GetDependencyLifetime<T>()
        {
            var type = typeof(T);
            return dependenciesLifetime.First(t => t.Type.Name == type.Name);
        }

        public Dependency GetDependencyLifetime(Type type)
        {
            return dependenciesLifetime.First(t => t.Type.Name == type.Name);
        }
    }
}
