using System;
using System.Linq;
using System.Reflection;

namespace AutoMapper.ReflectionProfile
{
    /// <summary>
    /// Implements a profile for AutoMapper that allows to create maps according to contracts of interfaces <see cref="IMapFrom{T}"/> and <see cref="IMapTo{T}"/>.
    /// All assemblies connected to the application will be scanned.
    /// </summary>
    public class ReflectionProfile : Profile
    {
        public ReflectionProfile()
        {
            Configure(AppDomain.CurrentDomain.GetAssemblies());
        }

        private void Configure(params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetTypes()
                                  .Where(t => !t.IsAbstract && t.GetInterfaces().Any(IsMapInterface)));

            foreach (var type in types)
            {
                var methods = type
                    .GetInterfaces()
                    .Where(IsMapInterface)
                    .Select(i => i.GetMethod("Mapping"));

                var instance = Activator.CreateInstance(type);

                foreach (var method in methods)
                    method.Invoke(instance, new[] { this });
            }
        }

        private static bool IsMapInterface(Type @interface)
        {
            return @interface.IsGenericType 
                && (@interface.GetGenericTypeDefinition() == typeof(IMapFrom<>) 
                    || @interface.GetGenericTypeDefinition() == typeof(IMapTo<>)
                    || @interface.GetGenericTypeDefinition() == typeof(IMap<,>));
        }
    }
}