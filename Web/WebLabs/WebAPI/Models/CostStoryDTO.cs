using DBLib.SysEntities;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    [JsonSerializable(typeof(CostStoryDTO))]
    public class CostStoryDTO : EntityObjectDTO<CostStory>
    {
        [JsonConstructor]
        public CostStoryDTO(int year, int month, int cost, int availabilityid)
        {
            Year = year;
            Month = month;
            Cost = cost;
            AvailabilityId = availabilityid;
        }

        public CostStoryDTO(CostStory costStory)
        {
            Id = costStory.Id;
            Year = costStory.Year;
            Month = costStory.Month;
            Cost = costStory.Cost;
            AvailabilityId = costStory.AvailabilityId;
        }

        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Cost { get; set; }
        public int AvailabilityId { get; set; }

        public override CostStory GetEntity() => new CostStory(Id, Year, Month, Cost, AvailabilityId);
    }
}
