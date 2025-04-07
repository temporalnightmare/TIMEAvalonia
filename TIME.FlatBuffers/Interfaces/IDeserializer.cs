namespace TIME.FlatBuffers.Interfaces
{
    public interface IDeserializer
    {
        T[] DeserializeFrom<T>(string[] files) where T : class, IFlatBufferSerializable<T>;
        T DeserializeFrom<T>(string file) where T : class, IFlatBufferSerializable<T>;
    }
}
