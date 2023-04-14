using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace EmpDetails
{
    public class Test
    {
        public int InsertTest(int id) // Test Case - For Duplicate checking.
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://gorest.co.in");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("public-api/users").Result;

            if (response.IsSuccessStatusCode)
            {
                var result2 = response.Content.ReadAsAsync<DataModel>().Result;
                var datas = result2.data.ToList();
                var count = datas.Where(x => x.id == id).Count();
                if(count > 0)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
