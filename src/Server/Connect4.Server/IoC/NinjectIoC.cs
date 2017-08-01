using Connect4.Server.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4.Server.Data;

namespace Connect4.Server.IoC
{
    public static class NinjectIoC
    {
        public static IKernel Initialize()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IDataProvider>().To<DataProvider>().InSingletonScope();
            kernel.Bind<GameServer>().To<GameServer>().InSingletonScope();
            return kernel;
        }
    }
}