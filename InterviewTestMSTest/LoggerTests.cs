using InterviewTestMid.Interfaces;
using InterviewTestMid.Services;
using InterviewTestMid.Models;
using Moq;
using InterviewTestMid.Repositories;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewTestMSTest
{
    public class Tests
    {
        private Logger _logger;
        private IPartRepository _partRepository;

        [SetUp]
        public void Setup()
        {
            _partRepository = new PartRepository();
            _logger = new Logger(_partRepository);
        }

        [Test]
        public void ModifyPartWeight_GivesValue_ReturnsNameOfFile()
        { 
            // Act
            var result = _logger.ModifyPartWeightValue("BLUE TRAY", 2.00000M, "ModifiedSampleData.json");
            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(!string.IsNullOrWhiteSpace(result));
        }


        [Test]
        public void ModifyPartWeight_GivesValue_ReturnsModifiedFile()
        {
            // Arrange
            var originalData = _logger.GetData();
            Part? originalPart = originalData?.Find(p => p.PartDesc == "BLUE TRAY");

            // Act
            var nameOfFile = _logger.ModifyPartWeightValue("BLUE TRAY", 2.00000M, "ModifiedSampleData.json");
            var pathToData = _logger.GetFilePathToSavedData(nameOfFile);
            List<Part>? data = _partRepository.GetListOfPartsFromFile(pathToData); ;
            Part? part = data?.Find(p => p.PartDesc == "BLUE TRAY");

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(originalPart?.PartWeight.Value != 2.00000M);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(part?.PartWeight.Value == 2.00000M);
        }

        [Test]
        [ExpectedException(typeof(IOException))]

        public void GetMaterialDetails_GivesString_ReturnsListOfMaterials()
        { 
            // Assert
            NUnit.Framework.Assert.Throws<Exception>(() => _logger.ModifyPartWeightValue("BLUE TRAY", 2.00000M, $"{DateTime.Now}: ModifiedSampleData.json"));
        }
    }
}