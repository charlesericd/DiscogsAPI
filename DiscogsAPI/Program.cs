using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DiscogsAPI
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();
        public static DiscogsCollection discogsCollection = new DiscogsCollection();

        public static async Task Main(string[] args)
        {
            await LoadDiscogsCollection();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //Permet d'importer la collection retourner par l'API de Discogs dans un objet de type DiscogsCollection.
        //On peut ensuite manipuler l'objet discogsCollection pour retourner le résultat désiré dans notre méthode Get.
        private static async Task LoadDiscogsCollection()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.discogs.com/users/ausamerika/collection/folders/0/releases");
            discogsCollection = await JsonSerializer.DeserializeAsync<DiscogsCollection>(await streamTask);
        }
    }
}
