using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Entities
{
    public class Ability
    {
        public Ability()
        {
            Heroes = new HashSet<Hero>();
        }
        public int AbilityId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Hero> Heroes { get; set; }

    }
}
