using CsvHelper;
using CsvHelper.Configuration;
using InterviewTestMid.DTOs;
using InterviewTestMid.Interfaces;
using InterviewTestMid.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace InterviewTestMid.Services
{
    internal class Logger : ILogger
    {
        public void WriteLogMessage(string LogMessage)
        {
            var currentTime = DateTime.UtcNow;
            if (string.IsNullOrEmpty(LogMessage))
                throw new ArgumentException($"{currentTime}: Log message not provided", "LogMessage");

            Debug.WriteLine($"{currentTime}: {LogMessage}");
        }

        public void WriteErrorMessage(Exception Ex)
        {
            if (Ex == null)
                throw new ArgumentException("Exception not provided", "Ex");

            Debug.WriteLine($"Error recieved: {Ex.Message}");
            Debug.WriteLine($"{Ex.StackTrace}");
        }

        public async Task WriteCSV(List<string> orders)
        {
            try
            {
                string path = Path.Combine(Environment.CurrentDirectory,"Data", "result.csv");
                if (!File.Exists(path))
                    using (File.Create(path)) { }


                var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };

                using (var writer = new StreamWriter(path, true))
                using (var csv = new CsvWriter(writer, csvConfiguration))
                {
                    foreach (var order in orders) {
                    await csv.WriteRecordsAsync(order); }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MaterialDetails>? GetMaterialDetails(string materialName)
        {
            var sampleData = Path.Combine(Environment.CurrentDirectory, "Data", "SampleData.json");
            if (sampleData == null)
                return null;

            using (StreamReader r = new StreamReader(sampleData))
            {
                string json = r.ReadToEnd();
                List<Part> partOrders = JsonConvert.DeserializeObject<List<Part>>(json, 
                    new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal });
                Part? part = partOrders?.Find(p => p.PartDesc == materialName);
                if (part == null)
                    return null;

                return part.Materials;
            }
        }

        public List<MaterialDetails>? GetMaterialDetailsLinq(string materialName)
        {
            var sampleData = Path.Combine(Environment.CurrentDirectory, "Data", "SampleData.json");
            if (sampleData == null)
                return null;

            using (StreamReader r = new StreamReader(sampleData))
            {
                string json = r.ReadToEnd();
                List<Part> partOrders = JsonConvert.DeserializeObject<List<Part>>(json, 
                    new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal });

                var materials =
                    (from part in partOrders
                    where part.PartDesc == materialName
                    select part.Materials).FirstOrDefault();
                
                if (materials.Count() == 0)
                    return null;

                return materials;
            }
        }

        public bool ModifyPartWeight(decimal value)
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var modifiedSampleData = Path.Combine(parent, "Data", 
                $"{DateTime.Now.ToString("g").Replace('/', '-').Replace(':','-')} ModifiedSampleData.json");

            var sampleData = Path.Combine(Environment.CurrentDirectory, "Data", "SampleData.json");
            if (sampleData == null)
                return false;

            using (StreamWriter w  = new StreamWriter(modifiedSampleData, true))
            using (StreamReader r = new StreamReader(sampleData))
            {
                string json = r.ReadToEnd();
                List<Part> partOrders = JsonConvert.DeserializeObject<List<Part>>(json, 
                    new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal });
                Part? part = partOrders?.Find(p => p.PartDesc == "BLUE TRAY");

                if (part == null)
                    return false;

                part.PartWeight.Value = value;

                var jsonFile = JsonConvert.SerializeObject(partOrders, 
                    new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal });

                w.WriteLine(jsonFile);

                return true;
            }
        }
    }
}
