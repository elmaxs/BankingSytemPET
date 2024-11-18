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
        public User FromUser { get; set; }
        public User ToUser { get; set; }
        public DateTime OperationTime { get; set; }
        public TypeOperation OperationType { get; set; }

        public BankOperation(int numberOperation, DateTime operationTime, TypeOperation operation, User fromUser, User toUser)
        {
            if (numberOperation <= 0) throw new ArgumentException("Number operation cant be less or equal 0", nameof(numberOperation));
            if (fromUser == null) throw new ArgumentNullException("User cant be null", nameof(fromUser));
            if (toUser == null) throw new ArgumentNullException("User cant be null", nameof(toUser));
            if (operationTime == DateTime.MinValue) throw new ArgumentException("Date time cant be min value", nameof(operationTime));
            if (!Enum.IsDefined(typeof(TypeOperation), operation)) throw new ArgumentException("Not correct type operation data", nameof(operation));

            NumberOperation = numberOperation;
            FromUser = fromUser;
            ToUser = toUser;
            OperationTime = operationTime;
            OperationType = operation;
        }
    }
}
