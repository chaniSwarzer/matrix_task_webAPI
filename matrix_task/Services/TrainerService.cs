using matrix_task.Entities;
using matrix_task.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Services
{
    public interface ITrainerService
    {
        Trainer Authenticate(string username, string password);
        Trainer GetById(int id);
        Trainer Create(Trainer user, string password);
        IEnumerable<Hero> GetHerosByTrainer(int TrainerId);

    }
    public class TrainerService : ITrainerService
    {
        private HeroDBcontext _context;

        public TrainerService(HeroDBcontext context)
        {
            _context = context;
        }

        public Trainer Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var trainer = _context.Trainer.SingleOrDefault(x => x.UserName == username);

            // check if username exists
            if (trainer == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, trainer.PasswordHash, trainer.PasswordSalt))
                return null;

            // authentication successful
            return trainer;
        }

    

        public Trainer GetById(int id)
        {
            return _context.Trainer.Find(id);
        }

        public Trainer Create(Trainer trainer, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Trainer.Any(x => x.UserName == trainer.UserName))
                throw new AppException("Username \"" + trainer.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            trainer.PasswordHash = passwordHash;
            trainer.PasswordSalt = passwordSalt;

            _context.Trainer.Add(trainer);
            _context.SaveChanges();

            return trainer;
        }

        public void Update(Trainer trainerParam, string password = null)
        {
            var trainer = _context.Trainer.Find(trainerParam.TrainerId);

            if (trainer == null)
                throw new Exception("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(trainerParam.UserName) && trainerParam.UserName != trainer.UserName)
            {
                // throw error if the new username is already taken
                if (_context.Trainer.Any(x => x.UserName == trainerParam.UserName))
                    throw new Exception("Username " + trainerParam.UserName + " is already taken");

                trainer.UserName = trainerParam.UserName;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(trainerParam.FirstName))
                trainer.FirstName = trainerParam.FirstName;

            if (!string.IsNullOrWhiteSpace(trainerParam.LastName))
                trainer.LastName = trainerParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                trainer.PasswordHash = passwordHash;
                trainer.PasswordSalt = passwordSalt;
            }

            _context.Trainer.Update(trainer);
            _context.SaveChanges();
        }


        // helper methods

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public IEnumerable<Hero> GetHerosByTrainer(int TrainerId)
        {
            return _context.Hero.Where(h => h.TrainerHeroes.Any(ht => ht.TrainerId == TrainerId)).Include(h => h.Ability);
        }
    }
}
