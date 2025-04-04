namespace ObjectWeaver.Lib.Interface;

public interface ICustomMapper<in TSource, out TDestination> where TSource : class where TDestination : class
{
    public TDestination Map(TSource source);
}