using Activity5_CRUD.Models.Entities;

namespace Activity5_CRUD.Models
{
    public class ArtViewModel
    {
        public List<Art> ArtList { get; set; } = new();
        public AddEntryViewModel AddEntry { get; set; } = new();
        public EditEntryViewModel EditEntry { get; set; } = new();
    }
}