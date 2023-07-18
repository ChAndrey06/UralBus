using AutoMapper;

namespace Common.Mapping
{
    public abstract class MapperFactoryBase
    {
        //  protected abstract MapperConfiguration MapperConfig { get; }

        public static IMapper GetMapper(Action<IMapperConfigurationExpression> configureAction = null)
        {
            
                
                var config = new MapperConfiguration(cfg =>
                {
                    // Define the default AutoMapper configuration here
                    
                    // Add more mappings as needed

                    // Apply any additional configuration provided by the caller
                    configureAction?.Invoke(cfg);
                });
                

                return config.CreateMapper();
            
        }
    }
}