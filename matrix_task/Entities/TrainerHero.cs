using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Entities
{
    public class TrainerHero
    {
        public int TrainerHeroId { get; set; }
        public int HeroId { get; set; }
        public int TrainerId { get; set; }

        public virtual Hero Hero { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
