using System;
using CourierParcels.Data;
using CourierParcels.Services;

namespace CourierParcels.Tests.Unit.Services
{
    public class ParcelServiceTests
    {
        IParcelService _parcelServiceSUT;

        [SetUp]
        public void Setup()
        {
            _parcelServiceSUT = new ParcelService();
        }

        [TestCase(1, 2, 3, 1, ParcelSizeType.Small, 3)]
        [TestCase(10, 10, 10,1, ParcelSizeType.Medium, 8)]
        [TestCase(80, 20, 30, 1, ParcelSizeType.Large, 15)]
        [TestCase(20, 150, 50, 1, ParcelSizeType.XL, 25)]
        [TestCase(100, 5, 5, 1, ParcelSizeType.XL, 25)]
        public void CalculateParcelSize_GIVEN_ThreeDimensions_AND_Weight_THEN_ShouldReturnCorrectSize(int givenLength, int givenWidth, int givenHeight 
            , int givenWeight, ParcelSizeType expectedParcelSizeType, int expectedCost)
        {
            //act
            var result = _parcelServiceSUT.NewParcel(givenLength, givenWidth, givenHeight, givenWeight);


            //assert
            Assert.That(expectedParcelSizeType, Is.EqualTo(result.ParcelSizeType));
            Assert.That(expectedCost, Is.EqualTo(result.Cost));
        }


        [TestCase(1, 2, 3, 0, false, "Weight should be more than 0")]
        [TestCase(1, 2, 0, 1, false, "Height should be more than 0")]
        [TestCase(1, 0, 3, 1, false, "Width should be more than 0")]
        [TestCase(0, 2, 3, 1, false, "Length should be more than 0")]
        public void Validator_GIVEN_ThreeDimensions_AND_WithoutWeight_THEN_ShouldReturnCorrectSize(int givenLength, int givenWidth, int givenHeight
            , int givenWeight, bool expectedIsValid, string expectedErrors)
        {
            //act
            var result = _parcelServiceSUT.NewParcel(givenLength, givenWidth, givenHeight, givenWeight);

            //assert
            Assert.That(expectedIsValid, Is.EqualTo(result.IsValid));
            Assert.That(expectedErrors, Is.EqualTo(result.Errors[0]));
        }
    }
}