using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BuildTool.SubCommand.Adf
{
    [Command(Name = "Adf:UpdateTrigger", Description = "Update the trigger time for ADF json template file. This only updates tumble window triggers")]
    class AdfUpdateTrigger
    {
        const int LookBakDays = -1;
        const int StartHourForCopy = 0;
        const int StartHourForTransform = 4;
        const int StartHourForRatingTransform = 0;
        const int StartHourForControl = 6;
        const int StartHourForReport = 6;

        protected ILogger _logger;
        protected IConsole _console;

        [Option(CommandOptionType.SingleValue, ShortName = "i", LongName = "input", Description = "Input Adf template file", ValueName = "Adf:UpdateTrigger Input", ShowInHelpText = true)]
        [FileExists]
        public string InputStream { get; set; }

        public AdfUpdateTrigger(ILogger<BuildToolCmd> logger, IConsole console)

        {
            _logger = logger;
            _console = console;
        }
        protected async Task<int> OnExecute(CommandLineApplication app) 
        {
            if (string.IsNullOrEmpty(InputStream))
            {
                _logger.LogError("Input filename mising");
                return 1;
            }

            var outputTemporaryStream = $"{InputStream}.tmp";

            using (var reader = File.OpenText(InputStream))
            {
                var jsonContent = await reader.ReadToEndAsync();
                JObject jsonObject = JObject.Parse(jsonContent);
                JArray resources = (JArray)jsonObject["resources"];
                var jObjects = resources.ToObject<List<JObject>>(); //Get list of objects inside resources array

                foreach (var obj in jObjects)
                {
                    if (obj["type"].ToString().Contains("triggers") && obj["properties"]["type"].ToString() == "TumblingWindowTrigger")
                    {
                        if (obj["name"].ToString().Contains("Trigger-Control", StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime date = DateTime.UtcNow.AddDays(LookBakDays).Date.AddHours(StartHourForControl);
                            obj["properties"]["typeProperties"]["startTime"] = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
                        }
                        else if (obj["name"].ToString().Contains("Trigger-Transform-Rating", StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime date = DateTime.UtcNow.AddDays(LookBakDays).Date.AddHours(StartHourForRatingTransform);
                            obj["properties"]["typeProperties"]["startTime"] = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
                        }
                        else if (obj["name"].ToString().Contains("Trigger-Transform", StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime date = DateTime.UtcNow.AddDays(LookBakDays).Date.AddHours(StartHourForTransform);
                            obj["properties"]["typeProperties"]["startTime"] = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
                        }
                        else if (obj["name"].ToString().Contains("Trigger-Copy", StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime date = DateTime.UtcNow.AddDays(LookBakDays).Date.AddHours(StartHourForCopy);
                            obj["properties"]["typeProperties"]["startTime"] = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
                        }
                        else if (obj["name"].ToString().Contains("Trigger-Report", StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime date = DateTime.UtcNow.AddDays(LookBakDays).Date.AddHours(StartHourForReport);
                            obj["properties"]["typeProperties"]["startTime"] = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
                        }
                    }
                }
                JArray outputArray = JArray.FromObject(jObjects);
                jsonObject.Remove("resources");
                jsonObject.Add("resources", outputArray);
                string updatedJsonString = jsonObject.ToString();
                using (StreamWriter writer = File.CreateText(outputTemporaryStream))
                {
                    
                    await writer.WriteAsync(updatedJsonString);
                }
            }

            // Replace the original file with the updated one
            File.Delete(InputStream);
            File.Move(outputTemporaryStream, InputStream);
            return 0;

        }


    }
}
