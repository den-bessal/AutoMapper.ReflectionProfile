namespace AutoMapper.ReflectionProfile
{
    /// <summary>
    /// Specifies that <see cref="ReflectionProfile"/> should create a map from type that inherits this contract to <see cref="TDestination"/>.
    /// </summary>
    /// <typeparam name="TDestination">Type to convert to.</typeparam>
    public interface IMapTo<TDestination>
    {
        /// <summary>
        /// Create map.
        /// </summary>
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(TDestination));
    }
}