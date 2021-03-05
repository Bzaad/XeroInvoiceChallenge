namespace InvoiceProject
{
    public class InvoiceLine
    {
	    public InvoiceLine(int invoiceLineId, string description, int quantity, dynamic cost)
	    {
		    InvoiceLineId = invoiceLineId;
		    Description = description;
		    Quantity = quantity;
		    Cost = cost;
	    }

        public int InvoiceLineId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public dynamic Cost { get; set; }
    }
}