using System;
using CourierParcels.Data;
using System.Runtime;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourierParcels.Services
{
    public interface IParcelService
    {
        Parcel NewParcel(int length, int width, int height, decimal weight);
    }

	public class ParcelService: IParcelService
    {
		public ParcelService()
		{
		}

        public Parcel NewParcel(int length, int width, int height, decimal weight)
        {
            var parcel = new Parcel
            {
                Length = length,
                Width = width,
                Height = height,
                Weight = weight
            };

            parcel.Validate();
            if (parcel.IsValid)
            { 
                parcel.CalculateParcelSizeType();
                parcel.CalculateParcelCost();
            }

            return parcel;
        }
    }
}

