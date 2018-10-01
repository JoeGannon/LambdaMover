namespace sampleFactCsharp
{
    using AlexaAPI.Request;

    public class MovePiece : EchoIntent
    {
        public string Name => "MovePiece";

        public string Process(Intent intent)
        {
            intent.Slots.TryGetValue("Piece", out var piece);
            intent.Slots.TryGetValue("File", out var file);
            intent.Slots.TryGetValue("Rank", out var rank);
            intent.Slots.TryGetValue("Square", out var square);

            var text = (piece?.Value?.ToPiece() ?? "") + (file?.Value?.ToLower() ?? "") + (rank?.Value ?? "") + square.Value.ToSquare();

            //if (square == null)
            //{
            //    text = "square was null";
            //}

            return text;
            //$"{Name}: \n\r" +
            //   $"Piece: {piece?.Value} \n\r " +
            //   $"File: {file?.Value} \n\r " +
            //   $"Rank: {rank?.Value} \n\r " +
            //   $"Square: {square?.Value}";
        }
    }
}