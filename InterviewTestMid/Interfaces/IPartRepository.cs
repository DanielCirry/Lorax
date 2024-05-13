using InterviewTestMid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTestMid.Interfaces
{
    public interface IPartRepository
    {
        List<MaterialDetails>? GeMaterialListByMaterialName(string materialName);
        List<Part>? GeData();
        bool WriteDataOnFile(List<Part>? sampleData, string modifiedSampleDataPath);
        string GetFilePathToSavedData(string fileName);
        List<Part>? GetListOfPartsFromFile(string? pathToData);
    }
}
