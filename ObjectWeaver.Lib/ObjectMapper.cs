using ObjectWeaver.Lib.Interface;

namespace ObjectWeaver.Lib
{
    public static class ObjectMapper
    {
        public static TDestination MapTo<TDestination>(this System.Object @object) where TDestination : class, new()
        {
            TDestination destination =
                    new();

            System.Type sourceType =
                @object.GetType();
            System.Type targetType =
                destination.GetType();

            MapFields(@object, sourceType, targetType, destination);

            MapProperties(@object, sourceType, targetType, destination);

            return destination;
        }

        public static TDestination MapTo<TSource, TDestination>(this System.Object @object, ICustomMapper<TSource, TDestination> customMapper)
            where TSource : class where TDestination : class
        {
            return customMapper.Map((TSource)@object);
        }

        private static void MapProperties<TDestination>(System.Object @object, Type sourceType, Type targetType,
            TDestination destination) where TDestination : class
        {
            System.Reflection.PropertyInfo[] sourceProperties =
                sourceType.GetProperties(bindingAttr: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            System.Reflection.PropertyInfo[] targetProperties =
                targetType.GetProperties(bindingAttr: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            foreach (System.Reflection.PropertyInfo sourceProperty in sourceProperties)
            {
                foreach (System.Reflection.PropertyInfo targetProperty in targetProperties)
                {
                    if (sourceProperty.Name != targetProperty.Name ||
                        sourceProperty.PropertyType != targetProperty.PropertyType ||
                        !targetProperty.CanWrite)
                        continue;
                    System.Object value =
                        sourceProperty.GetValue(@object);
                    targetProperty.SetValue(destination, value);
                }
            }
        }

        private static void MapFields<TDestination>(System.Object @object, Type sourceType, Type targetType, TDestination destination)
            where TDestination : class
        {
            System.Reflection.FieldInfo[] sourceFields =
                sourceType.GetFields(bindingAttr: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            System.Reflection.FieldInfo[] targetFields =
                targetType.GetFields(bindingAttr: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            foreach (System.Reflection.FieldInfo sourceField in sourceFields)
            {
                foreach (System.Reflection.FieldInfo targetField in targetFields)
                {
                    if (sourceField.Name != targetField.Name ||
                        sourceField.FieldType != targetField.FieldType)
                        continue;
                    System.Object value =
                        sourceField.GetValue(@object);
                    targetField.SetValue(destination, value);
                }
            }
        }
    }
}
