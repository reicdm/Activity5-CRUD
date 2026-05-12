namespace Activity5_CRUD.Models.Entities
{
    public class Art
    {
        public Guid Id { get; set; }
        public string ArtName { get; set; }
        public string ImageUrl { get; set; }
        public string Medium { get; set; }
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
    }
}
