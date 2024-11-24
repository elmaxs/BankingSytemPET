using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Model
{
    public class BankOperation
    {
        public int Id { get; set; }
        public int NumberOperation { get; set; }
        public BankAccount FromAccount { get; set; }
        public BankAccount ToAccount { get; set; }
        public DateTime OperationTime { get; set; }
        public TypeOperation OperationType { get; set; }

        public BankOperation() { }

        public BankOperation(int numberOperation, DateTime operationTime, TypeOperation operation, BankAccount fromAccount, BankAccount toAccount)
        {
            if (numberOperation <= 0) throw new ArgumentException("Number operation cant be less or equal 0", nameof(numberOperation));
            if (fromAccount == null) throw new ArgumentNullException("User cant be null", nameof(fromAccount));
            if (toAccount == null) throw new ArgumentNullException("User cant be null", nameof(toAccount));
            if (operationTime == DateTime.MinValue) throw new ArgumentException("Date time cant be min value", nameof(operationTime));
            if (!Enum.IsDefined(typeof(TypeOperation), operation)) throw new ArgumentException("Not correct type operation data", nameof(operation));

            NumberOperation = numberOperation;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            OperationTime = operationTime;
            OperationType = operation;
        }

        public override string ToString()
        {
            return $"{NumberOperation}, {OperationType}, {FromAccount.User.Indef}, {FromAccount.User.Indef}, {OperationTime}";
        }
    }
}
