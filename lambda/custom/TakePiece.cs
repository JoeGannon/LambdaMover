namespace sampleFactCsharp
{
    using AlexaAPI.Request;

    public class TakePiece : EchoIntent
    {
        public string Name => "TakePiece";

        public string Process(Intent intent)
        {
            intent.Slots.TryGetValue("Piece", out var piece);
            intent.Slots.TryGetValue("File", out var file);
            intent.Slots.TryGetValue("Rank", out var rank);
            intent.Slots.TryGetValue("Square", out var square);

            var text = piece.Value?.ToPiece() + (file?.Value?.ToLower() ?? "") + (rank?.Value ?? "") + "x" + square.Value.ToSquare();

            return text;
        }
    }
}