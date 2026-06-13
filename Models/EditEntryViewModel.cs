namespace Activity5_CRUD.Models
{
    public class EditEntryViewModel
    {
        public Guid Id { get; set; }
        public string ArtName { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string Medium { get; set; }
        public DateOnly Date { get; set; }
        public string? ExistingImageUrl { get; set; }
    }
}