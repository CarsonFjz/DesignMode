using System;

namespace Strategy
{
    #region 策略上下文
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

    /// <summary>
    /// 优惠类
    /// </summary>
    public class Activities
    {
        /// <summary>
        /// 折扣
        /// </summary>
        public double Discount { get; set; }
    }
    #endregion

    #region 具体实现策略类
    /// <summary>
    /// 双十一优惠
    /// </summary>
    public class DoubleElevenActivities : IActivitiesStrategy
    {
        public double GetDiscountStrategy()
        {
            return 0.11;
        }
    }

    /// <summary>
    /// 双十二优惠
    /// </summary>
    public class DoubleTwelveActivities : IActivitiesStrategy
    {
        public double GetDiscountStrategy()
        {
            return 0.12;
        }
    } 
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            ActivitiesContext context = new ActivitiesContext(new DoubleElevenActivities());
            Console.WriteLine("Hello World!");
        }
    }
}
