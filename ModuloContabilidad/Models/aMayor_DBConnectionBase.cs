using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdConta.Models
{
    public abstract class aMayor_DBConnectionBase : aDBConnectionBase
    {
        public virtual void CreateTable(string tableName, string cod, LedgeAccount acc)
        {
            throw new NotImplementedException();
        }        
    }
}