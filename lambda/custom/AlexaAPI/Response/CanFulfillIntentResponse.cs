namespace AlexaAPI.Response
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
  
    public class Slot
    {
        public string canUnderstand { get; set; } = "YES";
        public string canFulfill { get; set; } = "YES";
    }

    public class CanFulfillIntent
    {
        public string canFulfill { get; set; } = "YES";

        public Dictionary<string, Slot> slots { get; set; } = new Dictionary<string, Slot>
        {
            {"Pawn", new Slot()},
            {"Square", new Slot()},
        };
    }
}