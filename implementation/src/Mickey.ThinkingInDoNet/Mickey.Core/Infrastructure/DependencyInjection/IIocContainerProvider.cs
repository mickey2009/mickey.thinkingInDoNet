using Autofac;

namespace Mickey.Core.Infrastructure.DependencyInjection
{
    public interface IIocContainerProvider
    {
        ILifetimeScope Current { get; }
    }
}
