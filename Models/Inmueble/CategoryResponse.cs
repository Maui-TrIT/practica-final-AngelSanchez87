using Newtonsoft.Json;

namespace ShopApp.Models.Inmueble;

public class CategoryResponse
{
    public int Id { get; set; }

    [JsonProperty("nombre")]
    public string NombreCategory { get; set; }

    public string ImageUrl { get; set; }
}
