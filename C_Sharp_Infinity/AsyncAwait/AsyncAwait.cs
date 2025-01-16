using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class AsyncAwaitNew
    {
        public static async Task<string> FetchDataFromAPI(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;

                }
                catch (HttpRequestException ex)
                {

                    Console.WriteLine($"Request Error: {ex.Message}");
                    throw;
                }
            }
        }

        public Transaction GetTransactionDetails(int transactionId)
        {
            return FetchTransactionDetailsAsync(transactionId).Result;
        }

        private async Task<Transaction> FetchTransactionDetailsAsync(int transactionId)
        {
            //Simulate fetching data from database
            await Task.Delay(1000);
            return new Transaction { TransactionId = transactionId, Amount = 100.0m };


        }

       
    }
}
