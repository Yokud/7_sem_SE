using DBLib.SysEntities;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class ProductWithCostDTO : EntityObjectDTO<Product>
    {
        [JsonConstructor]
        public ProductWithCostDTO(string name, string productType, int cost)
        {
            Name = name;
            ProductType = productType;
            Cost = cost;
        }

        public ProductWithCostDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            ProductType = product.ProductType;
            Cost = product.Cost.HasValue ? product.Cost.Value : 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public int Cost { get; set; }

        public override Product GetEntity() => new Product(Id, Name, ProductType, Cost);
    }
}
