using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Entities
{
    public class Hero
    {
        public Hero()
        {
            TrainerHeroes = new HashSet<TrainerHero>();
        }
        public int HeroId { get; set; }
        public string name { get; set; }
        public DateTime Date { get; set; }
        public string Suit { get; set; }
        [Column(TypeName = "decimal(18,4)")]

        public decimal StartingPower { get; set; }
        [Column(TypeName = "decimal(18,4)")]

        public decimal CurrentPower { get; set; }
        public int AbilityId { get; set; }

        public virtual ICollection<TrainerHero> TrainerHeroes { get; set; }
        public virtual Ability Ability { get; set; }


    }
}
