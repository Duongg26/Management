namespace Management.Models.Entities
{
    public class Tep
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}
