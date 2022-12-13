using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.SysEntities
{
    public class Cost : INotifyPropertyChanged, IEquatable<Cost>
    {
        int availabilityId, costValue;

        public Cost(int availabilityId, int costValue)
        {
            AvailabilityId = availabilityId;
            CostValue = costValue;
        }

        public int AvailabilityId { get => availabilityId; set { availabilityId = value; OnPropertyChanged("AvailabilityId"); } }
        public int CostValue { get => costValue; set { costValue = value; OnPropertyChanged("CostValue"); } }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Equals(Cost? other)
        {
            return other is not null && AvailabilityId == other.AvailabilityId && CostValue == other.CostValue;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
