using System.Reflection;
using ReflectionExternal;
using Sigil;

#pragma warning disable CS8601
#pragma warning disable CS8603
#pragma warning disable CS8602
#pragma warning disable CS8604

namespace ReflectionConsole;

public class ReflectionUsage
{
    private const string PrivatePropertyName = "PrivateValue";
    private const string DefaultPropertyName = "_default";

    public static string GetSimple()
    {
        var instance = new PublicExample();
        return instance.PublicValue;
    }

    public static string GetDefaultValue()
    {
        var instance = new PublicExample();
        instance.GetType()
            .GetField(DefaultPropertyName, BindingFlags.Instance | BindingFlags.NonPublic)
            .SetValue(instance, "DEFAULT VALUE IS CHANGED");
        return instance.GetDefaultValue();
    } 
    
    public static string TraditionalReflection()
    {
        var instance = new PublicExample();
        var propertyInfo = instance.GetType().GetProperty(PrivatePropertyName,
            BindingFlags.Instance | BindingFlags.NonPublic);
        var value = propertyInfo.GetValue(instance);
        return value.ToString();
    }

    public static string OptimizedTraditionalReflection()
    {
        var instance = new PublicExample();
        var value = CachedProperty.GetValue(instance);
        return value.ToString();
    }

    public static string CompiledDelegate()
    {
        var instance = new PublicExample();
        return GetPropertyDelegate(instance);
    }

    public static string Emitted_IL_Version()
    {
        var instance = Activator.CreateInstance(InternalType);
        return GetPropertyEmittedDelegate(instance!);
    }

    private static readonly PropertyInfo CachedProperty = typeof(PublicExample)
        .GetProperty(PrivatePropertyName, BindingFlags.Instance | BindingFlags.NonPublic);

    private static readonly Func<PublicExample, string> GetPropertyDelegate =
        (Func<PublicExample, string>)
        Delegate.CreateDelegate(
            typeof(Func<PublicExample, string>),
            CachedProperty.GetGetMethod(true));

    private static readonly Type InternalType = Type.GetType("ReflectionExternal.InternalExample,ReflectionExternal");

    private static readonly PropertyInfo CachedInternalProperty = InternalType
        .GetProperty(PrivatePropertyName, BindingFlags.Instance | BindingFlags.NonPublic);

    private static readonly Emit<Func<object, string>> GetPropertyEmitter =
        Emit<Func<object, string>>.NewDynamicMethod("GetInternalPropertyValue")
            .LoadArgument(0)
            .CastClass(InternalType)
            .Call(CachedInternalProperty.GetGetMethod(true))
            .Return();

    private static readonly Func<object, string> GetPropertyEmittedDelegate = GetPropertyEmitter.CreateDelegate();
 
}