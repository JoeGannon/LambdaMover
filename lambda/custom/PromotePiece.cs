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
            intent.Slots.TryGetValue("Pawn", out var pawn);

            var text = square.Value.ToSquare() + "=" + piece.Value.ToPiece();

            if (!string.IsNullOrEmpty(pawn?.Value))
                text = pawn.Value.ToLower() + "x" + square.Value.ToSquare() + "=" + piece.Value.ToPiece();

            return text;
        }
    }
}