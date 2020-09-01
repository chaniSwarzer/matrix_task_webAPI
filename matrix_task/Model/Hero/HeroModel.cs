using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Model.Hero
{
    public class HeroModel
    {
        public int HeroId { get; set; }
        public string name { get; set; }
        public DateTime Date { get; set; }
        public string Suit { get; set; }
        public decimal StartingPower { get; set; }
        public decimal CurrentPower { get; set; }
        public string AbilityDescription { get; set; }

    }
}
