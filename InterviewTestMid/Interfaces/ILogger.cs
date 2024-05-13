using InterviewTestMid.Models;

namespace InterviewTestMid.Interfaces
{
    public interface ILogger
    {
        void WriteLogMessage(string LogMessage);
        void WriteErrorMessage(Exception Ex);
        Task WriteCSV(List<string> orders);
        List<Part>? GetData();
        List<Part>? GetDataByPath(string path);
        List<MaterialDetails>? GetMaterialDetailsLinq(string materialName);
        string GetFilePathToSavedData(string fileName);
        string ModifyPartWeightValue(string partDesc, decimal value, string fileName);
    }
}
