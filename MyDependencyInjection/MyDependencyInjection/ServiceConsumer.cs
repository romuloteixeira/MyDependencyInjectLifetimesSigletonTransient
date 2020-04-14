using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDependencyInjection
{
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
}
