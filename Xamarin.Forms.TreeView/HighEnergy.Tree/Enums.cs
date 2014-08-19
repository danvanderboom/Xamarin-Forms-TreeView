using System;

namespace HighEnergy.Collections
{
    public enum UpDownTraversalType
    {
        TopDown,
        BottomUp
    }

    public enum DepthBreadthTraversalType
    {
        DepthFirst,
        BreadthFirst
    }

    // TODO: bubble or tunnel each of the following event types
    [Flags]
    public enum NodeChangeType
    {
        NewRoot = 1, // NewParent at the top
        AncestorInserted = 2,
        AncestorNulled = 4, // AncestorNulled
        ParentInserted = 8,
        ParentNulled = 16,
        ChildAdded = 32,
        ChildRemoved = 64,
        DescendantAdded = 128,
        DescendantRemoved = 256
    }

    public enum NodeRelationType
    {
        Ancestor, // excludes direct Parent
        Parent,
        Self,
        Child,
        Descendant
    }
}