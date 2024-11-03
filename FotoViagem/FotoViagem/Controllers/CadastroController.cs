using FotoViagem.DAO;
using FotoViagem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FotoViagem.Controllers
{
	public class CadastroController : Controller
	{
		private CadastroDAO cadastroDAO;

		public IActionResult index(char operacao)
		{
			ViewBag.operacao = operacao;
			return View("index");
		}
		public IActionResult Login(CadastroViewModel model)
		{

			return RedirectToAction("index", "home");
		}
		public IActionResult Cadastro(CadastroViewModel model, string operacao)
		{
			try
			{
				ValidarDados(model,operacao);
				if (ModelState.IsValid)
				{
                    cadastroDAO.Insert(model);

                    return RedirectToAction("index", "home");
                }
				else
				{
                    ViewBag.operacao = operacao;

                    return View("index", model);
				}

            }
            catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));
			}


		}

		private void ValidarDados(CadastroViewModel model, string operacao)
		{
			cadastroDAO = new CadastroDAO();
            ModelState.Clear();

			if (operacao == "C")
			{
				if (!cadastroDAO.VerificarLogin(model.loginUsuario))
				{
                    ModelState.AddModelError("loginUsuario", "Login de Usuario já existe vazio");

                }
            }

        }
    }
}
