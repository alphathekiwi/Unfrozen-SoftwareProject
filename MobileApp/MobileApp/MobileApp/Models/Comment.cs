using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Comment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int Author { get; set; }
        public string Content { get; set; }
        public DateTime Posted { get; set; }
        public Comment() { }
        public Comment(int author, int issueId, string content)
        {
            Author = author;
            IssueId = issueId;
            Content = content;
            Posted = DateTime.Now;
        }
    }
}
