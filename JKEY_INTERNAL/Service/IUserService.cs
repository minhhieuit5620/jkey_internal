//using JKEY_INTERNAL.Models;

//namespace JKEY_INTERNAL.Service
//{
//    public interface IUserService
//    {
//        IEnumerable<User> GetUsers(int pageNumber, int pageSize);
//        int GetTotalUsers();
//        IEnumerable<User> SearchUsers(string? username, string? email, string? phone, string? fullname);

//    }

//    public class UserService : IUserService
//    {
//        private readonly JkeyInternalContext _Context;

//        public UserService(JkeyInternalContext Context)
//        {
//            _Context = Context;
//        }

//        public IEnumerable<User> GetUsers(int pageNumber, int pageSize)
//        {
//            var users = _Context.Users
//                .Skip((pageNumber - 1) * pageSize)
//                .Take(pageSize)
//                .ToList();

//            return users;
//        }

//        public int GetTotalUsers()
//        {
//            return _Context.Users.Count();
//        }

//        public IEnumerable<User> SearchUsers(string? username, string? email, string? phone, string? fullname)
//        {
//            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(email) &&
//           string.IsNullOrWhiteSpace(phone) && string.IsNullOrWhiteSpace(fullname))
//            {
//                return User.GetUsers();
//            }

//            // Thực hiện tìm kiếm người dùng dựa trên các thông tin truyền vào
//            return _userRepository.GetUsers().Where(u =>
//                (string.IsNullOrWhiteSpace(username) || u.Username.Contains(username)) &&
//                (string.IsNullOrWhiteSpace(email) || u.Email.Contains(email)) &&
//                (string.IsNullOrWhiteSpace(phone) || u.Phone.Contains(phone)) &&
//                (string.IsNullOrWhiteSpace(fullname) || u.Fullname.Contains(fullname)));
//        }
//    }
//    }

//}
