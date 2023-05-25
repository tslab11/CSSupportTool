using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSupportTool
{
    public partial class FormMain : Form
    {

        private HttpClient client = new HttpClient();

        private static readonly string BASE_URL = "http://localhost:5000";

        public class tModel
        {
            public int sample1 { get; set; } = 123;
            public double sample2 { get; set; } = 45.6;
            public bool sample3 { get; set; } = true;
            public string sample4 { get; set; } = "ABC";
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            // GETリクエストの例
            //await GetRequest();

            // POSTリクエストの例
            tModel req = new tModel();
            tModel res = await PostRequest<tModel, tModel>(req);
            await Console.Out.WriteLineAsync(res.sample4);

            //MessageBox.Show("Test");
        }

        async Task GetRequest()
        {
            string url = $"{BASE_URL}/api/data";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

            }
        }

        async Task<S> PostRequest<F, S>(F reqData)
        {
            string url = $"{BASE_URL}/api/data";
            //var requestData = new { key1 = "value1", key2 = "value2" };  // POSTデータの例
            //var requestData = "value1";
            //int requestData = 123;
            //var requestData = new tModel();
            var requestData = reqData;

            var jsonOptions = new JsonSerializerOptions
            {
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(requestData, jsonOptions);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode == false)
            {
                return default(S);
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            S res = JsonSerializer.Deserialize<S>(responseBody, jsonOptions);

            Console.WriteLine(responseBody);

            return res;
        }

        //マウスのクリック位置を記憶
        private Point mousePoint;

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
                //または、つぎのようにする
                //this.Location = new Point(
                //    this.Location.X + e.X - mousePoint.X,
                //    this.Location.Y + e.Y - mousePoint.Y);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCapture_Click(object sender, EventArgs e)
        {
            Rectangle clientRect = new Rectangle(this.pictureBoxCaptureArea.Location, this.pictureBoxCaptureArea.Size);

            Rectangle screenRect = this.RectangleToScreen(clientRect);

            using (Bitmap bmp = this.CaptureScreen(screenRect))
            {
                bmp.Show(this, "capture");
            }
        }

        public Bitmap CaptureScreen(Rectangle screenRect)
        {

            Bitmap bmp = new Bitmap(screenRect.Size.Width, screenRect.Size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(screenRect.Location, new Point(0, 0), bmp.Size);
            }
            return bmp;

        }

    }
}
