using System;
using System.Collections.Generic;
using Xunit;
using InvoiceProject;

namespace InvoiceProjectTest
{
	public class InvoiceClassTest
	{
		/// <summary>
		/// Tests if the new invoice line has been added to invoice successfully.
		/// </summary>
		[Fact]
		public void Test_AddInvoiceLine_SuccessfullyAddsTheCorrectLineItemToInvoice()
		{
			var invoice = new Invoice
			{
				InvoiceNumber = 1,
				InvoiceDate = DateTime.Now,
				LineItems = new List<InvoiceLine>
				{
					new InvoiceLine {InvoiceLineId = 11, Description = "Banana", Quantity = 10, Cost = 25.5},
				}
			};

			var invoiceLine1 = new InvoiceLine {InvoiceLineId = 12, Description = "Orange", Quantity = 25, Cost = 5.5m};
			var invoiceLine2 = new InvoiceLine {InvoiceLineId = 13, Description = "Blueberry", Quantity = 01, Cost = 5001};

			Assert.Single(invoice.LineItems);

			invoice.AddInvoiceLine(invoiceLine1);
			invoice.AddInvoiceLine(invoiceLine2);

			Assert.Equal(3, invoice.LineItems.Count);
			Assert.True(invoice.LineItems.Exists(x => x.InvoiceLineId == 12));
			Assert.True(invoice.LineItems.Exists(x => x.InvoiceLineId == 13));
		}

		/// <summary>
		/// Tests if the correct invoice line item is being removed.
		/// </summary>
		[Fact]
		public void Test_RemoveInvoiceLine_RemovesTheCorrectLineItemFromInvoice()
		{
			var invoice = new Invoice
			{
				InvoiceNumber = 2,
				InvoiceDate = DateTime.Now,
				LineItems = new List<InvoiceLine>
				{
					new InvoiceLine {InvoiceLineId = 11, Description = "Banana", Quantity = 10, Cost = 25.5},
					new InvoiceLine {InvoiceLineId = 12, Description = "Orange", Quantity = 25, Cost = 5.5m},
					new InvoiceLine {InvoiceLineId = 13, Description = "Blueberry", Quantity = 01, Cost = 5001}
				}
			};

			Assert.Equal(3, invoice.LineItems.Count);

			invoice.RemoveInvoiceLine(12);

			Assert.Equal(2, invoice.LineItems.Count);
			Assert.True(!invoice.LineItems.Exists(x => x.InvoiceLineId == 12));
			Assert.True(invoice.LineItems.Exists(x => x.InvoiceLineId == 11));
			Assert.True(invoice.LineItems.Exists(x => x.InvoiceLineId == 13));
		}

		/// <summary>
		/// Tests is the correct total of Cost * Quantity for all the items in an invoice is returned.
		/// </summary>
		[Fact]
		public void Test_GetTotal_ReturnsTheCorrectTotalOfCostMultipliedByQualityForAllLineItems()
		{
			var invoice = new Invoice
			{
				InvoiceNumber = 2,
				InvoiceDate = DateTime.Now,
				LineItems = new List<InvoiceLine>
				{
					new InvoiceLine {InvoiceLineId = 11, Description = "Banana", Quantity = 10, Cost = 25.5},
					new InvoiceLine {InvoiceLineId = 12, Description = "Orange", Quantity = 25, Cost = 5.5m},
					new InvoiceLine {InvoiceLineId = 13, Description = "Blueberry", Quantity = 01, Cost = 5001}
				}
			};

			Assert.Equal(5393.5m, invoice.GetTotal());
		}

		/// <summary>
		/// Tests if the invoice object with one line item is being properly created.
		/// </summary>
		[Fact]
		public void Test_CreateInvoice_CreatesInvoiceWithOneItem()
		{
			var invoice = new Invoice
			{
				InvoiceNumber = 1,
				InvoiceDate = new DateTime(2021,3,4,12,01,00),
				LineItems = new List<InvoiceLine>
				{
					new InvoiceLine {InvoiceLineId = 11, Cost = 25.5, Description = "1st Invoice Line Item", Quantity = 10},
				}
			};

			var testDate = new DateTime(2021, 3, 4, 12, 01, 00);

			Assert.Equal(0, DateTime.Compare(invoice.InvoiceDate, testDate));
			Assert.Equal(1, invoice.InvoiceNumber);
			Assert.Single(invoice.LineItems);
		}

		/// <summary>
		/// Tests it the invoice creation throws NotSupportedException if an invalid data type is assigned to cost.
		/// </summary>
		[Fact]
		public void Test_CreateInvoice_ThrowsNotSupportedExceptionWhenAddingInvalidTypeCost()
		{
			Assert.Throws<NotSupportedException>(() =>
			{
				var invoice = new Invoice
				{
					InvoiceNumber = 1,
					InvoiceDate = new DateTime(2021, 3, 4, 12, 01, 00),
					LineItems = new List<InvoiceLine>
					{
						new InvoiceLine {InvoiceLineId = 11, Cost = "err", Description = "1st Invoice Line Item", Quantity = 10},
					}
				};
			});
		}

