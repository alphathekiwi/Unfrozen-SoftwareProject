using SQLite;
using System;
using System.Collections.Generic;

namespace MobileApp.Models
{
    public class Issue
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Posted { get; set; }
        public string UserFeedback { get; set; }

        public Issue()
        {
            Posted = DateTime.Now;
        }
        public Issue(int Author, string Title, string Content)
        {
            this.Author = Author;
            this.Title = Title;
            this.Content = Content;
            Posted = DateTime.Now;
        }
        public int TotalLikes()
        {
            int[] ints = Decompress(UserFeedback);
            return ints != null ? ints.Length : 0;
        }
        public bool HasLiked(int user)
        {
            int[] ints = Decompress(UserFeedback);
            if (ints != null)
                foreach (int i in ints)
                    if (i == user) return true;
            return false;
        }
        public void ManageFeedback(int user, bool like)
        {
            List<int> feedback = new List<int>(Decompress(UserFeedback));
            if (like && user != 0)
                feedback.Add(user);
            else
                feedback.Remove(user);
            UserFeedback = Compress(feedback.ToArray());
        }
        private int[] Decompress(string s)
        {
            if (s == null || s == "")
                return new int[] { };
            byte[] bytes = Convert.FromBase64String(s);
            int[] ints = new int[bytes.Length / sizeof(int)];
            for (int i = 0; i < ints.Length; i++)
                ints[i] = BitConverter.ToInt32(bytes, i * sizeof(int));
            return ints;
        }
        private string Compress(int[] ints)
        {
            byte[] bytes = new byte[ints.Length * sizeof(int)];
            for (int i = 0; i < ints.Length; i++)
                BitConverter.GetBytes(ints[i]).CopyTo(bytes, i * sizeof(int));
            return Convert.ToBase64String(bytes);
        }
    }
}
