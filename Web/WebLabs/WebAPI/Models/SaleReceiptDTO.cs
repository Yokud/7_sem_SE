using DBLib.SysEntities;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    [JsonSerializable(typeof(SaleReceiptDTO))]
    public class SaleReceiptDTO : EntityObjectDTO<SaleReceipt>
    {
        [JsonConstructor]
        public SaleReceiptDTO(string fio, DateTime dateOfPurchase, int shopId)
        {
            Fio = fio;
            DateOfPurchase = dateOfPurchase;
            ShopId = shopId;
        }

        public SaleReceiptDTO(SaleReceipt saleReceipt)
        {
            Id = saleReceipt.Id;
            Fio = saleReceipt.Fio;
            DateOfPurchase = saleReceipt.DateOfPurchase.ToDateTime(TimeOnly.MinValue);
            ShopId = saleReceipt.ShopId;
        }

        public int Id { get; set; }
        public string Fio { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int ShopId { get; set; }

        public override SaleReceipt GetEntity() => new SaleReceipt(Id, Fio, new DateOnly(DateOfPurchase.Year, DateOfPurchase.Month, DateOfPurchase.Day), ShopId);
    }
}
