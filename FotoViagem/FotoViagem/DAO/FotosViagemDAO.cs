using FotosViagem.Models;
using System.Data;
using System.Data.SqlClient;

namespace FotosViagem.DAO
{
	public class FotosViagemDAO : PadraoDAO<FotosViagemViewModel>
	{
		protected override void SetTabela()
		{
			tabela = "FotosViagem";
		}
		protected override SqlParameter[] CriarParametros(FotosViagemViewModel model)
		{
			List<object> imagensByte = new List<object>();
			for (int i = 0; i < 3; i++)
			{
				if (model.fotosByte.Count > i)
				{
                    imagensByte.Add(model.fotosByte[i]);

                }
				else {
					imagensByte.Add( DBNull.Value);
				}
            }

			SqlParameter[] sp;
			if (model.id == null || model.id ==0)
			{
				sp = new SqlParameter[]
				{
					new SqlParameter("dataFoto",model.dataFoto),
					new SqlParameter("localFoto",model.localFoto),
					new SqlParameter("usuario",model.usuario),
					new SqlParameter("Foto01", SqlDbType.VarBinary) { Value = imagensByte[0] ?? DBNull.Value },
                    new SqlParameter("Foto02", SqlDbType.VarBinary) { Value = imagensByte[1] ?? DBNull.Value },
					new SqlParameter("Foto03", SqlDbType.VarBinary) { Value = imagensByte[2] ?? DBNull.Value }

				};
			}
			else
			{
				sp = new SqlParameter[]
				{
					new SqlParameter("id",model.id),
					new SqlParameter("dataFoto",model.dataFoto),
					new SqlParameter("localFoto",model.localFoto),
					new SqlParameter("Foto01", SqlDbType.VarBinary) { Value = imagensByte[0] ?? DBNull.Value },
                    new SqlParameter("Foto02", SqlDbType.VarBinary) { Value = imagensByte[1] ?? DBNull.Value },
                    new SqlParameter("Foto03", SqlDbType.VarBinary) { Value = imagensByte[2] ?? DBNull.Value },

                };
			}
			return sp;

		}
		protected override FotosViagemViewModel MontarModel(DataRow registro)
		{
			FotosViagemViewModel model = new FotosViagemViewModel()
			{
				dataFoto = Convert.ToDateTime(registro["dataFoto"]),
				id = Convert.ToInt32(registro["id"]),
				localFoto = Convert.ToString(registro["localFoto"]),
				usuario = Convert.ToInt32(registro["usuario"])

			};
			for(int i = 0; i<3; i++)
			{
				if (registro[$"foto0{i+1}"] != DBNull.Value)
				{
					model.fotosByte.Add( registro[$"foto0{i+1}"] as byte[]);

				}
			}
			CadastroDAO cDao = new CadastroDAO();
			CadastroViewModel cadastro = cDao.Consulta(model.usuario);
			model.loginUsuario = cadastro.loginUsuario;
			model.nomeUsuario = cadastro.nome;
			return model;
		}
	}
}


