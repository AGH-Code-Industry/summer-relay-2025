public interface IDropZone
{
    public bool OnObjectDropped<T>(T item);

    public bool IsOpen();

}