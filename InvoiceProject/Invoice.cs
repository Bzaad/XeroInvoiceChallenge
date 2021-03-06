using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace InvoiceProject
{
	[Serializable]
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> LineItems { get; set; }

        /// <summary>
        /// Adds the given InvoiceLine to LineItems.
        /// </summary>
        /// <param name="invoiceLine">The InvoiceLine being added</param>
        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            //Check if invoice line has been initiated if not initiate it with an empty list.
            LineItems ??= new List<InvoiceLine>();
            LineItems.Add(invoiceLine);
        }

        /// <summary>
        /// Removes the InvoiceLine with the given id from LineItems.
        /// </summary>
        /// <param name="SOMEID">The id of the InvoiceLine being removed</param>
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
	        LineItems.AddRange(sourceInvoice.LineItems);
        }

        /// <summary>
        /// Creates a deep clone of the current invoice (all fields and properties)
        /// </summary>
        public Invoice Clone()
        {
            /*
	        Invoice other = (Invoice) this.MemberwiseClone();
	        other.InvoiceNumber = InvoiceNumber;
	        other.InvoiceDate = InvoiceDate;
	        other.LineItems = new List<InvoiceLine>();
            LineItems.ForEach(x =>
            {
	            var lineItem = new InvoiceLine()
	            {
                    Cost = x.Cost,
					Description = x.Description,
                    InvoiceLineId = x.InvoiceLineId,
                    Quantity = x.Quantity
	            };
                other.LineItems.Add(lineItem);
            });
            return other;
            */
            return this.DeepClone();
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [dd/MM/yyyy], LineItemCount: [Number of items in LineItems]
        /// </summary>
        public override string ToString()
        {
	        return $"InvoiceNumber: {InvoiceNumber}, InvoiceDate: {InvoiceDate:dd/MM/yyy}, LineItemCount: {LineItems.Count}";
        }
    }
}
