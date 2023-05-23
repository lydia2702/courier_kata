using CourierParcels.Configurations;

namespace CourierParcels.Data
{
	public class ParcelOrder
	{
		private List<Parcel> parcels;
        private bool isSpeedyShipping;
        private List<OrderDiscount> orderDiscounts;
        private int parcelsCost;
        private int discountAmount;
        private int speedyShippingCost;
        private int parcelsCostAfterDiscounted;

        public ParcelOrder()
		{
			isSpeedyShipping = false;
			parcels = new List<Parcel>();
            orderDiscounts = new List<OrderDiscount>();
        }

        public List<Parcel> Parcels { get { return parcels; } set { parcels = value; } }
        public bool IsSpeedyShipping { get { return isSpeedyShipping; } set { isSpeedyShipping = value; } }
        public List<OrderDiscount> OrderDiscounts { get { return orderDiscounts; } set { orderDiscounts = value; } }

        public int ParcelsCost {
            get {
                parcelsCost = parcels.Sum(c => c.Cost);
                return parcelsCost;
            }
        }
        public int DiscountAmount {
            get {
                discountAmount = orderDiscounts.Sum(d => d.Amount);
                return discountAmount;
            }
        }
        public int ParcelsCostAfterDiscounted {
            get {
                parcelsCostAfterDiscounted = ParcelsCost - DiscountAmount;
                return parcelsCostAfterDiscounted;
            }
        }

        public int SpeedyShippingCost {
			get {
                speedyShippingCost = 0;
                if (isSpeedyShipping)
                    speedyShippingCost = ParcelsCostAfterDiscounted;
                return speedyShippingCost;
			}
		}

        public int TotalCost {
			get {
                return SpeedyShippingCost + ParcelsCostAfterDiscounted;
			}
		}

        public void CalculateOrderDiscount()
        {
            //Calculate SmallSizeDiscount
            var smallSizeParcels = parcels.Where(p => p.ParcelSizeType == ParcelSizeType.Small)
				.OrderBy(o=>o.Cost).ToList() ?? new List<Parcel>();

            var (smallDiscountBatches, smallBatchesLeft) = UpdateDiscount(ref smallSizeParcels, SmallParcelDiscount.ParcelFreeAt, SmallParcelDiscount.DiscountType);
            orderDiscounts.AddRange(smallDiscountBatches);
            

            //Calculate MediumSizeDiscount
            var mediumSizeParcels = parcels.Where(p => p.ParcelSizeType == ParcelSizeType.Medium)
                .OrderBy(o => o.Cost).ToList() ?? new List<Parcel>();

            var (mediumDiscountBatches, mediumBatchesLeft) = UpdateDiscount(ref mediumSizeParcels, MediumParcelDiscount.ParcelFreeAt, MediumParcelDiscount.DiscountType);
            orderDiscounts.AddRange(mediumDiscountBatches);

            //Calculate MixedSizeDiscount
            //Get non small and non medium size
            var mixedSizeParcel = parcels.Where(p => p.ParcelSizeType != ParcelSizeType.Small
            && p.ParcelSizeType != ParcelSizeType.Medium).ToList() ?? new List<Parcel>();

            //Take non processed parcel
            if (smallBatchesLeft > 0)
            {
                mixedSizeParcel.AddRange(smallSizeParcels.TakeLast(smallBatchesLeft));
            }

            if(mediumBatchesLeft > 0)
            {
                mixedSizeParcel.AddRange(mediumSizeParcels.TakeLast(mediumBatchesLeft));
            }

            mixedSizeParcel = mixedSizeParcel.OrderBy(p => p.Cost).ToList();

            var (mixedDiscountBatches, mixedBatchesLeft) = UpdateDiscount(ref mixedSizeParcel, MixedParcelDiscount.ParcelFreeAt, MixedParcelDiscount.DiscountType);
            orderDiscounts.AddRange(mixedDiscountBatches);

        }

        public static (List<OrderDiscount>, int) UpdateDiscount(ref List<Parcel> workingParcels, int parcelFreeAt, DiscountType discountType)
        {
            var workingOrderDiscounts = new List<OrderDiscount>();
            var discountBatchesLeft = workingParcels.Count % parcelFreeAt;
            if (workingParcels.Count >= parcelFreeAt)
            {
                var discountBatchesCounter = workingParcels.Count / parcelFreeAt;
                var discountBatchesd = workingParcels.Take(parcelFreeAt * discountBatchesCounter);

                var counter = 0;
                foreach (var parcel in discountBatchesd)
                {
                    if ((counter % parcelFreeAt) == 0)
                    {
                        workingOrderDiscounts.Add(new OrderDiscount { DiscountType = discountType, Amount = parcel.Cost });
                    }
                    counter++;
                }
            }

            return (workingOrderDiscounts, discountBatchesLeft);
        }
    }
}

