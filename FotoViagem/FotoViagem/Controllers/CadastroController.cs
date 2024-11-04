using FotosViagem.DAO;
using FotosViagem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FotosViagem.Controllers
{
	public class CadastroController : Controller
	{
		private CadastroDAO cadastroDAO = new CadastroDAO();

		public IActionResult index(char operacao)
		{
			ViewBag.operacao = operacao;
			return View("index");
		}

		public IActionResult Cadastro(CadastroViewModel model, string operacao)
		{
			try
			{
				ValidarDados(model,operacao);
				if (ModelState.IsValid)
				{
                    cadastroDAO.Insert(model);
					HttpContext.Session.SetString("Logado", "true");
					ViewBag.UsuarioID = cadastroDAO.BuscaId(model.loginUsuario, model.senha);
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
        public IActionResult Login(CadastroViewModel model, string operacao)
        {
            try
            {
                ValidarDados(model, operacao);
                if (ModelState.IsValid)
                {
					HttpContext.Session.SetString("Logado", "true");
					ViewBag.UsuarioID = cadastroDAO.BuscaId(model.loginUsuario, model.senha);
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
            ModelState.Clear();

			if (operacao == "C")
			{
				if (!cadastroDAO.VerificarLogin(model.loginUsuario))
				{
                    ModelState.AddModelError("loginUsuario", "Login de Usuario já existe vazio");

                }
            }
            if(operacao == "L")
            {
                if (cadastroDAO.Login(model.loginUsuario, model.senha))
                    ModelState.AddModelError("loginUsuario", "Login ou senha incorreto");
            }

        }
    }
}
