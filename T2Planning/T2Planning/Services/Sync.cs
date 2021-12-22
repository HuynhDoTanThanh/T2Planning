using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Models;
using Xamarin.Forms;

namespace T2Planning.Services
{
    public class Sync
    {
        //==============================Table===========================//
        public async Task<int> PushTableAsync(Table table)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/AddTable";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(table);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                List<Dictionary<string, int>> tableId = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(result);

                return tableId[0]["tableId"];
            }
            catch
            {
                return 0;
            }
        }

        public async void PullTableAsync(string Uid)
        {
            try
            {
                Database database = new Database();
                HttpClient httpClient = new HttpClient();
                string url = "http://www.t2planning.somee.com/api/ServiceController/GetTable?Uid=";
                List<Table> tables = new List<Table>();
                var tableList = await httpClient.GetStringAsync(url + Uid);
                tables = JsonConvert.DeserializeObject<List<Table>>(tableList);

                List<Table> tables1 = database.GetTable();
                foreach (Table table in tables)
                {
                    bool containsItem = tables1.Any(item => item.tableId == table.tableId);
                    if (!containsItem)
                    {
                        database.AddNewTable(table);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public void PullTable(string Uid)
        {
            try
            {
                Database database = new Database();
                HttpClient httpClient = new HttpClient();
                string url = "http://www.t2planning.somee.com/api/ServiceController/GetTable?Uid=";
                List<Table> tables = new List<Table>();
                var tableList = Task.Run(() => httpClient.GetStringAsync(url + Uid)).Result;
                tables = JsonConvert.DeserializeObject<List<Table>>(tableList);

                List<Table> tables1 = database.GetTable();
                foreach (Table table in tables)
                {
                    bool containsItem = tables1.Any(item => item.tableId == table.tableId);
                    if (!containsItem)
                    {
                        database.AddNewTable(table);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        //==============================User===========================//

        public async void PushUserAsync(User user)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/AddUser";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
            }
            catch
            {
                return;
            }
        }

        public async void PullUserAsync(string Uid)
        {
            Database database = new Database();
            HttpClient httpClient = new HttpClient();
            var url = "http://www.t2planning.somee.com/api/ServiceController/GetUser?Uid=" + Uid;
            var userList = await httpClient.GetStringAsync(url);
            List<User> users = new List<User>();

            users = JsonConvert.DeserializeObject<List<User>>(userList);

            List<User> users1 = database.GetUser();
            if(users1 == null)
            {
                database.AddNewUser(users[0]);
            }
            else
            {
                foreach (User user in users)
                {
                    bool containsItem = users1.Any(item => item.Uid == user.Uid);
                    if (!containsItem)
                    {
                        database.AddNewUser(user);
                    }
                }
            }
        }

        public void PullUser(string Uid)
        {
            Database database = new Database();
            HttpClient httpClient = new HttpClient();
            var url = "http://www.t2planning.somee.com/api/ServiceController/GetUser?Uid=" + Uid;
            var userList = Task.Run(() => httpClient.GetStringAsync(url)).Result;
            List<User> users = new List<User>();

            users = JsonConvert.DeserializeObject<List<User>>(userList);

            List<User> users1 = database.GetUser();
            if (users1 == null)
            {
                database.AddNewUser(users[0]);
            }
            else
            {
                foreach (User user in users)
                {
                    bool containsItem = users1.Any(item => item.Uid == user.Uid);
                    if (!containsItem)
                    {
                        database.AddNewUser(user);
                    }
                }
            }
        }

        //==============================Member===========================//

        public async void PushMemberAsync(Member member)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/AddMember";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(member);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
            }
            catch
            {
                return;
            }
        }

        public async void PullMemberAsync(string Uid)
        {
            try
            {
                Database database = new Database();
                HttpClient httpClient = new HttpClient();
                string url = "http://www.t2planning.somee.com/api/ServiceController/GetMember?Uid=";

                var memberList = await httpClient.GetStringAsync(url + Uid);
                List<Member> members;

                members = JsonConvert.DeserializeObject<List<Member>>(memberList);

                List<Member> members1 = database.GetMember();
                foreach (Member member in members)
                {
                    bool containsItem = members1.Any(item => item.memberId == member.memberId);
                    if (!containsItem)
                    {
                        database.AddNewMember(member);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public void PullMember(string Uid)
        {
            try
            {
                Database database = new Database();
                HttpClient httpClient = new HttpClient();
                string url = "http://www.t2planning.somee.com/api/ServiceController/GetMember?Uid=";

                var memberList = Task.Run(() => httpClient.GetStringAsync(url + Uid)).Result;
                List<Member> members;

                members = JsonConvert.DeserializeObject<List<Member>>(memberList);

                List<Member> members1 = database.GetMember();
                foreach (Member member in members)
                {
                    bool containsItem = members1.Any(item => item.memberId == member.memberId);
                    if (!containsItem)
                    {
                        database.AddNewMember(member);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        //==============================ListCard===========================//

        public async void PushListCardAsync(ListCard listCard)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/AddListCard";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(listCard);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
            }
            catch
            {
                return;
            }
        }

        public void PushListCard(ListCard listCard)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/AddListCard";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(listCard);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
            }
            catch
            {
                return;
            }
        }

        public async void PullListCardAsync(string Uid)
        {
            try
            {
                Database database = new Database();
                HttpClient httpClient = new HttpClient();
                string url = "http://www.t2planning.somee.com/api/ServiceController/GetListCard?Uid=";

                var LCardList = await httpClient.GetStringAsync(url + Uid);
                List<ListCard> listCards;

                listCards = JsonConvert.DeserializeObject<List<ListCard>>(LCardList);

                List<ListCard> listCards1 = database.GetListCard();
                foreach (ListCard listCard in listCards)
                {
                    bool containsItem = listCards1.Any(item => item.listCardId == listCard.listCardId);
                    if (!containsItem)
                    {
                        database.AddNewListCard(listCard);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public void PullListCard(string Uid)
        {
            try
            {
                Database database = new Database();
                HttpClient httpClient = new HttpClient();
                string url = "http://www.t2planning.somee.com/api/ServiceController/GetListCard?Uid=";

                var LCardList = Task.Run(() => httpClient.GetStringAsync(url + Uid)).Result;
                List<ListCard> listCards;

                listCards = JsonConvert.DeserializeObject<List<ListCard>>(LCardList);

                List<ListCard> listCards1 = database.GetListCard();
                foreach (ListCard listCard in listCards)
                {
                    bool containsItem = listCards1.Any(item => item.listCardId == listCard.listCardId);
                    if (!containsItem)
                    {
                        database.AddNewListCard(listCard);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        //==============================Card===========================//

        public async void PushCardAsync(Card card)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/AddCard";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(card);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
            }
            catch
            {
                return;
            }
        }

        public void PushCard(Card card)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/AddCard";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(card);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
            }
            catch
            {
                return;
            }
        }

        public async void PullCardAsync(string Uid)
        {
            try
            {
                Database database = new Database();
                HttpClient httpClient = new HttpClient();
                string url = "http://www.t2planning.somee.com/api/ServiceController/GetCard?Uid=";

                var cardList = await httpClient.GetStringAsync(url + Uid);
                List<Card> cards;

                cards = JsonConvert.DeserializeObject<List<Card>>(cardList);

                List<Card> cards1 = database.GetCard();
                foreach (Card card in cards)
                {
                    bool containsItem = cards1.Any(item => item.cardId == card.cardId);
                    if (!containsItem)
                    {
                        database.AddNewCard(card);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public void PullCard(string Uid)
        {
            try
            {
                Database database = new Database();
                HttpClient httpClient = new HttpClient();
                string url = "http://www.t2planning.somee.com/api/ServiceController/GetCard?Uid=";

                var cardList = Task.Run(() => httpClient.GetStringAsync(url + Uid)).Result;
                List<Card> cards;

                cards = JsonConvert.DeserializeObject<List<Card>>(cardList);

                List<Card> cards1 = database.GetCard();
                foreach (Card card in cards)
                {
                    bool containsItem = cards1.Any(item => item.cardId == card.cardId);
                    if (!containsItem)
                    {
                        database.AddNewCard(card);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        //=====================Sync===========================//
        public void PullDB(string Uid)
        {
            try
            {
                PullUser(Uid);
                PullMemberAsync(Uid);
                PullTable(Uid);
                PullListCardAsync(Uid);
                PullCardAsync(Uid);
            }
            catch
            {
                return;
            }
        }
    }
}
