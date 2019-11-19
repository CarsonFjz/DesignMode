using System;

namespace Strategy
{
    /// <summary>
    /// 策略接口
    /// </summary>
    public interface IActivitiesStrategy
    {
        /// <summary>
        /// 获取则扣策略
        /// </summary>
        /// <returns></returns>
        double GetDiscountStrategy();
    }

    /// <summary>
    /// 策略上下文
    /// </summary>
    public class ActivitiesContext
    {
        private readonly IActivitiesStrategy _activitiesStrategy;
        public ActivitiesContext(IActivitiesStrategy activitiesStrategy)
        {
            _activitiesStrategy = activitiesStrategy;
        }

        public double GetActivitiesDiscount()
        {
            return _activitiesStrategy.GetDiscountStrategy();
        }
    }

    public class Activities
    {
        /// <summary>
        /// 折扣
        /// </summary>
        public double Discount { get; set; }
    }

    public class DoubleElevenActivities : IActivitiesStrategy
    {
        public double GetDiscountStrategy()
        {
            return 0.11;
        }
    }

    public class DoubleTwelveActivities : IActivitiesStrategy
    {
        public double GetDiscountStrategy()
        {
            return 0.12;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ActivitiesContext context = new ActivitiesContext(new DoubleElevenActivities());
            Console.WriteLine("Hello World!");
        }
    }
}
