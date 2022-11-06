using DBLib.SysEntities;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    [JsonSerializable(typeof(SaleReceiptPositionDTO))]
    public class SaleReceiptPositionDTO : EntityObjectDTO<SaleReceiptPosition>
    {
        [JsonConstructor]
        public SaleReceiptPositionDTO(int availabilityid, int salereceiptid)
        {
            AvailabilityId = availabilityid;
            SaleReceiptId = salereceiptid;
        }

        public SaleReceiptPositionDTO(SaleReceiptPosition saleReceiptPosition)
        {
            Id = saleReceiptPosition.Id;
            AvailabilityId = saleReceiptPosition.AvailabilityId;
            SaleReceiptId = saleReceiptPosition.SaleReceiptId;
        }

        public int Id { get; set; }
        public int AvailabilityId { get; set; }
        public int SaleReceiptId { get; set; }

        public override SaleReceiptPosition GetEntity() => new SaleReceiptPosition(Id, AvailabilityId, SaleReceiptId);
    }
}
