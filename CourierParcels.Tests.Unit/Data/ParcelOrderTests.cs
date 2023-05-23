using System;
using CourierParcels.Data;
using CourierParcels.Services;

namespace CourierParcels.Tests.Unit.Data
{
	public class ParcelOrderTests
    {
        IParcelService _parcelService;

        [SetUp]
        public void Setup()
        {
            _parcelService = new ParcelService();
        }

        [Test]
        public void CalculateParcelOrderDiscount_GIVEN_NineSmallParcels_THEN_ReturnCorrectCostWithDiscount()
        {
            //arrange
            var givenParcel1 = _parcelService.NewParcel(1, 1, 1, 1); //$3, Expected Free
            var givenParcel2 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenParcel3 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenParcel4 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenParcel5 = _parcelService.NewParcel(1, 1, 1, 2); //$5, Expected Free 
            var givenParcel6 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenParcel7 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenParcel8 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenParcel9 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 

            var parcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() {
                    givenParcel1, givenParcel2, givenParcel3,
                    givenParcel4, givenParcel5, givenParcel6,
                    givenParcel7, givenParcel8, givenParcel9 }
            };

            //act
            parcelOrder.CalculateOrderDiscount();

            //assert
            Assert.That(parcelOrder.OrderDiscounts.Count, Is.EqualTo(2));
            Assert.That(parcelOrder.DiscountAmount, Is.EqualTo(8));
            Assert.That(parcelOrder.TotalCost, Is.EqualTo(43));
        }


        [Test]
        public void CalculateParcelOrderDiscount_GIVEN_OneParcels_THEN_ReturnCorrectCostWithoutDiscount()
        {
            //arrange
            var givenParcel1 = _parcelService.NewParcel(101, 1, 1, 10); //XL Size, $25

            var parcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() { givenParcel1 }
            };

            //act
            parcelOrder.CalculateOrderDiscount();

