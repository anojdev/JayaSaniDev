
using AsyncAwait;


string url = "https://jsonplaceholder.typicode.com/posts/1";

try
{
	//string result = await AsyncAwaitNew.FetchDataFromAPI(url);
 //   Console.WriteLine($"API Response: {result}");

    //AsyncAwaitNew asyncAwaitNew = new AsyncAwaitNew();
 //   //This call can cause deadlock
 //   Transaction transaction = asyncAwaitNew.GetTransactionDetails(123);
 //   Console.WriteLine($"Transaction ID: {transaction.TransactionId}, Amount: {transaction.Amount}");
  TaskVsValueTask taskVsValueTask = new TaskVsValueTask();
    int result = await taskVsValueTask.GetDataAsync();
    Console.WriteLine($"Result: {result}");
}
catch (Exception ex)
{

    Console.WriteLine($"An error occured: {ex.Message}");
}