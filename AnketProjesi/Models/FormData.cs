namespace AnketProjesi.Models
{
    public class FormData
    {
        public string Tip { get; set; }
        public string Meslek { get; set; }
        public List<QuestionAnswer> Sorular { get; set; }
    }
}
