namespace sampleFactCsharp
{
    using AlexaAPI.Request;

    public class PromotePiece : EchoIntent
    {
        public string Name => "PromotePiece";

        public string Process(Intent intent)
        {
            intent.Slots.TryGetValue("Square", out var square);
            intent.Slots.TryGetValue("Piece", out var piece);

            var text = square.Value.ToSquare() + "=" + piece.Value.ToPiece();

            return text;
        }
    }
}