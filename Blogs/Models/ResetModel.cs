namespace Blogs.Models { 
    public class ResetModel
    {
        public string Id { get; set; }
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set;}

        public string ErrorMessage { get; set;}
    }
}
