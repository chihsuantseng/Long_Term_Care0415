using System.ComponentModel.DataAnnotations;

namespace Long_Term_Care.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }
        [Required(ErrorMessage = "請輸入電子信箱")]
        [EmailAddress(ErrorMessage = "請輸入有效的電子郵件地址")] 
        public string Email { get; set; }
    }
}
