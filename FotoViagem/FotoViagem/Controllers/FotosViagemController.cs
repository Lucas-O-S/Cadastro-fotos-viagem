using FotosViagem.DAO;
using FotosViagem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FotosViagem.Controllers
{
	public class FotosViagemController : PadraoController<FotosViagemViewModel>
	{
        private byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }
        public List<CadastroViewModel> buscarCadastro()
        {
            CadastroDAO cDAO = new CadastroDAO();

            List<CadastroViewModel> cadastro = cDAO.Listagem();

            return cadastro;
        }
        public FotosViagemController() { dao = new FotosViagemDAO(); }
        public IActionResult SaveFoto(FotosViagemViewModel model, string operacao, IFormFile[] fotos)
        {
            try
            {
                for(int i = 0;i < 3; i++)
                {
                    model.fotos[i] = fotos[i];
                }

                
                foreach(var item in model.fotos)
                {
                    model.fotosByte.Add(ConvertImageToByte(item));
                }

                ValidarDados(model, operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.operacao = operacao;
                    return View(NomeViewForm, model);
                }
                else
                {
                    if (operacao == "I")
                        dao.Insert(model);


                    else
                        dao.Update(model);
                    return RedirecionaParaIndex(model);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }



        public override IActionResult Create()
        {
            try
            {
                ViewBag.operacao = "I";
                ViewBag.Usuarios = buscarCadastro();
                FotosViagemViewModel model = new FotosViagemViewModel();
                return View(NomeViewForm, model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

    }
}
