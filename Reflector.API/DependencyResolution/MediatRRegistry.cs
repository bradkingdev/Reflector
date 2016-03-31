using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediatR;
using StructureMap;
using StructureMap.Configuration.DSL;
using Reflector.Services.CQRS.Commands;

namespace ReflectorAPI.DependencyResolution
{
    public class MediatRRegistry<T> : Registry
    {
        public MediatRRegistry()
        {
            Scan(
                   scan =>
                   {
                       scan.AssemblyContainingType<T>();
                       scan.AssemblyContainingType<IMediator>();
                       scan.WithDefaultConventions();
                       scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                       scan.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                       scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                       scan.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                   });

            For<IMediator>().Use<Mediator>();
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
        }
    }
}