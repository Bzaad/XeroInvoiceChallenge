using System;
using System.Collections.Generic;

namespace InvoiceProject
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> LineItems { get; set; }

        public Invoice(int invoiceNumber, DateTime invoiceDate, List<InvoiceLine> lineItems)
        {
	        InvoiceNumber = invoiceNumber;
	        InvoiceDate = invoiceDate;
	        LineItems = lineItems;
        }

        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            //Check if invoice line has been initiated if not initiate it with an empty list.
            LineItems ??= new List<InvoiceLine>();

            LineItems.Add(invoiceLine);
        }

        public void RemoveInvoiceLine(int SOMEID)
        {
	        LineItems.RemoveAll(x => x.InvoiceLineId == SOMEID);
        }   

        /// <summary>
        /// GetTotal should return the sum of (Cost * Quantity) for each line item
        /// </summary>
        public decimal GetTotal()
        {
	        var total = 0m;
	        LineItems.ForEach(x => total += ((decimal)x.Cost * x.Quantity));
	        return total;
        }

        /// <summary>
        /// MergeInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="sourceInvoice">Invoice to merge from</param>
        public void MergeInvoices(Invoice sourceInvoice)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a deep clone of the current invoice (all fields and properties)
        /// </summary>
        public Invoice Clone()
        {
	        return new Invoice()
	        {
		        InvoiceDate = this.InvoiceDate,
		        InvoiceNumber = this.InvoiceNumber,
		        LineItems = this.LineItems
	        };

	        // throw new NotImplementedException();
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [dd/MM/yyyy], LineItemCount: [Number of items in LineItems]
        /// </summary>
        public override string ToString()
        {
	        return $"InvoiceNumber: {InvoiceNumber}, InvoiceDate: {InvoiceDate:dd/MM/yyy}, LineItemCount: {LineItems.Count}";
	        //throw new NotImplementedException();
        }
    }
}
