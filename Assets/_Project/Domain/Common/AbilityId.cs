using System;

namespace Echo.Domain.Common;

[Serializable]
public readonly record struct AbilityId
{
    public AbilityId(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("AbilityId cannot be empty", nameof(value));
        
        Value = value;
    }

    public string Value { get; }
    
    public static AbilityId None => default;
    public bool IsNone => string.IsNullOrEmpty(Value);
    
    public override string ToString() => Value ?? "<none>";
}
