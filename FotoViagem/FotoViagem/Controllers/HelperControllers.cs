namespace FotoViagem.Controllers
{
	public class HelperControllers
	{
		public static Boolean verificaUserLogado(ISession session)
		{
			string logado = session.GetString("Logado");
			if (logado == null)
				return false;
			else
				return false;


		}
	}
}
