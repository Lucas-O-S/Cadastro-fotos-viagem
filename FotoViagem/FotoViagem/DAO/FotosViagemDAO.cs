using FotosViagem.Models;
using System.Data;
using System.Data.SqlClient;

namespace FotosViagem.DAO
{
	public class FotosViagemDAO : PadraoDAO<FotosViagemViewModel>
	{
		protected override void SetTabela()
		{
			tabela = "ForosViagem";
		}
		protected override SqlParameter[] CriarParametros(FotosViagemViewModel model)
		{
			object[] imagensByte = new object[3];
			imagensByte[0] = model.fotosByte[0];
			imagensByte[1] = model.fotosByte[1];
			imagensByte[2] = model.fotosByte[2];

			
			for(int i = 0; i<3; i++)
			{
				if (imagensByte[i] == null)
					imagensByte[i] = DBNull.Value;
			}

			SqlParameter[] sp;
			if (model.id == null || model.id ==0)
			{
				sp = new SqlParameter[]
				{
					new SqlParameter("dataFoto",model.dataFoto),
					new SqlParameter("",model.localFoto),
					new SqlParameter("usuario",model.usuario),
					new SqlParameter("Foto01", imagensByte[0]),
					new SqlParameter("Foto02", imagensByte[1]),
					new SqlParameter("Foto03", imagensByte[2])

				};
			}
			else
			{
				sp = new SqlParameter[]
				{
					new SqlParameter("id",model.id),
					new SqlParameter("dataFoto",model.dataFoto),
					new SqlParameter("",model.localFoto),
					new SqlParameter("usuario",model.usuario),
					new SqlParameter("Foto01", imagensByte[0]),
					new SqlParameter("Foto02", imagensByte[1]),
					new SqlParameter("Foto03", imagensByte[2])

				};
			}
			return sp;

		}
		protected override FotosViagemViewModel MontarModel(DataRow registro)
		{
			FotosViagemViewModel fotosViagem = new FotosViagemViewModel()
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
					fotosViagem.fotosByte.Add( registro[$"foto0{i+1}"] as byte[]);

				}
			}
			return fotosViagem;
		}
	}
}
