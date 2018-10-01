namespace sampleFactCsharp
{
    using AlexaAPI.Request;

    public class CastleKing : EchoIntent
    {
        public string Name => "CastleKing";

        public string Process(Intent intent)
        {
            intent.Slots.TryGetValue("Castle", out var castle);

            var side = "o-o";

            if (castle.Value.ToLower().Contains("long"))
                side = "o-o-o";

            return side;
        }
    }
}