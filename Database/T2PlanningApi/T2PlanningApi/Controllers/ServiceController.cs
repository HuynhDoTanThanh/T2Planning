using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using T2PlanningApi.Models;

namespace T2PlanningApi.Controllers
{
    public class ServiceController : ApiController
    {
        //======================== Table============================//
        [Route("api/ServiceController/GetTable")]
        [HttpGet]
        public IHttpActionResult GetTable(string Uid)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Uid", Uid);

                DataTable result = Database.Database.ReadTable("GetTable", param);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/ServiceController/AddTable")]
        [HttpPost]
        public IHttpActionResult AddTable(Table table)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tableAdmin", table.tableAdmin);
            param.Add("tableName", table.tableName);
            param.Add("tableTeam", table.tableTeam);
            param.Add("tablePermission", table.tablePermission);

            DataTable result = Database.Database.WriteTb("InsertTable", param);

            return Ok(result);
        }

        [Route("api/ServiceController/UpdateTable")]
        [HttpPost]
        public Response UpdateTable(Table table)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tableId", table.tableId);
            param.Add("tableName", table.tableName);

            response = Database.Database.UpdateTable("UpdateTable", param);

            return response;
        }

        [Route("api/ServiceController/DeleteTable")]
        [HttpPost]
        public Response DeleteTable(Table table)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tableId", table.tableId);

            response = Database.Database.DeleteTable("DeleteTable", param);

            return response;
        }

        //========================User============================//
        [Route("api/ServiceController/GetUser")]
        [HttpGet]
        public IHttpActionResult GetUser(string Uid)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Uid", Uid);

                DataTable result = Database.Database.ReadTable("GetUser", param);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/ServiceController/AddUser")]
        [HttpPost]
        public Response AddUser(User user)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("Uid", user.Uid);
            param.Add("userName", user.userName);
            param.Add("userEmail", user.userEmail);
            param.Add("userAvatar", user.userAvatar);

            response = Database.Database.WriteTable("InsertUser", param);

            return response;
        }

        [Route("api/ServiceController/UpdateUser")]
        [HttpPost]
        public Response UpdateTable(User user)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("Uid", user.Uid);
            param.Add("userName", user.userName);
            param.Add("userAvatar", user.userAvatar);

            response = Database.Database.UpdateTable("UpdateUser", param);

            return response;
        }

        //========================Member============================//
        [Route("api/ServiceController/GetMember")]
        [HttpGet]
        public IHttpActionResult GetMember(string Uid)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Uid", Uid);

                DataTable result = Database.Database.ReadTable("GetMember", param);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/ServiceController/GetTableMember")]
        [HttpGet]
        public IHttpActionResult GetTableMember(int tableId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("tableId", tableId);

                DataTable result = Database.Database.ReadTable("GetTableMember", param);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/ServiceController/AddMember")]
        [HttpPost]
        public Response AddMember(Member member)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tableId", member.tableId);
            param.Add("Uid", member.Uid);

            response = Database.Database.WriteTable("InsertMember", param);

            return response;
        }

        [Route("api/ServiceController/DeleteMember")]
        [HttpPost]
        public Response DeleteMember(Member member)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tableId", member.tableId);
            param.Add("Uid", member.Uid);

            response = Database.Database.DeleteTable("DeleteMember", param);

            return response;
        }


        //========================ListCard============================//
        [Route("api/ServiceController/GetListCard")]
        [HttpGet]
        public IHttpActionResult GetListCard(string Uid)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Uid", Uid);

                DataTable result = Database.Database.ReadTable("GetListCard", param);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/ServiceController/AddListCard")]
        [HttpPost]
        public Response AddListCard(ListCard listCard)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("listCardName", listCard.listCardName);
            param.Add("tableId", listCard.tableId);

            response = Database.Database.WriteTable("InsertListCard", param);

            return response;
        }

        [Route("api/ServiceController/UpdateListCard")]
        [HttpPost]
        public Response UpdateListCard(ListCard listCard)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("listCardId", listCard.listCardId);
            param.Add("listCardName", listCard.listCardName);

            response = Database.Database.UpdateTable("UpdateListCard", param);

            return response;
        }

        [Route("api/ServiceController/DeleteListCard")]
        [HttpPost]
        public Response DeleteListCard(ListCard listCard)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("listCardId", listCard.listCardId);

            response = Database.Database.DeleteTable("DeleteListCard", param);

            return response;
        }

        //========================Card============================//
        [Route("api/ServiceController/GetCard")]
        [HttpGet]
        public IHttpActionResult GetCard(string Uid)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Uid", Uid);

                DataTable result = Database.Database.ReadTable("GetCard", param);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/ServiceController/AddCard")]
        [HttpPost]
        public Response AddCard(Card card)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tableId", card.tableId);
            param.Add("listCardId", card.listCardId);
            param.Add("cardName", card.cardName);
            param.Add("cardDescription", card.cardDescription);
            param.Add("cardDeadline", card.cardDeadline);

            response = Database.Database.WriteTable("InsertCard", param);

            return response;
        }

        [Route("api/ServiceController/UpdateCard")]
        [HttpPost]
        public Response UpdateCard(Card card)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("cardId", card.cardId);
            param.Add("cardName", card.cardName);
            param.Add("cardDescription", card.cardDescription);
            param.Add("cardDeadline", card.cardDeadline);

            response = Database.Database.UpdateTable("UpdateCard", param);

            return response;
        }

        [Route("api/ServiceController/DeleteCard")]
        [HttpPost]
        public Response DeleteCard(Card card)
        {
            Response response = new Response();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("cardId", card.cardId);

            response = Database.Database.DeleteTable("DeleteCard", param);

            return response;
        }
    }
}