using System;
namespace InvoiceProject
{
    public class Tax<T> where T : TaxType
    {
        public Tax(T taxType)
        {
                this.TType = taxType;
        }

        public T TType { get; set; }

        public decimal GetTaxValue(decimal total)
        {
            return total * TType.TaxRate;
        }

        public decimal TotalWithTax(decimal total)
        {
            return GetTaxValue(total) + total;
        }
    }
}
