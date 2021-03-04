using System;
using System.Collections.Generic;
using Xunit;
using InvoiceProject;

namespace InvoiceProjectTest
{
	public class InvoiceClassTest
	{
		/// <summary>
		/// Test if the invoice object is being properly created.
		/// </summary>
		[Fact]
		public void Test1()
		{
			var invoice = new Invoice()
			{
				InvoiceDate = new DateTime(2021,3,4,12,01,00),
				InvoiceNumber = 1,
				LineItems = new List<InvoiceLine>()
			};

			var testDate = new DateTime(2021, 3, 4, 12, 01, 00);
			Assert.Equal(0, DateTime.Compare(invoice.InvoiceDate, testDate));
			Assert.Equal(1,invoice.InvoiceNumber);
			Assert.Empty(invoice.LineItems);
		}

		/// <summary>
		/// Tests if toString method produces the correct string.
		/// </summary>
		[Fact]
		public void ToString_ReturnsTheCorrectString()
		{
			var invoice = new Invoice()
			{
				InvoiceDate = new DateTime(2021, 3, 4, 12, 01, 00),
				InvoiceNumber = 2,
				LineItems = new List<InvoiceLine>()
			};
			Assert.Equal("InvoiceNumber: 2, InvoiceDate: 04/03/2021, LineItemCount: 0", invoice.ToString());

			invoice.InvoiceDate = new DateTime(2022,4,5,12,01,00);
			invoice.InvoiceNumber = 100;
			invoice.AddInvoiceLine(new InvoiceLine(){Cost = 10.1,Description = "Electricity Bitll", InvoiceLineId = 1, Quantity = 1});

			Assert.Equal("InvoiceNumber: 100, InvoiceDate: 05/04/2022, LineItemCount: 1", invoice.ToString());
		}
	}
}
