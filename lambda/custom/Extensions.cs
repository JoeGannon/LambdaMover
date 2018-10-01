﻿namespace sampleFactCsharp
{
    public static class Extensions
    {
        public static string ToPiece(this string piece)
        {
            piece = piece.ToLower();

            var result = "";

            switch (piece)
            {
                case "bishop":
                    result = "B";
                    break;
                case "knight":
                    result = "N";
                    break;
                case "rook":
                    result = "R";
                    break;
                case "queen":
                    result = "Q";
                    break;
                case "king":
                    result = "K";
                    break;
            }

            return result;
        }

        public static string ToSquare(this string square)
        {
            square = square.ToLower();

            square = square.Replace("die", "d");

            if (square.Contains("before"))
                square = "b4";

            return square;
        }
    }
}