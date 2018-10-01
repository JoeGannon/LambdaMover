using System;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using AlexaAPI;
using AlexaAPI.Request;
using AlexaAPI.Response;
using System.IO;
using System.Text.RegularExpressions;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace sampleFactCsharp
{
    using System.Linq;

    public class Function
    {
        const string LOCALENAME = "locale";
        const string USA_Locale = "en-US";

        private readonly List<EchoIntent> _intents = new List<EchoIntent>
        {
            new MovePiece(),
            new TakePiece(),
            new PawnTakesPiece(),
            new PromotePiece()
        };


        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var response = new SkillResponse();
            response.Response = new ResponseBody();
            response.Response.ShouldEndSession = false;
            response.Version = AlexaConstants.AlexaVersion;

            var text = "Hello";

            if (input.Request.Type.Equals(AlexaConstants.LaunchRequest))
            {
                return ProcessLaunch(input);
            }

            if (input.Request.Type.Equals(AlexaConstants.IntentRequest))
            {
                var intent = _intents.FirstOrDefault(x => x.Name == input.Request.Intent.Name);

                if (intent != null)
                    text = intent.Process(input.Request.Intent);
            }

            response.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = text
            };

            return response;
        }

        private static SkillResponse ProcessLaunch(SkillRequest input)
        {
            var response = new SkillResponse
            {
                Response = new ResponseBody { ShouldEndSession = false , OutputSpeech = new PlainTextOutputSpeech
                {
                    Text = "Hello"
                }},
                
                Version = AlexaConstants.AlexaVersion
            };

            var locale = input.Request.Locale;
            if (string.IsNullOrEmpty(locale))
            {
                locale = USA_Locale;
            }

            IOutputSpeech prompt = new PlainTextOutputSpeech();
            (prompt as PlainTextOutputSpeech).Text = "Hello";
            response.Response.OutputSpeech = prompt;
            response.SessionAttributes = new Dictionary<string, object>() { { LOCALENAME, locale } };

            return response;
        }

        private void Log(ILambdaContext context, string text)
        {
            context.Logger.LogLine(text);
        }
    }
}
