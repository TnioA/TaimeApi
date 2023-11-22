using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Entities
{
    [Table("transaction")]
    public class TransactionEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("discount_value")]
        public decimal DiscountValue { get; set; }

        [Column("shipping_type")]
        public ShippingType ShippingType { get; set; }

        [Column("shipping_value")]
        public decimal ShippingValue { get; set; }

        [Column("shipping_address")]
        public string ShippingAddress { get; set; }

        [Column("sub_total_value")]
        public decimal SubTotalValue { get; set; }

        [Column("total_value")]
        public decimal TotalValue { get; set; }

        [Column("transaction_type")]
        public TransactionType TransactionType { get; set; }

        [Column("transaction_status")]
        public TransactionType TransactionStatus { get; set; }

        [Column("payment_type")]
        public PaymentType PaymentType { get; set; }

        [Column("bank_payment_id")]
        public string BankPaymentId { get; set; }
    }

    public enum TransactionType
    {
        TransactionIn = 0,
        TransactionOut = 1
    }

    public enum TransactionStatus
    {
        Created = 0,
        WatingPayment = 1,
        PaymentConfirmed = 2,
        Sended = 3,
        WatingTaking = 4,
        Finished = 5,
        Cancelled = 6
    }

    public enum PaymentType
    {
        PaymentOnTakeOff = 0,
        PixTransfer = 1,
        CreditCard = 2,
        BankBolet = 3
    }

    public enum ShippingType
    {
        TakeOff = 0,
        MotoDelivery = 1,
        SlowShipping = 2,
        FastShipping = 3
    }
}