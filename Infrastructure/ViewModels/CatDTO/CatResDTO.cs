namespace Infrastructure.ViewModels.CatDTO;

public class CatResDTO
{
    public int CatId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Breed { get; set; }
    public bool IsAdopted { get; set; }
    public int ShelterId { get; set; }
}