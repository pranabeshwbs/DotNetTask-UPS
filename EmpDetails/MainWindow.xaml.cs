using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace EmpDetails
{
    public class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      private void GetData() // For GET data into the GridView. 
    {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://gorest.co.in");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("public-api/users").Result;
                
                if (response.IsSuccessStatusCode)
                {
                 var result2 = response.Content.ReadAsAsync<DataModel>().Result;
                 datagrid.ItemsSource = result2.data;
                }
                else
                {
                    MessageBox.Show("Error" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
    }
        public MainWindow()
        {

            InitializeComponent();
            GetData();
        }
        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://gorest.co.in/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var employee = new Datum();
            employee.id = int.Parse(id.Text);
            employee.name = name.Text;
            employee.email = email.Text;
            employee.gender = gender.Text;
            employee.status = status.Text;

            Test Obj = new Test();
            int res = Obj.InsertTest(employee.id);
            if (res == 0)
            {
                var response = client.PostAsJsonAsync("public-api/users", employee).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Employee Added");
                    id.Text = "";
                    name.Text = "";
                    email.Text = "";
                    gender.Text = "";
                    
                }
                else
                {
                    MessageBox.Show("Error" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
            else
            {
                MessageBox.Show("This Data has been already added");
            } 
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://gorest.co.in/");
            var EmpID = int.Parse(id.Text);
            var url = "public-api/users" + EmpID;
            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Employee deleted");
                GetData();
            }
            else
            {
                MessageBox.Show("Error" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
    }
}
