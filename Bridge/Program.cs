using System;

namespace Bridge
{
    #region 发布消息抽象模型
    /// <summary>
    /// 消息实体
    /// </summary>
    public class Message
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// 发布消息抽象类
    /// </summary>
    public abstract class AbstractPublisher
    {
        public abstract void Send(Message objMessage);
    }

    /// <summary>
    /// 发布消息类
    /// </summary>
    public class Publisher
    {
        public AbstractPublisher AbstractPublisher { get; set; }

        public virtual void Send(Message objMessage)
        {
            AbstractPublisher.Send(objMessage);
        }
    }
    #endregion

    #region 发布消息具体实现类
    /// <summary>
    /// RabbitMq发布消息类
    /// </summary>
    public class RabbitMqPublisher : AbstractPublisher
    {
        public override void Send(Message objMessage)
        {
            Console.WriteLine("RabbitMq发布消息");
        }
    }

    /// <summary>
    /// Kafka发布消息类
    /// </summary>
    public class KafkaPublisher : AbstractPublisher
    {
        public override void Send(Message objMessage)
        {
            Console.WriteLine("Kafka发布消息");
        }
    } 
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Publisher publisher = new Publisher();

            publisher.AbstractPublisher = new RabbitMqPublisher();

            publisher.Send(new Message());

            publisher.AbstractPublisher = new KafkaPublisher();

            publisher.Send(new Message());

            Console.ReadKey();
        }
    }
}
