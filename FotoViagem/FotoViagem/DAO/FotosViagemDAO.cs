using FotoViagem.Models;
using System.Data.SqlClient;

namespace FotoViagem.DAO
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
					new SqlParameter("",model.localForo),
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
					new SqlParameter("",model.localForo),
					new SqlParameter("usuario",model.usuario),
					new SqlParameter("Foto01", imagensByte[0]),
					new SqlParameter("Foto02", imagensByte[1]),
					new SqlParameter("Foto03", imagensByte[2])

				};
			}
			return sp;

		}
	}
}
