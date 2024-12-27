using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelegramBot
{
    public partial class Form1 : Form
    {
        private static readonly string BotToken = "987654321:abcdefghijklmnopqrstuvxyz";
        private static readonly string ChatId = "123456";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SendMessageAsync("Hello world");
        }

        private static async Task SendMessageAsync(string message)
        {
            var client = new HttpClient();
            string url = $"https://api.telegram.org/bot{BotToken}/sendMessage";
            var parameters = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("chat_id", ChatId),
                new KeyValuePair<string, string>("text", message)
            });

            HttpResponseMessage response = await client.PostAsync(url, parameters);
            string result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send message. Telegram API response: {result}");
            }

            Console.WriteLine("Message sent successfully.");
        }

        private static async Task SendImageAsync(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                throw new Exception($"Image file not found: {imagePath}");
            }

            using (var client = new HttpClient())
            {
                string url = $"https://api.telegram.org/bot{BotToken}/sendPhoto";

                var form = new MultipartFormDataContent();
                form.Add(new StringContent(ChatId), "chat_id");
                form.Add(new StreamContent(File.OpenRead(imagePath)), "photo", Path.GetFileName(imagePath));

                HttpResponseMessage response = await client.PostAsync(url, form);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to send image. Telegram API response: {result}");
                }

                Console.WriteLine("Image sent successfully.");
            }
        }
    }
}
