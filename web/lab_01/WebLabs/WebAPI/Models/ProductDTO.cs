using DBLib.SysEntities;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace WebAPI.Models
{
    [JsonSerializable(typeof(ProductDTO))]
    public class ProductDTO
    {
        [JsonConstructor]
        public ProductDTO(int id, string name, string productType)
        {
            Id = id;
            Name = name;
            ProductType = productType;
        }

        public ProductDTO(string name, string productType)
        {
            Name = name;
            ProductType = productType;
        }

        public ProductDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            ProductType = product.ProductType;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public int? Cost { get; set; }

        public Product GetEntity() => new Product(Id, Name, ProductType);
    }
}
