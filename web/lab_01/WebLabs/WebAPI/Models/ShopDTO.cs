using DBLib.SysEntities;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace WebAPI.Models
{
    [JsonSerializable(typeof(ShopDTO))]
    public class ShopDTO
    {
        [JsonConstructor]
        public ShopDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public ShopDTO(Shop shop)
        {
            Id = shop.Id;
            Name = shop.Name;
            Description = shop.Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Shop GetEntity() => new Shop(Id, Name, Description);
    }
}
