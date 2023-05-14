namespace AutoMapper.ReflectionProfile
{
    /// <summary>
    /// Specifies that <see cref="ReflectionProfile"/> should create a map from <see cref="TSource"/> to <see cref="TDestination"/>.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public interface IMap<TSource, TDestination>
    {
        /// <summary>
        /// Create map.
        /// </summary>
        void Mapping(Profile profile) => profile.CreateMap<TSource, TDestination>();
    }
}