using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BankingSystem.WebPortal.Managers;
using BankingSystem.WebPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BankingSystem.WebPortal.Controllers
{
    /// <summary>
    ///     Represents a controller responsible for user authentication.
    /// </summary>
    [RequireHttps]
    public class AuthController : Controller
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        private CustomerSignInManager _signInManager;
        private CustomerManager _userManager;
        private IAuthenticationManager _authenticationManager;

        /// <summary>
        ///     Gets the sign in manager.
        /// </summary>
        /// <value>
        ///     The sign in manager.
        /// </value>
        public CustomerSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<CustomerSignInManager>(); }
            internal set { _signInManager = value; }
        }

        /// <summary>
        ///     Gets the user manager.
        /// </summary>
        /// <value>
        ///     The user manager.
        /// </value>
        public CustomerManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().Get<CustomerManager>(); }
            internal set { _userManager = value; }
        }

        /// <summary>
        ///     Gets the authentication manager.
        /// </summary>
        /// <value>
        ///     The authentication manager.
        /// </value>
        public IAuthenticationManager AuthenticationManager
        {
            get { return _authenticationManager ?? HttpContext.GetOwinContext().Authentication; }
            internal set { _authenticationManager = value; }
        }

        /// <summary>
        ///     Returns the Login view.
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        ///     Logs in the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        /// <summary>
        ///     Logs off the user.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     Logs in with an externals login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", new {ReturnUrl = returnUrl}));
        }

        /// <summary>
        ///     Logs in with an externals login.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                default:
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginCannotBeVerified");
            }
        }

        /// <summary>
        ///     Returns a view which allows to manage user logins.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> Manage()
        {
            var userId = User.Identity.GetUserId<int>();
            var model = new ManageAccountViewModel
            {
                HasPassword = HasPassword(),
                Logins = await UserManager.GetLoginsAsync(userId),
            };
            return View(model);
        }

        /// <summary>
        ///     Manages the logins.
        /// </summary>
        /// <param name="message">The message.</param>
        [Authorize]
        public async Task<ActionResult> ManageLogins(string message)
        {
            ViewBag.StatusMessage = message;
            var userId = User.Identity.GetUserId<int>();
            var user = await UserManager.FindByIdAsync(userId);
            var userLogins = await UserManager.GetLoginsAsync(userId);
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(
                new ManageLoginsViewModel
                {
                    CurrentLogins = userLogins,
                    OtherLogins = otherLogins
                });
        }

        /// <summary>
        ///     Removes the user's login.
        /// </summary>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="providerKey">The provider key.</param>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            var userId = User.Identity.GetUserId<int>();
            var result = await UserManager.RemoveLoginAsync(userId, new UserLoginInfo(loginProvider, providerKey));
            string message;
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = "Login has been successfully removed.";
            }
            else
            {
                message = "An error has occurred.";
            }
            return RedirectToAction("ManageLogins", new {Message = message});
        }

        /// <summary>
        ///     Links the user's login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback"), User.Identity.GetUserId());
        }

        /// <summary>
        ///     Links the user's login.
        /// </summary>
        [Authorize]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new {Message = "An error has occurred"});
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId<int>(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new {Message = "An error has occurred"});
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId<int>());
            return user?.PasswordHash != null;
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}