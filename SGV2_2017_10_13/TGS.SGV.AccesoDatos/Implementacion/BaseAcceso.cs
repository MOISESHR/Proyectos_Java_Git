using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.AccesoDatos.Implementacion
{
    public class BaseAcceso : IDisposable
    {
        private DbContext _context;

        public BaseAcceso(DbContext context)
        {
            _context = context;
        }

        protected DbContext Context
        {
            get { return _context; }
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Context.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }
        
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }

}
