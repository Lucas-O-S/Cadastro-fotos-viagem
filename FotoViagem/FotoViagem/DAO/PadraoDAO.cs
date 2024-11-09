using System.Data.SqlClient;
using System.Data;
using FotosViagem.Models;

namespace FotosViagem.DAO
{
    public abstract class PadraoDAO<T> where T : PadraoViewModel 
    {
        public PadraoDAO() { SetTabela(); }
        protected string tabela { get; set; }
        protected string NomeSpListagem { get; set; } = "sp_list";
        protected abstract SqlParameter[] CriarParametros(T model);
        protected abstract T MontarModel(DataRow registro);
        protected abstract void SetTabela();
        public virtual void Insert(T model)
        {
            HelperDAO.ExecutaProc("sp_insert_" + tabela, CriarParametros(model));
        }
        public virtual void Update(T model)
        {
            HelperDAO.ExecutaProc("sp_update_" + tabela, CriarParametros(model));
        }
        public virtual void Delete(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id",id),
                new SqlParameter("tabela",tabela)
            };
            HelperDAO.ExecutaProc("sp_delete", p);
        }

        public virtual T Consulta(int? id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id",id),
                new SqlParameter("tabela",tabela)
            };

            DataTable dt = HelperDAO.ExecutaProcSelect("sp_select", p);
            if (dt.Rows.Count == 0)
                return null;
            else
                return MontarModel(dt.Rows[0]);

        }

        public virtual List<T> Listagem()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela",tabela)
            };
            DataTable dt = HelperDAO.ExecutaProcSelect(NomeSpListagem, p);
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
                list.Add(MontarModel(dr));
            return list;
        }

    }
}
