﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailQueue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = ContainerConfigure.Configure();

            using(var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                await app.Run();
            }
        }
    }
}