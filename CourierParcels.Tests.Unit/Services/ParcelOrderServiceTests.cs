using System.Text;
using CourierParcels.Data;
using CourierParcels.Services;

namespace CourierParcels.Tests.Unit.Services
{
	public class ParcelOrderServiceTests
    {
        IParcelOrderService _parcelOrderServiceSUT;

        [SetUp]
        public void Setup()
        {
            _parcelOrderServiceSUT = new ParcelOrderService();
        }

        [Test]
        public void PrintParcelOrderSummary_GIVEN_ValidParcelOrderWithOneParcel_THEN_ShouldReturnOrderSummaryInString()
        {
            //arrange
            var expectedOrderSummary = "Small Parcel (2KG): $3.\nTotal Cost: $3.\n";

            var givenParcel1 = new Parcel
            {
                ParcelSizeType = ParcelSizeType.Small,
                Length = 10,
                Width = 10,
                Height = 10,
                Weight = 2,
                Cost = 3,
                IsValid = true
            };

            var givenParcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() { givenParcel1 }
            };

           
            //act
            var result = _parcelOrderServiceSUT.PrintParcelOrderSummary(givenParcelOrder);

            //assert
            Assert.That(result, Is.EqualTo(expectedOrderSummary));
        }

        [Test]
        public void PrintParcelOrderSummary_GIVEN_ValidParcelOrderWithMultipleParcel_THEN_ShouldReturnOrderSummaryInString()
        {
            //arrange
            var expectedOrderSummary = new StringBuilder();
            expectedOrderSummary.AppendLine("XL Parcel (12KG): $25.");
            expectedOrderSummary.AppendLine("Small Parcel (2KG): $3.");
            expectedOrderSummary.AppendLine("Medium Parcel (2KG): $8.");
            expectedOrderSummary.AppendLine("Total Cost: $36.");


            var givenParcel1 = new Parcel
            {
                ParcelSizeType = ParcelSizeType.XL,
                Length = 100,
                Width = 10,
                Height = 10,
                Weight =12,
                Cost = 25,
                IsValid = true
            };
            var givenParcel2 = new Parcel
            {
                ParcelSizeType = ParcelSizeType.Small,
                Length = 10,
                Width = 10,
                Height = 10,
                Weight = 2,
                Cost = 3,
                IsValid = true
            };
            var givenParcel3 = new Parcel
            {
                ParcelSizeType = ParcelSizeType.Medium,
                Length = 50,
                Width = 50,
                Height = 50,
                Weight = 2,
                Cost = 8,
                IsValid = true
            };

            var givenParcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() { givenParcel1, givenParcel2, givenParcel3 }
            };


            //act
            var result = _parcelOrderServiceSUT.PrintParcelOrderSummary(givenParcelOrder);

            //assert
            Assert.That(result, Is.EqualTo(expectedOrderSummary.ToString()));
        }

        [Test]
        public void PrintParcelOrderSummary_GIVEN_ValidParcelOrderWithSpeedyShipping_THEN_ShouldReturnOrderSummaryInString()
        {
            //arrange
            var expectedOrderSummary = new StringBuilder();
            expectedOrderSummary.AppendLine("Small Parcel (1KG): $3.");
            expectedOrderSummary.AppendLine("Speedy shipping: $3.");
            expectedOrderSummary.AppendLine("Total Cost: $6.");

            var givenParcel1 = new Parcel
            {
                ParcelSizeType = ParcelSizeType.Small,
                Length = 10,
                Width = 10,
                Height = 10,
                Weight = 1,
                Cost = 3,
                IsValid = true
            };

            var givenParcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() { givenParcel1 },
                IsSpeedyShipping = true
            };


            //act
            var result = _parcelOrderServiceSUT.PrintParcelOrderSummary(givenParcelOrder);

            //assert
            Assert.That(result, Is.EqualTo(expectedOrderSummary.ToString()));
        }

        [Test]
        public void PrintParcelOrderSummary_GIVEN_ValidParcelOrderWithSpeedyShipping_AND_Discount_THEN_ShouldReturnOrderSummaryInString()
        {
            //arrange
            var expectedOrderSummary = new StringBuilder();
            expectedOrderSummary.AppendLine("Small Parcel (1KG): $3.");
            expectedOrderSummary.AppendLine("Small Parcel (1KG): $3.");
            expectedOrderSummary.AppendLine("Small Parcel (1KG): $3.");
            expectedOrderSummary.AppendLine("Small Parcel (1KG): $3.");
            expectedOrderSummary.AppendLine("Discount offer (Small Type): $-3.");
            expectedOrderSummary.AppendLine("Speedy shipping: $9.");
            expectedOrderSummary.AppendLine("Total Cost: $18.");

            var givenParcel1 = new Parcel
            {
                ParcelSizeType = ParcelSizeType.Small,
                Length = 10,
                Width = 10,
                Height = 10,
                Weight = 1,
                Cost = 3,
                IsValid = true
            };

            var givenParcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() { givenParcel1, givenParcel1, givenParcel1, givenParcel1 },
                OrderDiscounts = new List<OrderDiscount>() { new OrderDiscount { DiscountType = DiscountType.Small, Amount = 3 } },
                IsSpeedyShipping = true
            };


            //act
            var result = _parcelOrderServiceSUT.PrintParcelOrderSummary(givenParcelOrder);

            //assert
            Assert.That(result, Is.EqualTo(expectedOrderSummary.ToString()));
        }
    }
}

