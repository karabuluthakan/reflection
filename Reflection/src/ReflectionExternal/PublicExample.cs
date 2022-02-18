namespace ReflectionExternal;

public class PublicExample
{
    private readonly string _default = "Default Value";
    public string PublicValue { get; set; } = "Public";
    private string PrivateValue { get; set; } = "Private";

    public string GetDefaultValue()
    {
        return _default;
    }
}