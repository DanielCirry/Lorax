using CsvHelper;
using CsvHelper.Configuration;
using InterviewTestMid.Interfaces;
using InterviewTestMid.Models;
using System.Diagnostics;
using System.Globalization;

namespace InterviewTestMid.Services
{
    public class Logger(IPartRepository partRepository) : ILogger
    {
        public IPartRepository _partRepository = partRepository;

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
            catch (Exception ex)
            {
                WriteErrorMessage(ex);
            }
        }

        public List<Part>? GetData() => _partRepository.GeData();
        public List<Part>? GetDataByPath(string path) => _partRepository.GeData();

        public List<MaterialDetails>? GetMaterialDetailsLinq(string materialName)
        {
            return _partRepository.GeMaterialListByMaterialName(materialName);
        }

        public string GetFilePathToSavedData(string fileName)
        {
            try
            {
                return _partRepository.GetFilePathToSavedData(fileName);
            }
            catch (Exception ex)
            {
                WriteErrorMessage(ex);
                throw new Exception();
            }
        }

        public string ModifyPartWeightValue(string partDesc, decimal value, string fileName)
        {
            try
            {
                var sampleData = _partRepository.GeData();
                if (sampleData == null)
                    return string.Empty;

                string pathToSavedData = _partRepository.GetFilePathToSavedData(fileName);

                Part? part = sampleData?.Find(p => p.PartDesc == partDesc);
                if (part == null)
                    return string.Empty;

                part.PartWeight.Value = value;

                if (_partRepository.WriteDataOnFile(sampleData, pathToSavedData))
                    return pathToSavedData;

            }
            catch (Exception ex)
            {
                WriteErrorMessage(ex);
                throw new Exception();
            }

            return string.Empty;
        }
    }
}
