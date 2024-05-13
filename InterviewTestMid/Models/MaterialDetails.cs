namespace InterviewTestMid.Models
{
    public class MaterialDetails
    {
        public Material Material { get; set; } = new Material();
        public decimal Percentage { get; set; }
        public bool MatrIsBarrier { get; set; }
        public bool MatrIsDensier { get; set; }
        public bool MatrIsOppacifier { get; set; }
    }
}