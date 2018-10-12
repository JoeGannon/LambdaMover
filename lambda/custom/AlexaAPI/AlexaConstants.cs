using System;

namespace AlexaAPI
{
    // see https://developer.amazon.com/public/solutions/alexa/alexa-skills-kit/docs/dialog-interface-reference#delegate
    public static class AlexaConstants
    {
        public const string LaunchRequest = "LaunchRequest";
        public const string IntentRequest = "IntentRequest";
        public const string CanFulfillIntentRequest = "CanFulfillIntentRequest";
        
        public const string AlexaVersion = "1.0";
    }
}

