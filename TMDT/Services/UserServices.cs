using TMDT.Models;

namespace TMDT.Services
{
	public class UserServices
	{
		private db_ECommerceShopContext _context;
		public UserServices(db_ECommerceShopContext context)
		{
			_context = context;	
		}
		public bool AddUser(User user)
		{
			try
			{
				_context.Users.Add(user);
				_context.SaveChanges();
				return true;
			}
			catch (Exception )
			{
				return false;
			}
			
		}
		public bool UpdateUser(User user) 
		{
			try
			{
				var UserUpdate = _context.Users.FirstOrDefault(p=>p.UserId==user.UserId);	
				if (UserUpdate != null)
				{
					UserUpdate.UserEmail = user.UserEmail;
					UserUpdate.UserName= user.UserName;
					UserUpdate.UserRole = user.UserRole;
					UserUpdate.FullName = user.FullName;
					UserUpdate.UserPassword = user.UserPassword;
					UserUpdate.AddressInfo= user.AddressInfo;
					UserUpdate.PhoneNum= user.PhoneNum;
					_context.SaveChanges();
				}

			}catch(Exception )
			{
				return false;
			}
		}
	}
}
