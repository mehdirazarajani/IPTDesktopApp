using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class Form1 : Form
    {
        List<FYPSearchModel> results = new List();
        public Form1()
        {
            InitializeComponent();
            searchField.Text = "Enter text here...";
            searchField.ForeColor = Color.SlateGray;
        }

        private void searchField_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchField.Text))
            {
                searchField.Text = "Enter text here...";
                searchField.ForeColor = Color.SlateGray;
            }

        }

        private void searchField_Enter(object sender, EventArgs e)
        {
            if (searchField.Text == "Enter text here...")
            {
                searchField.Text = "";
                searchField.ForeColor = Color.FromArgb(0, 0, 192);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            MessageBox.Show(results[selectedRow].description, results[selectedRow].title);
        }

        private void populateGridView()
        {
            dataGridView1.Rows.Clear();
            if(results.Count == 0)
            {
                MessageBox.Show("No Results Found", "ERROR");
                return;
            }
            foreach(var _data in results)
            {
                dataGridView1.Rows.Add(_data.supervisor, _data.member1, _data.member2, _data.member3, _data.title);
            }
        }

        private void search_Click(object sender, EventArgs e)
        {
            if (search.Text == "" || searchField.Text == "Enter text here...")
            {
                MessageBox.Show("Search Field Can't be Entry", "ERROR");
                return;
            }

            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];

            var client = new RestClient( baseUrl + "/api/SearchFYP/GetSearchResult");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"query\" : \"" + searchField.Text + "\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                if (response.ErrorMessage != null) 
                {
                    MessageBox.Show(response.ErrorMessage, "ERROR");
                }
                else
                {
                    MessageBox.Show(JsonConvert.DeserializeObject<string>(response.Content), "ERROR");
                }
                return;
            }
            results = JsonConvert.DeserializeObject<List<FYPSearchModel>>(response.Content);
            populateGridView();
        }
    }
}
