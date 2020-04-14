using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDependencyInjection
{
    public class Dependency
    {
        public Dependency(Type type, DependencyLifetimeEnum dependencyLifetime)
        {
            Type = type;
            DependencyLifetime = dependencyLifetime;
        }
        public Type Type { get; set; }
        public DependencyLifetimeEnum DependencyLifetime { get; set; }
        public object Implementation { get; set; }
        public bool IsImplemented { get; set; }

        public void AddImplementation(object ob)
        {
            Implementation = ob;
            IsImplemented = true;
        }
    }
}
