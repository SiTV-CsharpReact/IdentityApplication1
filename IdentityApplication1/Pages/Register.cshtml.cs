using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IdentityApplication1.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityApplication1.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;// tao phuong thuc usermanager

        private readonly SignInManager<IdentityUser> signInManager; //tao phuong thuc signinManager
    
        [BindProperty] // ghi nho tk
        public Register Model { get; set; }
        public RegisterModel(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;  //gan userManager
            this.signInManager = signInManager;//gan SignInManager

        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser()//khoi tao doi tuong user 
                {
                    UserName = Model.Email,
                    Email = Model.Email
                };
                var result= await userManager.CreateAsync(user, Model.Password);
                if(result.Succeeded)
                {
                     
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach(var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
