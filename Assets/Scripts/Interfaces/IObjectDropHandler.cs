interface IObjectDropHanlder
{
    /// <summary>
    /// Should return true if the object was handled correctly, false otherwise
    /// </summary>
    bool HandleObjectDropped(IDropZone zone);
}