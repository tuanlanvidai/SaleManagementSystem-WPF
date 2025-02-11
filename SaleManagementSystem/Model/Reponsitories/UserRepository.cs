using SaleManagementSystem.Model.Entities;
using SaleManagementSystem.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementSystem.Model.Reponsitories
{
    internal sealed class UserRepository
    {
        private static UserRepository _instance = null;
        private UserRepository() { }
        public static UserRepository Instance{
            get { 
                if (_instance == null)
                {
                    _instance = new UserRepository();
                }
                return _instance;
            }    
        }
        public List<UserView> GetUser(string username)
        {
            try
            {
                DbEntities en = new DbEntities();
                var rs = en.Tbl_Users.Where(d => d.username == username && d.status!=0)
                    .Select(d => new UserView
                    {
                        Id = d.user_id,
                        UserName = d.username,
                        RoleId = (int)d.role_id,
                        Password = d.password,
                        Status = (int)d.status
                    }).ToList();
                return rs;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return new List<UserView>();
        }
    }
}
