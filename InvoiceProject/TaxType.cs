using System;
namespace InvoiceProject
{
    public interface TaxType
    {
        public string Code { get; }
        public decimal TaxRate { get; }
    }

    public class NZTax : TaxType
    {
        public NZTax(DateTime invoiceDate)
        {
            this.InvoiceDate = invoiceDate;
        }

        public string Code => "NZ";
        public decimal TaxRate { get
            {
                if (InvoiceDate.CompareTo(new DateTime(2013, 1, 1)) < 0) return 0.125m;
                return 0.15m;
            }
        }
        public DateTime InvoiceDate { get; set; }
    }

    public class AUTax : TaxType
    {
        public string Code => "AU";
        public decimal TaxRate => 0.175m;
    }

    public class UKTax: TaxType
    {
        public string Code => "UK";
        public decimal TaxRate => 0.21m;
    }
}
