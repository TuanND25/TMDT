namespace TMDT.ViewModels
{
	public class SignUpUser
	{
		public string UserName { get; set; }
		public string UserEmail { get; set; } 
		public string UserPassword { get; set; }
		public string UserRole { get; set; } 
		public string FullName { get; set; } 
		public bool? Gender { get; set; }
		public string? AddressInfo { get; set; }
		public string? PhoneNum { get; set; }
	}
}
