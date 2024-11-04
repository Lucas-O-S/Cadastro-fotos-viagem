using FotosViagem.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace FotosViagem.DAO
{
	public class CadastroDAO : PadraoDAO<CadastroViewModel>
	{
		protected override void SetTabela()
		{
			tabela = "Cadastro";
		}
		protected override CadastroViewModel MontarModel(DataRow registro)
		{
			CadastroViewModel model = new CadastroViewModel()
			{
				id = Convert.ToInt32(registro["ID"]),
				nome = Convert.ToString(registro["nome"]),
				loginUsuario = Convert.ToString(registro["senha"]),
				senha = Convert.ToString(registro["Senha"])

			};
			return model;
		}
		protected override SqlParameter[] CriarParametros(CadastroViewModel model)
		{

			SqlParameter[] sp;
			if (model.id == 0 || model.id == null)
			{
				sp = new SqlParameter[]
				{
					new SqlParameter("nome",model.nome),
					new SqlParameter("loginUsuario",model.loginUsuario),
					new SqlParameter("senha",model.senha)

				};

			}
			else
			{
				sp = new SqlParameter[]
				{
					new SqlParameter("id",model.id),
					new SqlParameter("nome",model.nome),
					new SqlParameter("loginUsuario",model.loginUsuario),
					new SqlParameter("senha",model.senha)

				};

			}
			return sp;

		}

		public bool VerificarLogin(string login)
		{
			SqlParameter[] sp = new SqlParameter[] {
				new SqlParameter("loginUsuario",login)
			};
			DataTable dt = HelperDAO.ExecutaProcSelect("sp_verificarLogin_Cadastro", sp);

            return dt.Rows.Count == 0;
		}

		public bool Login(string login, string senha)
		{
			SqlParameter[] sp = new SqlParameter[] {
				new SqlParameter("loginUsuario",login),
				new SqlParameter("senha",senha)

			};
			DataTable dt = HelperDAO.ExecutaProcSelect("sp_Fazerlogin_Cadastro", sp);

			return dt.Rows.Count == 0;
		}

		public int BuscaId(string login, string senha)
		{
			SqlParameter[] sp = new SqlParameter[] {
				new SqlParameter("loginUsuario",login),
				new SqlParameter("senha",senha)

			};
			DataTable dt = HelperDAO.ExecutaProcSelect("sp_Fazerlogin_Cadastro", sp);
			if (dt.Rows.Count != 0)
			{
				int id = Convert.ToInt32(dt.Rows[0]["ID"]);
				return id;
			}
			return 0;
		}
	}
}