		/// <summary>
		/// Tests if the invoice object is with multiple line items being properly created.
		/// </summary>
		[Fact]
		public void Test_CreateInvoice_CreatesInvoiceWithMultipleItems()
		{
			var invoice = new Invoice
			{
				InvoiceNumber = 2,
				InvoiceDate = new DateTime(2022, 5, 5, 11, 12, 00),
				LineItems = new List<InvoiceLine>
				{
					new InvoiceLine {InvoiceLineId = 11, Description = "Apple", Quantity = 10, Cost = 25.5},
					new InvoiceLine {InvoiceLineId = 12, Description = "Oranges", Quantity = 25, Cost = 5.5m },
					new InvoiceLine {InvoiceLineId = 13, Description = "Pineapple", Quantity = 01, Cost = 5001}
				}
			};

			var testDate = new DateTime(2022, 5, 5, 11, 12, 00);
			Assert.Equal(0, DateTime.Compare(invoice.InvoiceDate, testDate));
			Assert.Equal(2, invoice.InvoiceNumber);
			Assert.Equal(3, invoice.LineItems.Count);
		}

		/// <summary>
		/// Tests if line items from two invoices are being merged properly.
		/// </summary>
		[Fact]
		public void Test_MergeInvoices_MergesLineItemsOfTwoInvoices()
		{
			var invoice1 = new Invoice
			{
				InvoiceNumber = 1,
				InvoiceDate = DateTime.Now,
				LineItems = new List<InvoiceLine>
				{
					new InvoiceLine {InvoiceLineId = 11, Description = "Banana", Quantity = 10, Cost = 25.5},
					new InvoiceLine {InvoiceLineId = 12, Description = "Orange", Quantity = 25, Cost = 5.5m}
				}
			};

			var invoice2 = new Invoice
			{
				InvoiceNumber = 2,
				InvoiceDate = DateTime.Now,
				LineItems = new List<InvoiceLine>
				{
					new InvoiceLine {InvoiceLineId = 21, Description = "Blueberry", Quantity = 01, Cost = 10},
					new InvoiceLine {InvoiceLineId = 22, Description = "Pineapple", Quantity = 01, Cost = 5.2}
				}
			};

			Assert.Equal(2, invoice1.LineItems.Count);
			invoice1.MergeInvoices(invoice2);
			Assert.Equal(4, invoice1.LineItems.Count);
			Assert.True(invoice1.LineItems.Exists(x => x.InvoiceLineId == 21));
			Assert.True(invoice1.LineItems.Exists(x => x.InvoiceLineId == 22));
		}

		/// <summary>
		/// Tests if a deep clone of the Invoice object is correctly performed.
		/// </summary>
		[Fact]
		public void Test_InvoiceDeepClone_CorrectlyCreatesADeepCloneOfInvoice()
		{
			var invoice = new Invoice
			{
				InvoiceDate = DateTime.Now,
				InvoiceNumber = 2,
				LineItems = new List<InvoiceLine>
				{
					new InvoiceLine{InvoiceLineId =  21, Description = "Apple", Quantity = 1, Cost = 4.5},
					new InvoiceLine{InvoiceLineId = 22, Description = "Blueberries", Quantity = 2, Cost = 10.67}
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
				x.InvoiceLineId += 10;
				x.Description += "(Cloned)";
				x.Quantity += 25;
				x.Cost += 25.2m;
			});

			clonedInvoice.LineItems.ForEach(x =>
			{
				Assert.True(!invoice.LineItems.Exists(y => y.InvoiceLineId == x.InvoiceLineId));
				Assert.True(!invoice.LineItems.Exists(y => y.Description == x.Description));
				Assert.True(!invoice.LineItems.Exists(y => y.Quantity == x.Quantity));
				Assert.True(!invoice.LineItems.Exists(y => y.Cost == x.Cost));
			});
		}

		/// <summary>
		/// Tests if toString method produces the correct string.
		/// </summary>
		[Fact]
		public void Test_ToString_ReturnsTheCorrectString()
		{
			var invoice = new Invoice
			{
				InvoiceDate = new DateTime(2021, 3, 4, 12, 01, 00),
				InvoiceNumber = 2,
				LineItems = new List<InvoiceLine>()
			};
			Assert.Equal("InvoiceNumber: 2, InvoiceDate: 04/03/2021, LineItemCount: 0", invoice.ToString());

			invoice.InvoiceDate = new DateTime(2022, 4, 5, 12, 01, 00);
			invoice.InvoiceNumber = 100;
			invoice.AddInvoiceLine(new InvoiceLine { Cost = 10.1, Description = "Electricity Bitll", InvoiceLineId = 1, Quantity = 1 });

			Assert.Equal("InvoiceNumber: 100, InvoiceDate: 05/04/2022, LineItemCount: 1", invoice.ToString());
		}

		[Fact]
		public void Test_TaxType()
        {
			var tax = new Tax<NZTax>(new NZTax(new DateTime(2011,1,1)));
			var taxValue = tax.GetTaxValue(1000);
			Assert.Equal(125, taxValue);
        }
	}
}