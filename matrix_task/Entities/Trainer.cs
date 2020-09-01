using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Entities
{
    public class Trainer
    {
        public Trainer()
        {
            TrainerHeroes = new HashSet<TrainerHero>();
        }
        public int TrainerId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<TrainerHero> TrainerHeroes { get; set; }
    }
}
