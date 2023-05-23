using System;
using CourierParcels.Data;

namespace CourierParcels.Configurations
{
    public static class SmallParcelDiscount
    {
        public static readonly DiscountType DiscountType = DiscountType.Small;
        public static readonly int ParcelFreeAt = 4;
    }

    public static class MediumParcelDiscount
    {
        public static readonly DiscountType DiscountType = DiscountType.Medium;
        public static readonly int ParcelFreeAt = 3;
    }

    public static class MixedParcelDiscount
    {
        public static readonly DiscountType DiscountType = DiscountType.Mixed;
        public static readonly int ParcelFreeAt = 5;
    }
}

