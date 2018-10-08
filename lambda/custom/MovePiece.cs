namespace sampleFactCsharp
{
    using AlexaAPI.Request;

    public class MovePiece : EchoIntent
    {
        public string Name => "MovePiece";

        public string AlgebraicMove(Intent intent)
        {
            intent.Slots.TryGetValue("Piece", out var piece);
            intent.Slots.TryGetValue("File", out var file);
            intent.Slots.TryGetValue("Rank", out var rank);
            intent.Slots.TryGetValue("Square", out var square);

            var text = (piece?.Value?.ToPiece() ?? "") + (file?.Value?.ToLower() ?? "") + (rank?.Value ?? "") + square.Value.ToSquare();

            return text;
        }

        public string SpokenMove(Intent intent)
        {
            intent.Slots.TryGetValue("Piece", out var piece);
            intent.Slots.TryGetValue("File", out var file);
            intent.Slots.TryGetValue("Rank", out var rank);
            intent.Slots.TryGetValue("Square", out var square);

            var text = (piece?.Value ?? "") + " " + (file?.Value?.ToLower() ?? "") + (rank?.Value ?? "") + " " + square.Value.ToSquare();

            return text;
        }
    }
}