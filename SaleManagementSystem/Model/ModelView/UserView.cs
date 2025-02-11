using SaleManagementSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagementSystem.Model.ModelView
{
    internal class UserView
    {
        public UserView() { }
        public int Id {  get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 0;
        public int Status { get; set; } = 0;

        public UserView ToUserView(Tbl_Users tbl_Users)
        {
            return new UserView
            {
                Id = tbl_Users.user_id,
                UserName = tbl_Users.username,
                Password = tbl_Users.password,
                RoleId = (int)tbl_Users.role_id,
                Status = (int)tbl_Users.status
            };
        }
        public Tbl_Users toUser(UserView userView)
        {
            return new Tbl_Users
            {
                user_id = Id,
                username = UserName,
                password = Password,
                role_id = RoleId,
                status = (int)Status
            };
        }
    }
}
