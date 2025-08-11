using System.Collections.Generic;

public sealed class FixedSizeQueue<T> : Queue<T>
{
    public int FixedCapacity { get; }
    public FixedSizeQueue(int fixedCapacity)
    {
        this.FixedCapacity = fixedCapacity;
    }
    public new T Enqueue(T item)
    {
        base.Enqueue(item);
        if (base.Count > FixedCapacity)
        {
            return base.Dequeue();
        }
        return default;
    }
}