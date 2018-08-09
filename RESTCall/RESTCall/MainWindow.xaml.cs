using RestSharp;
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

namespace RESTCall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        class StringKeyValuePair
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        class Campaign
        {
            public string cif { get; set; }
            public string productName { get; set; }
            public string result { get; set; }
            public string reason { get; set; }
            public string id { get; set; }
            public string code { get; set; }
            public string treatmentId { get; set; }
            public List<StringKeyValuePair> personalizationFields { get; set; }

        }
        class Campaigns
        {
            public List<Campaign> campaigns { get; set; }
        }

        private void btnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan ds = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));

            //RestClient client = new RestClient("http://marketing-campaigns.getsandbox.com/marketing/v1");
            RestClient client = new RestClient(txtBaseUrl.Text);

            RestRequest request = new RestRequest("/campaigns", Method.GET);
            request.AddHeader("Authorization", "Bear token1234");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Financial-Id", "EBI");
            request.AddHeader("Client-Timestamp", Convert.ToInt64(ds.TotalMilliseconds).ToString());
            request.AddHeader("Accept-Language", "en");
            request.AddHeader("Unique-Reference-Code", Guid.NewGuid().ToString());
            request.AddHeader("Client-Ip", "127.0.0.1");
            request.AddHeader("Channel-Id", "BNK");

            request.AddParameter("cif", "1234567890");
            request.AddParameter("productName", "PERSONAL LOAN");

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            txtResponse.Text = content;
        }

        private void btnCampaignID_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan ds = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            //RestClient client = new RestClient("http://marketing-campaigns.getsandbox.com");
            RestClient client = new RestClient(txtBaseUrl.Text);

            RestRequest request = new RestRequest("/campaigns/{id}", Method.GET);
            request.AddHeader("Authorization", "Bear token1234");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Financial-Id", "EBI");
            request.AddHeader("Client-Timestamp", Convert.ToInt64(ds.TotalMilliseconds).ToString());
            request.AddHeader("Accept-Language", "en");
            request.AddHeader("Unique-Reference-Code", Guid.NewGuid().ToString());
            request.AddHeader("Client-Ip", "127.0.0.1");
            request.AddHeader("Channel-Id", "BNK");

            request.AddUrlSegment("id", "abc");
            //request.AddParameter("productName", "PERSONAL LOAN");

            //IRestResponse response = client.Execute(request);
            //var content = response.Content;

            IRestResponse<Campaign> response2 = client.Execute<Campaign>(request);
            txtResponse.Text = response2.Content;
        }

        private void btnPostCampain_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan ds = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            //RestClient client = new RestClient("http://marketing-campaigns.getsandbox.com");

            RestClient client = new RestClient(txtBaseUrl.Text);

            Campaign toUpdate = new Campaign
            {
                cif = "24515711",
                productName = "PERSONAL LOAN",
                result = "Eligible",
                reason = "Customer Found Under Campaign",
                id = "1-1UPAJ1",
                code = "Camp - 1-1220097791",
                treatmentId = "1-K75BZO",
                personalizationFields = new List<StringKeyValuePair>
                {
                    new StringKeyValuePair
                    {
                        key = "PersonalizationField01",
                        value = "test"
                    }
                }
            };

            RestRequest request = new RestRequest("/campaigns", Method.POST);
            request.AddHeader("Authorization", "Bear token1234");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Financial-Id", "EBI");
            request.AddHeader("Client-Timestamp", Convert.ToInt64(ds.TotalMilliseconds).ToString());
            request.AddHeader("Accept-Language", "en");
            request.AddHeader("Unique-Reference-Code", Guid.NewGuid().ToString());
            request.AddHeader("Client-Ip", "127.0.0.1");
            request.AddHeader("Channel-Id", "BNK");

            request.AddBody(request.JsonSerializer.Serialize(toUpdate));

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            txtResponse.Text = content;
            //response.StatusCode
        }

        private void btnClearResponse_Click(object sender, RoutedEventArgs e)
        {
            txtResponse.Text = "";
        }
    }
}
