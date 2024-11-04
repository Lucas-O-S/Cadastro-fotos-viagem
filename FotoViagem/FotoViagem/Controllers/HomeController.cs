using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;



namespace FotosViagem.Controllers
{
	public class HomeController : Controller
	{
		bool ExiteAutenticao = true;
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (ExiteAutenticao && !HelperControllers.verificaUserLogado(HttpContext.Session))
			{
				ViewBag.Logado = false;
			}
			else
			{
				ViewBag.Logado = true;
			}
			base.OnActionExecuting(context);

		}
		public IActionResult Index()
		{

			return View();
		}
		public IActionResult Login(string username, string password)
		{
			// Lógica de autenticação (verificação do usuário e senha)

			// Se o login for bem-sucedido:
			if(username != null)
				HttpContext.Session.SetString("UserName", username);
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear(); // Remove o usuário da sessão ao fazer logout
			return RedirectToAction("Index", "Home");
		}


	}
}
