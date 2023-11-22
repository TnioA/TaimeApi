using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Contracts.Transaction
{
    public class TransactionResponse
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal DiscountValue { get; set; }

        public ShippingType ShippingType { get; set; }

        public decimal ShippingValue { get; set; }

        public decimal SubTotalValue { get; set; }

        public decimal TotalValue { get; set; }

        public TransactionType TransactionType { get; set; }

        public TransactionType TransactionStatus { get; set; }

        public PaymentType PaymentType { get; set; }

        public TransactionResponse() { }

        public TransactionResponse(TransactionEntity transactionEntity) 
        { 
            Id = transactionEntity.Id;
            Description = transactionEntity.Description;
            ShippingType = transactionEntity.ShippingType;
            ShippingValue = transactionEntity.ShippingValue;
            SubTotalValue = transactionEntity.SubTotalValue;
            TotalValue = transactionEntity.TotalValue;
            TransactionStatus = transactionEntity.TransactionStatus;
            PaymentType = transactionEntity.PaymentType;
            TransactionStatus = transactionEntity.TransactionStatus;
            PaymentType = transactionEntity.PaymentType;
        }
    }
}
