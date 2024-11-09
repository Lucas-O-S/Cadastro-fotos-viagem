using FotosViagem.DAO;
using FotosViagem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace FotosViagem.Controllers
{
    public abstract class PadraoController<T> : Controller where T : PadraoViewModel
    {
        protected bool ExiteAutenticao { get; set; } = true;
        protected PadraoDAO<T> dao { get; set; }
        protected string NomeViewIndex { get; set; } = "index";
        protected string NomeViewForm { get; set; } = "form";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(ExiteAutenticao && !HelperControllers.verificaUserLogado(HttpContext.Session))
            {
                ViewBag.operacao = "L";
                context.Result = RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }
        }

        protected virtual IActionResult RedirecionaParaIndex(T model)
        {
            return RedirectToAction(NomeViewIndex);
        }

        public virtual IActionResult Index()
        {
            try
            {
                var model = dao.Listagem();
                return View(model);

            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Create()
        {
            try
            {
                ViewBag.operacao = "I";
                T model = Activator.CreateInstance(typeof(T)) as T;
                return View(NomeViewForm, model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public virtual IActionResult Save(T model, string operacao)
        {
            try
            {
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

        protected virtual void ValidarDados(T model, string operacao) { ModelState.Clear(); }

        public virtual IActionResult Edit(int id)
        {
            try
            {
                ViewBag.operacao = "A";
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
        public IActionResult Delete(int id)
        {
            try
            {
                var model = dao.Consulta(id);
                if (model != null)
                    dao.Delete(id);

                return RedirecionaParaIndex(model);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }




    }
}
