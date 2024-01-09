namespace HttpClientMockDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var namedayClient = new NamedayClient();
            Console.WriteLine($"Todays nameday in Sweden is {await namedayClient.GetTodaysNameday()}");
        }
    }
}
