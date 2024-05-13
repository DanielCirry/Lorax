using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTestMid.Models
{
    public class Meta
    {
        public PartClassification PartClassification { get; set; } = new PartClassification();
        public PartMasterType MasterType { get; set; } = new PartMasterType();
        public PartColour PartColour { get; set; } = new PartColour();
        public PartOpacity PartOpacity { get; set; } = new PartOpacity();
    }
}
