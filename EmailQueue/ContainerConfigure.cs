using Autofac;
using EmailQueue.Model;
using EmailQueue.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailQueue
{
    public class ContainerConfigure
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();

            builder.RegisterType<ApplicationConfiguration>().As<IApplicationConfiguration>();
            builder.RegisterType<DataOperation>().As<IDataOperation>();
            builder.RegisterType<Mailer>().As<IMailer>();


            return builder.Build();
        }
    }
}
