using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace FotoViagem.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			var isLoggedIn = !string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"));

			Console.WriteLine(HttpContext.Session.GetString("UserName"));
			ViewBag.IsLoggedIn = isLoggedIn;
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
			HttpContext.Session.Remove("UserName"); // Remove o usuário da sessão ao fazer logout
			return RedirectToAction("Index", "Home");
		}
	}
}
