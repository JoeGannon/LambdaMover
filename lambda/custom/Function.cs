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
            new PawnTakesPiece()
        };


        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var skillResponse = new SkillResponse
            {
                Response = new ResponseBody()
            };
            skillResponse.Response.ShouldEndSession = false;

            var text = "";

            if (input.Request.Type.Equals(AlexaConstants.LaunchRequest))
            {
                return ProcessLaunch(input);
            }

            if (input.Request.Type.Equals(AlexaConstants.IntentRequest))
            {
                var intent = _intents.First(x => x.Name == input.Request.Intent.Name);

                text = intent.Process(input.Request.Intent);
            }

            skillResponse.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = text
            };

            return skillResponse;
        }

        private static SkillResponse ProcessLaunch(SkillRequest input)
        {
            var response = new SkillResponse
            {
                Response = new ResponseBody { ShouldEndSession = false },
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
