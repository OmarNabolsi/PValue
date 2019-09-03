using System.Linq;

namespace PValue.Repository
{
    public class Util
    {
        public static double[] BuildArray(IQueryable<dynamic> data)
        {
            double[] arr = new double[data.Count()];
            int i = 0;

            foreach (var item in data)
            {
                arr[i] = item.value;
                i++;
            }

            return arr;
        }

        public static double[] BuildExpected(double[] observed)
        {
            double a, b, c, d, n;
            double physYes, physNo, peersYes, peersNo;

            a = observed[0] + observed[2];
            b = observed[1] + observed[3];
            c = observed[0] + observed[1];
            d = observed[2] + observed[3];
            
            n = observed.Sum();

            if(n != 0)
            {
                physYes = (a * c) / n;
                physNo = (b * c) / n;
                peersYes = (a * d) / n;
                peersNo = (b * d) / n;
                return new double[] { physYes, physNo, peersYes, peersNo };
            }

            return new double[] {};
        }
    }
}