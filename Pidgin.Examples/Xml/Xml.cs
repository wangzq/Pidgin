using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Pidgin.Examples.Xml;

public class Tag : IEquatable<Tag>
{
    public string Name { get; }

    public IEnumerable<Attribute> Attributes { get; }

    public IEnumerable<Tag>? Content { get; }

    public string? InnerText { get; }

    public Tag(string name, IEnumerable<Attribute> attributes)
    {
        Name = name;
        Attributes = attributes;
    }

    public Tag(string name, IEnumerable<Attribute> attributes, string? innerText)
    {
        Name = name;
        Attributes = attributes;
        InnerText = innerText;
    }

    public Tag(string name, IEnumerable<Attribute> attributes, IEnumerable<Tag>? content)
    {
        Name = name;
        Attributes = attributes;
        Content = content;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals((Tag)obj);
    }

    public override int GetHashCode() => HashCode.Combine(Name, Attributes, Content);

    public bool Equals(Tag? other)
        => Name == other?.Name
           && Attributes.SequenceEqual(other.Attributes)
           && (SameContent(other!) || SameInnerText(other!));

    private bool SameContent(Tag other) =>
        (Content is null && other.Content is null) || Content!.SequenceEqual(other.Content!);

    private bool SameInnerText(Tag other) =>
        (InnerText is null && other.InnerText is null) || InnerText == other.InnerText;
}

[SuppressMessage(
    "naming",
    "CA1711:Rename type name so that it does not end in 'Stream'",
    Justification = "Example code"
)]
public class Attribute : IEquatable<Attribute>
{
    public string Name { get; }

    public string Value { get; }

    public Attribute(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals((Attribute)obj);
    }

    public bool Equals(Attribute? other)
        => Name == other?.Name
           && Value == other.Value;

    public override int GetHashCode() => HashCode.Combine(Name, Value);
}
