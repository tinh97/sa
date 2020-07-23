using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User GetById(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetListPagging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if(!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
                
            }
            //model = model.OrderByDescending(x => x.CreatedDate);
            return model.ToPagedList(page, pageSize);
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;// entity.PassWord <=> user.PassWord là mã md5
                    //( được truyền từ phương thức Update(user) trong UserController sang)
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                //user.ModifiedBy = entity.ModifiedBy;
                //user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
           catch(Exception ex)
            {
                return false;
            }

        }
        public bool Deleteuser(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x=>x.UserName==userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if(result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if(result.Password == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }

    }   
}
