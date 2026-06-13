namespace Activity5_CRUD.Models
{
    public class AddEntryViewModel
    {
        public string ArtName { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Medium { get; set; }
        public DateOnly Date { get; set; }
    }
}
