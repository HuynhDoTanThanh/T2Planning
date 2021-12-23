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
                    database.AddNewTable(table);
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
                    database.AddNewTable(table);
                }
            }
            catch
            {
                return;
            }
        }

        public void UpdateTable(Table table)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/UpdateTable";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(table);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
            }
            catch
            {
                return;
            }
        }

        public void DeleteTable(int tableId)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/DeleteTable";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(new { tableId = tableId });
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
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
                    database.AddNewUser(user);
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
                    database.AddNewUser(user);
                }
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/UpdateUser";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
            }
            catch
            {
                return;
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
                    database.AddNewMember(member);
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
                    database.AddNewMember(member);
                }
            }
            catch
            {
                return;
            }
        }

        public void DeleteMember(int memberId)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/DeleteMember";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(new { memberId = memberId });
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
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
                    database.AddNewListCard(listCard);
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
                    database.AddNewListCard(listCard);
                }
            }
            catch
            {
                return;
            }
        }

        public void UpdateListCard(ListCard listCard)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/UpdateListCard";
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

        public void DeleteListCard(int listCardId)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/DeleteListCard";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(new { listCardId = listCardId });
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
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
                    database.AddNewCard(card);
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
                    database.AddNewCard(card);
                }
            }
            catch
            {
                return;
            }
        }

        public void UpdateCard(Card card)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/UpdateListCard";
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

        public void DeleteCard(int cardId)
        {
            try
            {
                string url = "http://www.t2planning.somee.com/api/ServiceController/DeleteListCard";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(new { cardId = cardId });
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
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
