using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;  
namespace ProjectUI.ViewModels
{
    public class Account
    {
       [Required(ErrorMessage="*")]
        [Display(Name ="Kullanıcı Adı")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Birim")]
        public int UnitId { get; set; }
        public string LDAPName { get; set; }
        public string ReturnUrl { get; set; }
    }
}