using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public class PresenceSettingsDTO
    {
        public int PresenceId { get; set; }
        public int? UserId { get; set; }
        public int Day { get; set; }
        public int Hours { get; set; }
    }

    public class AmountSettingsDTO
    {
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        public int Day { get; set; }
        public string Product { get; set; }
        public string ProductType { get; set; }
        public int ProductValue { get; set; }
        public int ProductQuantity { get; set; }
    }
}
