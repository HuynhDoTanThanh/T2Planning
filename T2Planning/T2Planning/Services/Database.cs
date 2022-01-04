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
                connection.CreateTable<Member>();
                connection.CreateTable<User>();


                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteDatabase()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");

                var connection = new SQLiteConnection(path);

                connection.DropTable<Card>();
                connection.DropTable<ListCard>();
                connection.DropTable<Table>();
                connection.DropTable<Member>();
                connection.DropTable<User>();


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
        public bool AddNewMember(Member member)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                connection.Insert(member);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddNewUser(User user)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                connection.Insert(user);

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
        public List<Table> GetTable()
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
        public List<User> GetUser()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                return connection.Table<User>().ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<Member> GetMember()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);

                return connection.Table<Member>().ToList();
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
                return connection.Query<Card>("select * from Card where listCardId=" + listCardId.ToString());
            }
            catch
            {
                return null;
            }
        }

        
        public Table GetTableWithQuery(int tableId)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);
                return connection.Query<Table>("select * from Table where tableId=" + tableId.ToString())[0];
            }
            catch
            {
                return null;
            }
        }
        public ListCard GetListCardWithQuery(int listCardId)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");
                var connection = new SQLiteConnection(path);
                return connection.Query<ListCard>("select * from ListCard where listCardId=" + listCardId.ToString())[0];
            }
            catch
            {
                return null;
            }
        }

        public void resetTable()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");

                var connection = new SQLiteConnection(path);

                connection.DeleteAll<Table>();
            }
            catch
            {

            }
        }
        public void resetUser()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");

                var connection = new SQLiteConnection(path);

                connection.DeleteAll<User>();
            }
            catch
            {

            }
        }
        public void resetMember()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");

                var connection = new SQLiteConnection(path);

                connection.DeleteAll<Member>();
            }
            catch
            {

            }
        }
        public void resetListCard()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");

                var connection = new SQLiteConnection(path);

                connection.DeleteAll<ListCard>();
            }
            catch
            {

            }
        }
        public void resetCard()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "t2planning.db");

                var connection = new SQLiteConnection(path);

                connection.DeleteAll<Card>();
            }
            catch
            {

            }
        }
    }
}
