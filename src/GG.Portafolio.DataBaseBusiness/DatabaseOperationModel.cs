using System;
using System.Data.CRUD;

namespace GG.Portafolio.DataBaseBusiness
{
    public class DatabaseOperationModel
    {
        public OperationType Operation { get; set; }

        public IEntity Entity { get; set; }

        public Func<IEntity, bool> AfterExecute { get; set; }
    }
}
