namespace AutoMapper.ReflectionProfile
{
    /// <summary>
    /// Specifies that <see cref="ReflectionProfile"/> should create a map from <see cref="TSource"/> to type that inherits this contract.
    /// </summary>
    /// <typeparam name="TSource">Type to convert from.</typeparam>
    public interface IMapFrom<TSource>
    {
        /// <summary>
        /// Create map.
        /// </summary>
        void Mapping(Profile profile) => profile.CreateMap(typeof(TSource), GetType());
    }
}