            //assert
            Assert.That(parcelOrder.OrderDiscounts.Count, Is.EqualTo(0));
            Assert.That(parcelOrder.DiscountAmount, Is.EqualTo(0));
            Assert.That(parcelOrder.TotalCost, Is.EqualTo(25));
        }

        [Test]
        public void CalculateParcelOrderDiscount_GIVEN_SevenMediumParcels_THEN_ReturnCorrectCostWithDiscount()
        {
            //arrange
            var givenParcel1 = _parcelService.NewParcel(10, 10, 10, 3); //$8, Expected Free
            var givenParcel2 = _parcelService.NewParcel(10, 10, 10, 3); //$8
            var givenParcel3 = _parcelService.NewParcel(10, 10, 10, 3); //$8
            var givenParcel4 = _parcelService.NewParcel(10, 10, 10, 3); //$8, Expected Free 
            var givenParcel5 = _parcelService.NewParcel(10, 10, 10, 3); //$8
            var givenParcel6 = _parcelService.NewParcel(10, 10, 10, 5); //$12
            var givenParcel7 = _parcelService.NewParcel(10, 10, 10, 5); //$12 

            var parcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() {
                    givenParcel1, givenParcel2, givenParcel3,
                    givenParcel4, givenParcel5, givenParcel6,
                    givenParcel7}
            };

            //act
            parcelOrder.CalculateOrderDiscount();

            //assert
            Assert.That(parcelOrder.OrderDiscounts.Count, Is.EqualTo(2));
            Assert.That(parcelOrder.DiscountAmount, Is.EqualTo(16));
            Assert.That(parcelOrder.TotalCost, Is.EqualTo(48));
        }

        [Test]
        public void CalculateParcelOrderDiscount_GIVEN_NineSmall_AND_SevenMediumParcels_THEN_ReturnCorrectCostWithDiscount()
        {
            //arrange
            var givenSmallParcel1 = _parcelService.NewParcel(1, 1, 1, 1); //$3, Expected Free
            var givenSmallParcel2 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenSmallParcel3 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenSmallParcel4 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenSmallParcel5 = _parcelService.NewParcel(1, 1, 1, 2); //$5, Expected Free 
            var givenSmallParcel6 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenSmallParcel7 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenSmallParcel8 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenSmallParcel9 = _parcelService.NewParcel(1, 1, 1, 3); //$7,

            var givenMediumParcel1 = _parcelService.NewParcel(10, 10, 10, 3); //$8, Expected Free
            var givenMediumParcel2 = _parcelService.NewParcel(10, 10, 10, 3); //$8
            var givenMediumParcel3 = _parcelService.NewParcel(10, 10, 10, 3); //$8
            var givenMediumParcel4 = _parcelService.NewParcel(10, 10, 10, 3); //$8, Expected Free
            var givenMediumParcel5 = _parcelService.NewParcel(10, 10, 10, 3); //$8 
            var givenMediumParcel6 = _parcelService.NewParcel(10, 10, 10, 5); //$12 
            var givenMediumParcel7 = _parcelService.NewParcel(10, 10, 10, 5); //$12 

            var parcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() {
                    givenSmallParcel1, givenSmallParcel2, givenSmallParcel3,
                    givenSmallParcel4, givenSmallParcel5, givenSmallParcel6,
                    givenSmallParcel7, givenSmallParcel8, givenSmallParcel9,
                givenMediumParcel1, givenMediumParcel2, givenMediumParcel3,
                givenMediumParcel4, givenMediumParcel5, givenMediumParcel6,
                givenMediumParcel7}
            };

            //act
            parcelOrder.CalculateOrderDiscount();

            //assert
            Assert.That(parcelOrder.OrderDiscounts.Count, Is.EqualTo(4));
            Assert.That(parcelOrder.DiscountAmount, Is.EqualTo(24));
            Assert.That(parcelOrder.TotalCost, Is.EqualTo(91));
        }

        [Test]
        public void CalculateParcelOrderDiscount_GIVEN_NineSmall_AND_SevenMediumParcels_AND_FourOthers_THEN_ReturnCorrectCostWithDiscount()
        {
            //arrange
            var givenSmallParcel1 = _parcelService.NewParcel(1, 1, 1, 1); //$3, Expected Free (Small Size)
            var givenSmallParcel2 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenSmallParcel3 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenSmallParcel4 = _parcelService.NewParcel(1, 1, 1, 2); //$5
            var givenSmallParcel5 = _parcelService.NewParcel(1, 1, 1, 2); //$5, Expected Free (Small Size)
            var givenSmallParcel6 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenSmallParcel7 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenSmallParcel8 = _parcelService.NewParcel(1, 1, 1, 3); //$7, 
            var givenSmallParcel9 = _parcelService.NewParcel(1, 1, 1, 12); //$25,

            var givenMediumParcel1 = _parcelService.NewParcel(10, 10, 10, 3); //$8, Expected Free (Medium Size)
            var givenMediumParcel2 = _parcelService.NewParcel(10, 10, 10, 3); //$8
            var givenMediumParcel3 = _parcelService.NewParcel(10, 10, 10, 3); //$8
            var givenMediumParcel4 = _parcelService.NewParcel(10, 10, 10, 3); //$8, Expected Free (Medium Size)
            var givenMediumParcel5 = _parcelService.NewParcel(10, 10, 10, 3); //$8 
            var givenMediumParcel6 = _parcelService.NewParcel(10, 10, 10, 5); //$12 
            var givenMediumParcel7 = _parcelService.NewParcel(10, 10, 10, 5); //$12, Expected Free (MixedSize)

            var givenOtherParcel1 = _parcelService.NewParcel(50, 50, 50, 3); //$15, Large
            var givenOtherParcel2 = _parcelService.NewParcel(101, 10, 10, 3); //$25, XL
            var givenOtherParcel3 = _parcelService.NewParcel(101, 10, 10, 5); //$25, XL
            var givenOtherParcel4 = _parcelService.NewParcel(10, 10, 10, 51); //$51, Heavy

            var parcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() {
                    givenSmallParcel1, givenSmallParcel2, givenSmallParcel3,
                    givenSmallParcel4, givenSmallParcel5, givenSmallParcel6,
                    givenSmallParcel7, givenSmallParcel8, givenSmallParcel9,
                    givenMediumParcel1, givenMediumParcel2, givenMediumParcel3,
                    givenMediumParcel4, givenMediumParcel5, givenMediumParcel6,
                    givenMediumParcel7,
                    givenOtherParcel1, givenOtherParcel2, givenOtherParcel3,
                    givenOtherParcel4}
            };

            //act
            parcelOrder.CalculateOrderDiscount();

            //assert
            Assert.That(parcelOrder.OrderDiscounts.Count, Is.EqualTo(5));
            Assert.That(parcelOrder.DiscountAmount, Is.EqualTo(36));
            Assert.That(parcelOrder.TotalCost, Is.EqualTo(213));
        }

        [Test]
        public void CalculateParcelOrderDiscount_GIVEN_OneMediumParcels_AND_FourOthers_THEN_ReturnCorrectCostWithDiscount()
        {
            //arrange

            var givenMediumParcel1 = _parcelService.NewParcel(10, 10, 10, 3); //$8, Expected Free (Medium Size)

            var givenOtherParcel1 = _parcelService.NewParcel(50, 50, 50, 3); //$15, Large
            var givenOtherParcel2 = _parcelService.NewParcel(101, 10, 10, 3); //$25, XL
            var givenOtherParcel3 = _parcelService.NewParcel(101, 10, 10, 5); //$25, XL
            var givenOtherParcel4 = _parcelService.NewParcel(10, 10, 10, 51); //$51, Heavy

            var parcelOrder = new ParcelOrder()
            {
                Parcels = new List<Parcel>() {
                    givenMediumParcel1,
                    givenOtherParcel1, givenOtherParcel2, givenOtherParcel3,
                    givenOtherParcel4}
            };

            //act
            parcelOrder.CalculateOrderDiscount();

            //assert
            Assert.That(parcelOrder.OrderDiscounts.Count, Is.EqualTo(1));
            Assert.That(parcelOrder.DiscountAmount, Is.EqualTo(8));
            Assert.That(parcelOrder.TotalCost, Is.EqualTo(116));
        }
    }
}

