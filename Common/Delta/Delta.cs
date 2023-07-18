using System.Reflection;
using Common.Attributes;
using Common.Helpers;

namespace Common.Delta;

public class Delta<TEntity> : Dictionary<string, object>, IDelta<TEntity>
    where TEntity : class
{
    public void Patch(TEntity entity)
    {
        var values=this.ToCamelCase();
        var entityType = typeof(TEntity);
        
        foreach (var key in values.Keys)
        {
            var propertyInfo = entityType.GetProperty(key);
            var newValue = values[key];
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                var canPatchAttr = propertyInfo.GetCustomAttribute<CanPatchAttribute>();
                if (canPatchAttr != null)
                {
                    propertyInfo.SetValue(entity, newValue);
                }
            }
        }        
    }
}