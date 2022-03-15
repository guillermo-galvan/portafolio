using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Transactions;
using System.Reflection;

namespace GG.Portafolio.DataBaseBusiness
{
    public class DatabaseOperation
    {
        private bool Execute(IDataBaseConnection connection, IEnumerable<DatabaseOperationModel> operatios)
        {
            bool completeTransaction = true;
            bool? tmpCompleteTransaction = null;

            foreach (var ope in operatios)
            {
                switch (ope.Operation)
                {
                    case OperationType.Insert:
                        ope.Entity.Insert(connection);
                        break;
                    case OperationType.Update:
                        ope.Entity.Update(connection);
                        break;
                    case OperationType.Delte:
                        ope.Entity.Delete(connection);
                        break;
                }

                tmpCompleteTransaction = ope.AfterExecute?.Invoke(ope.Entity);
                completeTransaction = tmpCompleteTransaction ?? completeTransaction;
            }

            return completeTransaction;
        }

        public void WithTransaction(IDataBaseConnection connection, IEnumerable<DatabaseOperationModel> operatios, 
            ILogger logger)
        {
            try
            {
                using TransactionScope tran = new(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.Serializable });

                if (Execute(connection, operatios))
                {
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public void WithoutTransaction(IDataBaseConnection connection, IEnumerable<DatabaseOperationModel> operatios, 
            ILogger logger)
        {
            try
            {
                Execute(connection, operatios);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
