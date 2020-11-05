using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace UpdateTriggerTime
{
    class Program
    {
        const int LookBakDays = -1;
        const int StartHourForCopy = 0;
        const int StartHourForTransform = 4;
        const int StartHourForRatingTransform = 0;
        const int StartHourForControl = 6;
        const int StartHourForReport = 6;

        static void Main(string[] args)
        {
            string jsonContent = File.ReadAllText(@"C:\src\ComFin.Controls\.ev2\Templates\ARMTemplateForFactory.Control.json");
            //dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            //jsonObj["resources"].
            JObject jsonObject = JObject.Parse(jsonContent);
            JArray resources = (JArray)jsonObject["resources"];
            var jObjects = resources.ToObject<List<JObject>>(); //Get list of objects inside array
            
            foreach (var obj in jObjects)                             //Loop through on a list
            {
                if (obj["type"].ToString().Contains("triggers") && obj["properties"]["type"].ToString()=="TumblingWindowTrigger")
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
            File.WriteAllText(@"E:\code\csharp\Tools\UpdateTriggerTime\Data\ARMTemplateForFactory.Control.json", updatedJsonString);


        }
    }
}
