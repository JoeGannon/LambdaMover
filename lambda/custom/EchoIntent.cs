using AlexaAPI.Request;

namespace sampleFactCsharp
{
    public interface EchoIntent 
    {
        string Name { get; }
        string Process(Intent intent);
    }
}
