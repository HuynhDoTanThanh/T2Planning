﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Planning.Models;
using T2Planning.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2Planning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardDetail : ContentPage
    {
        Database database = new Database();
        List<Table> tables;
        List<ListCard> listCards;
        Card card;
        string tableChoosed = "";
        string listChoosed = "";
        int? tableId;
        int listCardId;
        DateTime cardDeadline;
        List<ListCard> listCardschoosed;
        public CardDetail()
        {
            InitializeComponent();
        }
        public CardDetail(Card cardTake)
        {
            InitializeComponent();
            card = cardTake;
            listCardId = 0;
            init();
            //listchoose.SelectedIndex = 0;
            cardName_entry.Text = card.cardName;
            cardDescription_entry.Text = card.cardDescription;
            deadlineDay.Date = card.cardDeadline.Date;
            deadlineTime.Time = card.cardDeadline.TimeOfDay;

        }
        void init()
        {
            tables = database.GetTable();
            listCards = database.GetListCard();
            
            List<string> tablename = new List<string>();
            foreach (Table table in tables)
            {
                tablename.Add(table.tableName);
            }

            tablechoose.ItemsSource = tablename;
            tablechoose.SelectedIndex = card.tableId;

            listCardschoosed = new List<ListCard>();
            foreach (ListCard listCard in listCards)
            {
                if (listCard.tableId == card.tableId)
                {
                    listCardschoosed.Add(listCard);
                }
            }
            

            List<string> listname = new List<string>();
            if (listCardschoosed != null)
            {
                foreach (ListCard listCard in listCardschoosed)
                {
                    listname.Add(listCard.listCardName);
                }
                listchoose.ItemsSource = listname;

                int index = 0;
                foreach (ListCard listcard in listCardschoosed)
                {
                    if (card.listCardId == listcard.listCardId)
                    {

                        break;
                    }
                    index++;
                }
                listchoose.SelectedIndex = index;
            }
            
        }

        private void tablechoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                tableChoosed = (string)picker.ItemsSource[selectedIndex];
            }

            foreach (Table table in tables)
            {
                if (table.tableName == tableChoosed)
                {
                    tableId = table.tableId;
                    break;
                }
            }

            List<ListCard> listCardschoosed = new List<ListCard>();
            foreach (ListCard listCard in listCards)
            {
                if (listCard.tableId == tableId)
                {
                    listCardschoosed.Add(listCard);
                }
            }

            List<string> listname = new List<string>();
            if (listCardschoosed != null)
            {
                foreach (ListCard listCard in listCardschoosed)
                {
                    listname.Add(listCard.listCardName);
                }
                listchoose.ItemsSource = listname;

            }
        }

        private void listchoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                listChoosed = (string)picker.ItemsSource[selectedIndex];
            }
            foreach (ListCard listCard in listCards)
            {
                if ((listCard.listCardName == listChoosed) && (listCard.tableId == tableId))
                {
                    listCardId = listCard.listCardId;
                    break;
                }
            }
        }

    }
}