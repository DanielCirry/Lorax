using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTestMid.Models
{
    public class Part
    {
        public int PartId { get; set; }
        public string PartNbr { get; set; } = string.Empty;
        public string PartDesc { get; set; } = string.Empty;
        public Meta Meta { get; set; } = new Meta();
        public PartWeight PartWeight { get; set; } = new PartWeight();
        public bool ConversionsApplied { get; set; }
        public List<MaterialDetails> Materials { get; set; } = new List<MaterialDetails>();
    }
}
