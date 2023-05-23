using System;
using CourierParcels.Data;
namespace CourierParcels.Configurations
{
    /*
	● Small parcel: all dimensions < 10cm. Cost $3
	● Medium parcel: all dimensions < 50cm. Cost $8
	● Large parcel: all dimensions < 100cm. Cost $15
	● XL parcel: any dimension >= 100cm. Cost $25

    Weight limit
    ● Small parcel: 1kg
    ● Medium parcel: 3kg
    ● Large parcel: 6kg
    ● XL parcel: 10kg
	*/

    public static class ParcelSize
    {
        public static readonly int OverWeightCostPerKg = 2;
        public static readonly int OverWeightCostForHeavyPerKg = 1;
    }

    public static class SmallSize
    {
        public static readonly ParcelSizeType ParcelSizeType = ParcelSizeType.Small;
        public static readonly int MaxDimensions = 10;
        public static readonly int Cost = 3;
        public static readonly decimal WeightLimt = 1;
    }

    public static class MediumSize
    {
        public static readonly ParcelSizeType ParcelSizeType = ParcelSizeType.Medium;
        public static readonly int MaxDimensions = 50;
        public static readonly int Cost = 8;
        public static readonly decimal WeightLimt = 3;
    }

    public static class LargeSize 
    {
        public static readonly ParcelSizeType ParcelSizeType = ParcelSizeType.Large;
        public static readonly int MaxDimensions = 100;
        public static readonly int Cost = 15;
        public static readonly decimal WeightLimt = 6;
    }

    public static class XLSize
    {
        public static readonly ParcelSizeType ParcelSizeType = ParcelSizeType.XL;
        public static readonly int Cost = 25;
        public static readonly decimal WeightLimt = 10;
    }

    public static class HeavySize
    {
        public static readonly ParcelSizeType ParcelSizeType = ParcelSizeType.Heavy;
        public static readonly int Cost = 50;
        public static readonly decimal WeightLimt = 50;
    }
}

