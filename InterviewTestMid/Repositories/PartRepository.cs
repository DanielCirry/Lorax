﻿using CsvHelper.Configuration;
using CsvHelper;
using InterviewTestMid.Interfaces;
using InterviewTestMid.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace InterviewTestMid.Repositories
{
    public class PartRepository : IPartRepository
    {
        public string CreateCSVFile()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Data", "result.csv");
            if (!File.Exists(path))
                using (File.Create(path)) { }

            return path;
        }

        public async Task WriteDataToCSVFile(List<string> orders, string path)
        {
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            using (var writer = new StreamWriter(path, true))
            using (var csv = new CsvWriter(writer, csvConfiguration))
            {
                foreach (var order in orders)
                {
                    await csv.WriteRecordsAsync(order);
                }
            }
        }

        public List<MaterialDetails>? GeMaterialListByMaterialName(string materialName)
        {
            string? sampleData = GetDataPath();
            List<Part>? parts = GetListOfPartsFromFile(sampleData);
            return GetListOfMaterialsByPartDescQuery(materialName, parts);
        }

        public List<Part>? GeData()
        {
            string? sampleData = GetDataPath();
            return GetListOfPartsFromFile(sampleData);
        }

        public bool WriteDataOnFile(List<Part>? sampleData, string modifiedSampleDataPath)
        {
            using (StreamWriter w = new StreamWriter(modifiedSampleDataPath))
            {
                var jsonFile = JsonConvert.SerializeObject(sampleData,
                    new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal });

                w.WriteLine(jsonFile);

                return true;
            }
        }

        public string GetFilePathToSavedData(string fileName)
        {
            string? parent = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
            if (parent == null)
                return string.Empty;

            return Path.Combine(parent, "InterviewTestMid", "Data", fileName);
        }

        private string? GetDataPath() => 
            Path.Combine(Environment.CurrentDirectory, "Data", "SampleData.json") ?? null;

        public List<Part>? GetListOfPartsFromFile(string? pathToData)
        {
            if (string.IsNullOrWhiteSpace(pathToData))
                return null;
            try
            {
                using (StreamReader r = new StreamReader(pathToData))
                {
                    string json = r.ReadToEnd();
                    List<Part>? partOrders = JsonConvert.DeserializeObject<List<Part>>(json,
                        new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal });

                    if (partOrders?.Count == 0)
                        return null;

                    return partOrders;
                }

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        private static List<MaterialDetails>? GetListOfMaterialsByPartDescQuery(string materialName, List<Part>? sampleData)
        {
            var materials =
                (from part in sampleData
                 where part.PartDesc == materialName
                 select part.Materials).FirstOrDefault();

            if (materials?.Count() == 0)
                return null;

            return materials;
        }
    }
}
