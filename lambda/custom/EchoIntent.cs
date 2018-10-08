using AlexaAPI.Request;

namespace sampleFactCsharp
{
    public interface EchoIntent 
    {
        string Name { get; }
        string AlgebraicMove(Intent intent);
        string SpokenMove(Intent intent);
    }
}
