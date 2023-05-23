using System;
using CourierParcels.Configurations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourierParcels.Data
{
	public class Parcel
    {
        private ParcelSizeType parcelSizeType;
        private int cost;
        private List<string> errors;

        public Parcel()
        {
            cost = 0;
            Length = 0;
            Width = 0;
            Height = 0;
            errors = new List<string>();
        }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ParcelSizeType ParcelSizeType { get { return parcelSizeType; } set { parcelSizeType = value; } }
        public decimal Weight { get; set; }
        public int Cost { get {return cost; } set { cost = value; } }
        public List<string> Errors { get { return errors; } set { errors = value; } }
        public bool IsValid { get; set; }

        public void CalculateParcelSizeType()
        {
            if(this.Weight > HeavySize.WeightLimt)
            {
                parcelSizeType = HeavySize.ParcelSizeType;
            }
            else if (this.Length < SmallSize.MaxDimensions
               && this.Width < SmallSize.MaxDimensions
               && this.Height < SmallSize.MaxDimensions)
            {
                parcelSizeType = SmallSize.ParcelSizeType;
            }
            else if (this.Length < MediumSize.MaxDimensions
                && this.Width < MediumSize.MaxDimensions
                && this.Height < MediumSize.MaxDimensions)
            {
                parcelSizeType = MediumSize.ParcelSizeType;
            }
            else if (this.Length < LargeSize.MaxDimensions
                && this.Width < LargeSize.MaxDimensions
                && this.Height < LargeSize.MaxDimensions)
            {
                parcelSizeType = LargeSize.ParcelSizeType;
            }
            else
            {
                parcelSizeType = ParcelSizeType.XL;
            }
        }

        public void CalculateParcelCost()
        {
            switch (parcelSizeType)
            {
                case ParcelSizeType.Small:
                    {
                        if (Weight > SmallSize.WeightLimt)
                        {
                            var exceedWeight = Math.Ceiling(Weight) - SmallSize.WeightLimt;
                            cost = (int) (SmallSize.Cost + exceedWeight*ParcelSize.OverWeightCostPerKg);
                            break;
                        }

                        cost = SmallSize.Cost;
                        break;
                    }
                case ParcelSizeType.Medium:
                    {
                        if (Weight > MediumSize.WeightLimt)
                        {
                            var exceedWeight = Math.Ceiling(Weight) - MediumSize.WeightLimt;
                            cost = (int)(MediumSize.Cost + exceedWeight * ParcelSize.OverWeightCostPerKg);
                            break;
                        }

                        cost = MediumSize.Cost;
                        break;
                    }
                case ParcelSizeType.Large:
                    {
                        if (Weight > LargeSize.WeightLimt)
                        {
                            var exceedWeight = Math.Ceiling(Weight) - LargeSize.WeightLimt;
                            cost = (int)(LargeSize.Cost + exceedWeight * ParcelSize.OverWeightCostPerKg);
                            break;
                        }

                        cost = LargeSize.Cost;
                        break;
                    }
                case ParcelSizeType.XL:
                    {
                        if (Weight > XLSize.WeightLimt)
                        {
                            var exceedWeight = Math.Ceiling(Weight) - XLSize.WeightLimt;
                            cost = (int)(XLSize.Cost + exceedWeight * ParcelSize.OverWeightCostPerKg);
                            break;
                        }

                        cost = XLSize.Cost;
                        break;
                    }
                case ParcelSizeType.Heavy:
                    {
                        var overWeight = Math.Ceiling(Weight - HeavySize.WeightLimt);
                        cost = (int) (HeavySize.Cost + overWeight * ParcelSize.OverWeightCostForHeavyPerKg);
                        break;
                    }
            }
        }

        public void Validate()
        {
            try
            {
                if (Length <= 0)
                    Errors.Add("Length should be more than 0");

                if (Width <= 0)
                    Errors.Add("Width should be more than 0");

                if (Height <= 0)
                    Errors.Add("Height should be more than 0");

                if (Weight <= 0)
                    Errors.Add("Weight should be more than 0");

                if (Errors.Count > 0)
                    this.IsValid = false;
                else
                    this.IsValid = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}

