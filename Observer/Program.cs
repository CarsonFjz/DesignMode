using System;
using System.Collections.Generic;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                
                StatusChangedSubscribe statusChangedSubscribe = new StatusChangedSubscribe();
                Note1 note1 = new Note1();
                Note2 note2 = new Note2();
                statusChangedSubscribe.OnStatusChanged += note1.OnStatusChanged;
                statusChangedSubscribe.OnStatusChanged += note2.OnStatusChanged;

                string inputKey = Console.ReadLine();
                statusChangedSubscribe.CurrentStatus = int.Parse(inputKey);
            }
        }
    }

    public class Note1 : IStatusChanged
    {
        public int Status => 1;

        public void OnStatusChanged(int newStatus)
        {
            if (newStatus == Status)
            {
                Console.WriteLine("status 1");
            }
        }
    }

    public class Note2 : IStatusChanged
    {
        public int Status => 2;

        public void OnStatusChanged(int newStatus)
        {
            if (newStatus == Status)
            {
                Console.WriteLine("status 2");
            }
        }
    }

    public class StatusChangedSubscribe
    {
        private int _currentStatus;
        public int CurrentStatus
        {
            get { return _currentStatus; }
            set
            {
                //检查值是否发生变更
                if (value != CurrentStatus)
                {
                    _currentStatus = value;

                    //非空就全部执行
                    //OnStatusChanged?.Invoke(value);

                    //因为线性得处理任务，中途出错会影响后面的执行，所以必要时候需要进行错误处理
                    Action<int> handlers = OnStatusChanged;

                    if (handlers != null)
                    {
                        //把错误收集，最好自定义错误，带上出现错误的订阅者的名字
                        List<Exception> exCollection = new List<Exception>();

                        foreach (Action<int> handler in handlers.GetInvocationList())
                        {
                            try
                            {
                                handler(value);
                            }
                            catch (Exception e)
                            {
                                exCollection.Add(e);
                            }
                        }

                        //抛出组合异常
                        if (exCollection.Count > 0)
                        {
                            throw new AggregateException("StatusChangedSubscribe error", exCollection);
                        }
                    }
                }
            }
        }

        public Action<int> OnStatusChanged { get; set; }
    }
}
