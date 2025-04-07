using TIME.FlatBuffers.Interfaces;

namespace TIME.FlatBuffers.Helpers;

public class Deserializer : IDeserializer
{
    public T[] DeserializeFrom<T>(string[] files) where T : class, IFlatBufferSerializable<T>
    {
        var result = new T[files.Length];
        for (int i = 0; i < result.Length; i++)
        {
            var file = files[i];
            result[i] = DeserializeFrom<T>(file);
        }
        return result;
    }

    public T DeserializeFrom<T>(string file) where T : class, IFlatBufferSerializable<T>
    {
        // TODO: Implement actual deserialization logic here
        throw new NotImplementedException();
    }
}
