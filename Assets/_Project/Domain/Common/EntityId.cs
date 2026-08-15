using System;

namespace Echo.Domain.Common;

[Serializable]
public readonly record struct EntityId
{
    public EntityId(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("EntityId cannot be empty", nameof(value));
            
        Value = value;
    }
        
    public string Value { get; }
    public static EntityId None => default;
    public bool IsNone => string.IsNullOrEmpty(Value);
        
    public override string ToString() => Value ?? "<none>";
}