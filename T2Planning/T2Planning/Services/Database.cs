using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using T2Planning.Models;

namespace T2Planning.Services
{
    public class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDatabase()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");

                var connection = new SQLiteConnection(path);

                connection.CreateTable<Card>();
                connection.CreateTable<ListCard>();
                connection.CreateTable<Table>();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddNewCard(Card card)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                connection.Insert(card);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddNewListCard(ListCard listcard)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                connection.Insert(listcard);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddNewTable(Table table)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                connection.Insert(table);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Table> GetTableWork()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                return connection.Table<Table>().ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<ListCard> GetListCard()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                return connection.Table<ListCard>().ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<Card> GetCard()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                return connection.Table<Card>().ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<Card> GetCardWithQuery(int listCardId)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);
                return connection.Query<Card>("select * from Book where listCardId=" + listCardId.ToString());
            }
            catch
            {
                return null;
            }
        }
    }
}
