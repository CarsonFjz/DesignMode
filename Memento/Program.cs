using System;

namespace Memento
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
        public ArticleBackup ArticleBackup { get; set; }
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
            var article = new Article("title", "content");

            //备份
            var messageModelCaretaker = new ArticleCaretaker
            {
                ArticleBackup = article.Backup()
            };

            //更新数据
            article.TryUpdate("new title", "new content");

            Console.WriteLine($"{article.Title},{article.Content}");

            //回滚数据
            article.RestoreMemento(messageModelCaretaker.ArticleBackup);

            Console.WriteLine($"{article.Title},{article.Content}");

            Console.ReadKey();
        }
    }
}
