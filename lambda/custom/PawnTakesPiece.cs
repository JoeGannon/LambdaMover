namespace sampleFactCsharp
{
    using AlexaAPI.Request;

    public class PawnTakesPiece : EchoIntent
    {
        public string Name => "PawnTakesPiece";

        public string Process(Intent intent)
        {
            intent.Slots.TryGetValue("Pawn", out var pawn);
            intent.Slots.TryGetValue("Square", out var square);
            pawn.Value = pawn.Value.Replace(".", "");

            var text = pawn.Value.ToLower() + "x" + square.Value.ToSquare();


            return text;
        }
    }
}