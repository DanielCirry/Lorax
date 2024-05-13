using InterviewTestMid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTestMid.Interfaces
{
    public interface ILogger
    {
        void WriteLogMessage(string LogMessage);
        void WriteErrorMessage(Exception Ex);
        Task WriteCSV(List<string> orders);
        List<MaterialDetails>? GetMaterialDetails(string materialName);
        List<MaterialDetails>? GetMaterialDetailsLinq(string materialName);
        bool ModifyPartWeight(decimal value);
    }
}
