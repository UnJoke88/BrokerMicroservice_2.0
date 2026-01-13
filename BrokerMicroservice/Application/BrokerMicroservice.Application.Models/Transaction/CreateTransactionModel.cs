using BrokerMicroservice.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerMicroservice.Application.Models.Transaction
{
    public record class CreateTransactionModel(Guid ClientId, DateTime Date, TransactionType Type, Guid? AssetId, int? Quantity, decimal Amount,
        TransactionStatus Status) : ICreateModel;
}
