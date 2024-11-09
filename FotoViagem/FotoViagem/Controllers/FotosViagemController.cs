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
        public IActionResult SaveFoto(FotosViagemViewModel model, string operacao)
        {
            try
            {
                model.fotos[0] = model.foto0;
                model.fotos[1] = model.foto1;
                model.fotos[2] = model.foto2;
                ValidarDados(model, operacao);

                if (ModelState.IsValid == false)
                {
                    ViewBag.operacao = operacao;
                    ViewBag.Usuarios = buscarCadastro();

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

        public override IActionResult Edit(int id)
        {
            try
            {
                ViewBag.operacao = "A";
                ViewBag.Usuarios = buscarCadastro();
                var model = dao.Consulta(id);
                if (model == null)
                    return RedirecionaParaIndex(model);
                else
                    return View(NomeViewForm, model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }


        }

        protected override void ValidarDados(FotosViagemViewModel model, string operacao)
        {

            base.ValidarDados(model, operacao);
            if (model.fotos.Length == 0)
                ModelState.AddModelError("Fotos", "Escolha uma imagem.");


            foreach (var fotos in model.fotos)
            {
                if (fotos != null && fotos.Length / 1024 / 1024 >= 2)
                    ModelState.AddModelError("fotos", "Imagem limitada a 2 mb.");
            }

            if (ModelState.IsValid)
            {

                if (operacao == "A")
                {
                    FotosViagemViewModel temp = dao.Consulta(model.id);
                    if (model.fotos.Length == 0)
                        model.fotosByte = temp.fotosByte;
                    else
                    {
                        for (int i = 0; i < 3; i++) { 

                            if (model.fotos[i] != null)
                            {
                                model.fotosByte.Add(ConvertImageToByte(model.fotos[i]));
                            }


                            else if (i < temp.fotosByte.Count)
                                model.fotosByte.Add(temp.fotosByte[i]);

                        }
                    }
                }
                else
                {
                    foreach(var fotos in model.fotos)
                        model.fotosByte.Add(ConvertImageToByte(fotos));

                }

            }


        }

    }
}
