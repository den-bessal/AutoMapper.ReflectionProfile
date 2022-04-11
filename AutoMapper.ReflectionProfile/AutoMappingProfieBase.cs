using System;
using System.Linq;
using System.Reflection;

namespace AutoMapper.ReflectionProfile
{
    public abstract class AutoMappingProfieBase : Profile
    {
        public AutoMappingProfieBase(Assembly assembly)
        {
            Configure(assembly);
        }

        private void Configure(Assembly assembly)
        {
            var types = assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && t.GetInterfaces()
                                              .Any(IsMapInterface));

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
                    || @interface.GetGenericTypeDefinition() == typeof(IMapTo<>));
        }
    }
}