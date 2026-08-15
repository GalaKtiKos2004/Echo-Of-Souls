using System;

namespace Echo.Domain.Common;

public readonly record struct ItemId
{
    public ItemId(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("ItemId cannot be empty", nameof(value));
        Value = value;
    }

    public string Value { get; }
    public static ItemId None => default;
    public bool IsNone => string.IsNullOrEmpty(Value);

    public override string ToString() => Value ?? "<none>";
}
