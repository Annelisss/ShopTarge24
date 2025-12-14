using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models;


namespace ShopTARge24.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailServices _emailServices;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailServices emailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailServices = emailServices;
        }

        // ----------------- REGISTER GET -----------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();   // otsib vaadet Views/Accounts/Register.cshtml
        }

        // ----------------- REGISTER POST -----------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                City = vm.City          
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(vm);
            }

            // GENEREERIME EMAILI KINNITUSE TOKENI
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.Action(
                "ConfirmEmail",
                "Accounts",
                new { userId = user.Id, token = token },
                Request.Scheme);

            // e-kirja DTO
            EmailTokenDto newsignup = new();
            newsignup.Token = token;
            newsignup.Body =
                $"Please registrate your account by: <a href=\"{confirmationLink}\">clicking here</a>";
            newsignup.Subject = "CRUD registration";
            newsignup.To = user.Email;

            // kui admin loob kasutaja, suunatakse ta nimekirja tagasi
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {
                return RedirectToAction("ListUsers", "Administrations");
            }

            // saadame emaili
            _emailServices.SendEmailToken(newsignup, token);

            // LOGI
            List<string> errordatas = new()
            {
                "Area", "Accounts",
                "Issue", "Success",
                "StatusMessage", "Registration Success",
                "ActedOn", $"{vm.Email}",
                "CreatedAccountData",
                $"{vm.Email}\n{vm.City}\n[password hidden]\n[password hidden]"
            };

            ViewBag.ErrorDatas = errordatas;
            ViewBag.ErrorTitle = "You have successfully registered";
            ViewBag.ErrorMessage =
                "Before you can log in, please confirm email from the link " +
                "\nwe have emailed to your email address.";

           
            return View("ConfirmationEmailMessage");
        }

        // ----------------- CONFIRM EMAIL -----------------
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {

                return RedirectToAction("Index", "Home");
            }

            return View("Error");
        }
    }
}
