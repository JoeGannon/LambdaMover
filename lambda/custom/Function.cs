using System;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using AlexaAPI;
using AlexaAPI.Request;
using AlexaAPI.Response;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace sampleFactCsharp
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.IotData;

    public class Function
    {
        const string LOCALENAME = "locale";
        const string USA_Locale = "en-US";

        private static readonly string _endpoint = Environment.GetEnvironmentVariable("Endpoint");
        private static readonly string _topic = Environment.GetEnvironmentVariable("Topic");
        private static readonly string _accessKey = Environment.GetEnvironmentVariable("AccessKey");
        private static readonly string _secretKey = Environment.GetEnvironmentVariable("Secret");

        private readonly AmazonIotDataClient _client = new AmazonIotDataClient(_accessKey, _secretKey, new AmazonIotDataConfig
        {
            RegionEndpoint = RegionEndpoint.USEast1,
            ServiceURL = _endpoint
        });

        private readonly List<EchoIntent> _intents = new List<EchoIntent>
        {
            new MovePiece(),
            new TakePiece(),
            new PawnTakesPiece(),
            new PromotePiece(),
            new CastleKing()
        };

        public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var response = new SkillResponse();
            response.Response = new ResponseBody();
            response.Response.ShouldEndSession = false;
            response.Version = AlexaConstants.AlexaVersion;
          
            var spokenMove = "";

            if (input.Request.Type.Equals(AlexaConstants.LaunchRequest))
            {
                return ProcessLaunch(input);
            }

            else if (input.Request.Type.Equals(AlexaConstants.IntentRequest))
            {
                var intent = _intents.First(x => x.Name == input.Request.Intent.Name);

                var algebraicMove = intent.AlgebraicMove(input.Request.Intent).Replace(".", "");

                await _client.PublishAsync(new Amazon.IotData.Model.PublishRequest()
                {
                    Topic = _topic,
                    Payload = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(algebraicMove)),
                    Qos = 0
                });

                //some reason the file b comes with a dot
                spokenMove = intent.SpokenMove(input.Request.Intent).Replace(".", "");
            }

            else if (input.Request.Type.Equals(AlexaConstants.CanFulfillIntentRequest))
            {
                var intent = _intents.First(x => x.Name == input.Request.Intent.Name);

                response.Response.canFulfillIntent = new CanFulfillIntent();

                return response;
            }


            response.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = spokenMove
            };

            return response;
        }

        private static SkillResponse ProcessLaunch(SkillRequest input)
        {
            var response = new SkillResponse
            {
                Response = new ResponseBody { ShouldEndSession = false , OutputSpeech = new PlainTextOutputSpeech
                {
                    Text = "Ready"
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
