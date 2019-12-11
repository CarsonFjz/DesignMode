using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWork
{
    /// <summary>
    /// 数据实体
    /// </summary>
    public class Model
    {
        public static Model CreateNew(string myKey)
        {
            return new Model()
            {
                Key = myKey
            };
        }
        public string Key { get; set; }
    }
    /// <summary>
    /// 仓储类
    /// </summary>
    public class Repository
    {
        public string Add(Model model)
        {
            Console.WriteLine(model.Key);
            return "add command";
        }

        public string Update(Model model)
        {
            Console.WriteLine(model.Key);
            return "update command";
        }

        public string Delete(Model model)
        {
            Console.WriteLine(model.Key);
            return "delete command";
        }
    }
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork
    {
        public List<Func<string, string>> Commands { get; set; } = new List<Func<string, string>>();

        public void AddCommand(Func<string, string> repositoryFunc)
        {
            Commands.Add(repositoryFunc);
        }

        public void Save()
        {
            var publicKey = "PublicKey-";
            //执行委托集合命令
            var commandList = Commands.Select(c => c(publicKey)).ToList();

            //打包redis命令然后提交
            //code...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork uow = new UnitOfWork();

            Repository repository = new Repository();

            uow.AddCommand((publicKey) =>
            {
                var model = Model.CreateNew(publicKey + "AddKey");
                return repository.Add(model);
            });
            uow.AddCommand((publicKey) =>
            {
                var model = Model.CreateNew(publicKey + "UpdateKey");
                return repository.Update(model);
            });
            uow.AddCommand((publicKey) =>
            {
                var model = Model.CreateNew(publicKey + "DeleteKey");
                return repository.Delete(model);
            });

            Console.WriteLine("Hello World!");

            //到这里才把所有语句执行
            uow.Save();

            Console.ReadKey();
        }
    }
}
