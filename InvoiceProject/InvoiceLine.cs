using System;

namespace InvoiceProject
{
	public class InvoiceLine
    {
	    public int InvoiceLineId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        //2021,03,06 - Behzad Farokhi - The cost is being defined as decimal in the Private object
        //This is because for the cost we need the most precise answer and the decimal can offer that
        private decimal DecimalCost { get; set; }

        // 2021,03,06 - Behzad Farokhi - The data is returned in a way that we are not be sure if the type is
        // decimal or double (Based on the inconsistency of the data in the provided example).
		// This is not the best practice because the type checking is done only on runtime.
		public dynamic Cost
        {
	        get => DecimalCost;
	        set
	        {
		        if (value is double || value is decimal || value is int)
			        DecimalCost = (decimal) value;
		        else 
					throw new NotSupportedException("The type is not supported. Only int, decimal, and long values are accepted.");
	        }
        }
    }
}