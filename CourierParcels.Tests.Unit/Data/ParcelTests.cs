using System;
using CourierParcels.Data;

namespace CourierParcels.Tests.Unit.Data
{
	public class ParcelTests
    {

        [TestCase(1, 2, 3, 1, ParcelSizeType.Small)]
        [TestCase(10, 10, 10, 1, ParcelSizeType.Medium)]
        [TestCase(80, 20, 30, 1, ParcelSizeType.Large)]
        [TestCase(20, 150, 50, 1, ParcelSizeType.XL)]
        [TestCase(100, 5, 5, 1, ParcelSizeType.XL)]
        [TestCase(1, 5, 5, 50, ParcelSizeType.Small)]    //Small, Weight=50
        [TestCase(1, 5, 5, 55, ParcelSizeType.Heavy)]    //Heavy, Weight=55
        public void CalculateParcelSizeType_GIVEN_ThreeDimensions_THEN_ShouldReturnCorrectSizeType(int givenLength, int givenWidth, int givenHeight
            , decimal givenWeight, ParcelSizeType expectedParcelSizeType)
        {
            //arrange
            var givenParcel = new Parcel()
            {
                Length = givenLength,
                Width = givenWidth,
                Height = givenHeight,
                Weight = givenWeight 
            };

            //act
            givenParcel.CalculateParcelSizeType();

            //assert
            Assert.That(givenParcel.ParcelSizeType, Is.EqualTo(expectedParcelSizeType));
        }

        [TestCase(1, 2, 3, 1, 3)]       //Small, Weight=1, NoExceedCost
        [TestCase(10, 10, 10, 1, 8)]    //Medium, Weight=1, NoExceedCost
        [TestCase(80, 20, 30, 1, 15)]   //Large, Weight=1, NoExceedCost
        [TestCase(20, 150, 50, 1, 25)]  //XL, Weight=1, NoExceedCost
        [TestCase(100, 5, 5, 1, 25)]    //XL, Weight=10, NoExceedCost
        [TestCase(1, 2, 3, 3, 7)]       //Small, Weight=3, Exceed 2kg
        [TestCase(10, 10, 10, 6, 14)]    //Medium, Weight=6, Exceed 3kg
        [TestCase(80, 20, 30, 10, 23)]   //Large, Weight=10, Exceed 4kg
        [TestCase(20, 150, 50, 11, 27)]  //XL, Weight=11, Exceed 1kg
        [TestCase(100, 5, 5, 15, 35)]    //XL, Weight=15, Exceed 5kg
        [TestCase(1, 5, 5, 50, 101)]    //Small, Weight=50, Exceed 49
        [TestCase(1, 5, 5, 55, 55)]    //Heavy, Weight=55
        public void CalculateParcelCost_GIVEN_ParcelSizeType_THEN_ReturnCorrectCost(int givenLength, int givenWidth, int givenHeight
            , decimal givenWeight, int expectedCost)
        {
            //arrange
            var givenParcel = new Parcel()
            {
                Length = givenLength,
                Width = givenWidth,
                Height = givenHeight,
                Weight = givenWeight
            };

            //act
            givenParcel.CalculateParcelSizeType();
            givenParcel.CalculateParcelCost();

            //assert
            Assert.That(givenParcel.Cost, Is.EqualTo(expectedCost));
        }
    }
}

