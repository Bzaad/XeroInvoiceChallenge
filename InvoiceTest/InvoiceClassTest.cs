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

		[Fact]
		public void Test_TypeCasting()
		{
			var invoice = new Invoice()
			{
				InvoiceDate = DateTime.Now,
				InvoiceNumber = 101,
				LineItems = new List<InvoiceLine>()
				{
					new InvoiceLine() {Cost = 125.5, Description = "some description", InvoiceLineId = 201, Quantity = 20}
				}
			};
		}

		/// <summary>
		/// Tests if a deep clone of the Invoice object is correctly performed.
		/// </summary>
		[Fact]
		public void Test_InvoiceDeepClone_CorrectlyCreatesADeepCloneOfInvoice()
		{
			var invoice = new Invoice()
			{
				InvoiceDate = DateTime.Now,
				InvoiceNumber = 2,
				LineItems = new List<InvoiceLine>()
				{
					new InvoiceLine(){Cost = 4.5, Description = "First Line Item", InvoiceLineId =  21, Quantity = 1},
					new InvoiceLine(){Cost = 10.67, Description = "Second Line Item", InvoiceLineId = 22, Quantity = 2}
				}
			};

			var clonedInvoice = invoice.Clone();

			Assert.Equal(invoice.InvoiceNumber, clonedInvoice.InvoiceNumber);
			Assert.Equal(invoice.InvoiceDate, clonedInvoice.InvoiceDate);

			invoice.LineItems.ForEach(x =>
			{
				Assert.True(clonedInvoice.LineItems.Exists(y => y.InvoiceLineId == x.InvoiceLineId));
				Assert.True(clonedInvoice.LineItems.Exists(y => y.Description == x.Description));
				Assert.True(clonedInvoice.LineItems.Exists(y => y.Quantity == x.Quantity));
				Assert.True(clonedInvoice.LineItems.Exists(y => y.Cost == x.Cost));
			});

			clonedInvoice.InvoiceNumber = 20;
			Assert.NotEqual(invoice.InvoiceNumber, clonedInvoice.InvoiceNumber);

			invoice.InvoiceDate = invoice.InvoiceDate.AddDays(1);
			Assert.NotEqual(invoice.InvoiceDate, clonedInvoice.InvoiceDate);

			clonedInvoice.LineItems.ForEach(x =>
			{

			});
		}
	}
}
