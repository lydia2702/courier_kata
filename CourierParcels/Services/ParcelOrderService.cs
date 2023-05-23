using System;
using System.Text;
using CourierParcels.Data;

namespace CourierParcels.Services
{
	public interface IParcelOrderService
	{
		string PrintParcelOrderSummary(ParcelOrder parcelOrder);

    }

    public class ParcelOrderService: IParcelOrderService
    {
		public ParcelOrderService()
		{
		}

        public string PrintParcelOrderSummary(ParcelOrder parcelOrder)
		{
            //Print Small Parcel: $3. Total Cost: $3

            var sbParcelOrder = new StringBuilder();

            //Print Parcel Items
            if(parcelOrder.Parcels.Count == 0)
            {
                return "No Parcels found!";
            }

            if(parcelOrder.Parcels.Where(e=>e.IsValid == false).Any())
            {
                parcelOrder.Parcels.SelectMany(e => e.Errors).ToList().ForEach(f=> sbParcelOrder.AppendLine($"{f}"));

                return sbParcelOrder.ToString();
            }

            parcelOrder.Parcels.ForEach(s => sbParcelOrder.AppendLine(string.Format($"{s.ParcelSizeType} Parcel ({s.Weight}KG): ${s.Cost}.")));

            //Print Discounts
            parcelOrder.OrderDiscounts.ForEach(d=> sbParcelOrder.AppendLine(string.Format($"Discount offer ({d.DiscountType} Type): $-{d.Amount}.")));


            //Print SpeedyShipping
            if (parcelOrder.IsSpeedyShipping)
                sbParcelOrder.AppendLine(string.Format($"Speedy shipping: ${parcelOrder.SpeedyShippingCost}."));

            //Print Total Cost
            sbParcelOrder.AppendLine(string.Format($"Total Cost: ${parcelOrder.TotalCost}."));

            return sbParcelOrder.ToString();


        }
	}
}

