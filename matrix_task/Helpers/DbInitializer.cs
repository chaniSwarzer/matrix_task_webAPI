using matrix_task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace matrix_task.Helpers
{
    public static class DbInitializer
    {
        public static void Initialize(HeroDBcontext context)
        {

            if (context.Ability.Any())
            {
                return;   // DB has been seeded
            }

            ///add Abilities
            var a = new Ability[]
            {
                new Ability(){ Description = "attacker"},
                new Ability(){ Description = "defender"}
            };
            foreach (Ability item in a)
            {
                context.Ability.Add(item);
            }
            context.SaveChanges();

            //add Heroes
            var rand = new Random();
            var heroes = new Hero[10];
            for (int i = 0; i < 10; i++)
            {
                decimal Power = new decimal(rand.NextDouble());
                heroes[i] = new Hero() {
                AbilityId= i%2 + 1,
                Date = DateTime.Today,
                name = "Hero"+ i ,
                CurrentPower = Power,
                StartingPower = Power,
                Suit= "Green"
                
                };
            }

            foreach (Hero h in heroes)
            {
                context.Hero.Add(h);
            }
            context.SaveChanges();

            //add Trainer
            byte[] passwordHash, passwordSalt;
            Services.TrainerService.CreatePasswordHash("Admin12!", out passwordHash, out passwordSalt);

            var t = new Trainer {
                FirstName = "chana",
                LastName = "admin",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserName = "admin"
            };
            context.Trainer.Add(t);
            context.SaveChanges();

            //add  TrainerHero
            int index = 1;
            var th = new TrainerHero[]{
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
                new TrainerHero(){ HeroId =  index ++, TrainerId= 1 },
            };

            foreach (TrainerHero h in th)
            {
                context.TrainerHero.Add(h);
            }
            context.SaveChanges();
        }

    }
}
