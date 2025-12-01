namespace HRMApp.Model
{
    public class LoginResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public LoginData Data { get; set; }  // lồng "data"
    }

    public class LoginData
    {
        public string Access_Token { get; set; }     // JSON: access_token
        public string Refresh_Token { get; set; }    // JSON: refresh_token
        public string Expires_At { get; set; }       // JSON: expires_at
        public UserInfo User { get; set; }           // JSON: user
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string Employee_Id { get; set; }
        public string Username { get; set; }
        public string Role_Id { get; set; }
        public string Status { get; set; }
        public bool Is_First_Login { get; set; }
        public string Last_Login_At { get; set; }
        public EmployeeInfo Employee { get; set; }
        public RoleInfo Role { get; set; }
    }

    public class EmployeeInfo
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Hire_Date { get; set; }
        public string Avatar_Url { get; set; }
        public string Department_name { get; set; }
        public string Position_name { get; set; }
    }

    public class RoleInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
