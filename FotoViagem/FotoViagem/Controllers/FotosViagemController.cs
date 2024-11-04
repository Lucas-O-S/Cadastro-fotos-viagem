using FotosViagem.DAO;
using FotosViagem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FotosViagem.Controllers
{
	public class FotosViagemController : PadraoController<FotosViagemViewModel>
	{
		public FotosViagemController() { dao = new FotosViagemDAO(); }


	}
}
