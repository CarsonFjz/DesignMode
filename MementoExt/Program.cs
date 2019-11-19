using System;
using System.Collections.Generic;

namespace MementoExt
{
    /// <summary>
    /// 文章备份实体（某个时刻文章数据状态）
    /// </summary>
    public class ArticleBackup
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public ArticleBackup(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }

    /// <summary>
    /// 文章操作管理者
    /// </summary>
    public class ArticleCaretaker
    {
        /// <summary>
        /// 当前版本号
        /// </summary>
        public int CurrentBatch { get; set; }
        public List<ArticleBackup> ArticleBackupList { get; set; } = new List<ArticleBackup>();

        /// <summary>
        /// 获取上一个版本
        /// </summary>
        /// <returns></returns>
        public ArticleBackup GetPre()
        {
            if (CurrentBatch > 1)
            {
                return ArticleBackupList[CurrentBatch - 2];
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// 获取指定版本
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        public ArticleBackup GetByBatch(int batch)
        {
            return ArticleBackupList[batch - 1];
        }

        /// <summary>
        /// 备份新版本
        /// </summary>
        /// <param name="articleBackup"></param>
        public void Set(ArticleBackup articleBackup)
        {
            ArticleBackupList.Add(articleBackup);
            CurrentBatch++;
        }
    }

    /// <summary>
    /// 文章实体
    /// </summary>
    public class Article
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Article(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public bool TryUpdate(string title, string content)
        {
            //code...
            Title = title;
            Content = content;
            return true;
        }

        public ArticleBackup Backup()
        {
            return new ArticleBackup(Title, Content);
        }

        public void RestoreMemento(ArticleBackup model)
        {
            Title = model.Title;
            Content = model.Content;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //版本1
            var article = new Article("title1", "content1");

            var messageModelCaretaker = new ArticleCaretaker();
            messageModelCaretaker.Set(article.Backup());

            article.TryUpdate("title2", "new content2");
            messageModelCaretaker.Set(article.Backup());
            article.TryUpdate("title3", "new content3");
            messageModelCaretaker.Set(article.Backup());
            article.TryUpdate("title4", "new content4");
            messageModelCaretaker.Set(article.Backup());

            Console.WriteLine($"{article.Title},{article.Content}");

            //回滚上一个版本数据
            article.RestoreMemento(messageModelCaretaker.GetPre());
            Console.WriteLine($"获取上一个版本数据{article.Title},{article.Content}");

            article.RestoreMemento(messageModelCaretaker.GetByBatch(1));
            Console.WriteLine($"获取首个版本数据{article.Title},{article.Content}");

            Console.ReadKey();
        }
    }
}
