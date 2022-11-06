using DBLib.SysEntities;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    [JsonSerializable(typeof(SaleReceiptWithSumCostDTO))]
    public class SaleReceiptWithSumCostDTO : SaleReceiptDTO
    {
        [JsonConstructor]
        public SaleReceiptWithSumCostDTO(string fio, DateTime dateOfPurchase, int shopId) : base(fio, dateOfPurchase, shopId)
        {
        }

        public SaleReceiptWithSumCostDTO(SaleReceipt saleReceipt) : base(saleReceipt)
        { }

        public int SummaryCost { get; set; }
    }
}
