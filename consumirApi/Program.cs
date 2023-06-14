using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace consumirApi
{
    internal class Program
    {
        
        static async Task Main(string[] args)
        {
            Console.WriteLine("Consumindo API desenvolvida");
            int op;
            string BaseUrl = "http://localhost:5210/";
            do
            {
                Console.WriteLine("Informe a opção desejada");
                Console.WriteLine("1 - consultar pessoas ");
                Console.WriteLine("2 - cadastrar pessoas ");
                Console.WriteLine("3 - alterar pessoa ");
                Console.WriteLine("4 - excluir pessoa ");
                Console.WriteLine("0 - sair");
                op = int.Parse(Console.ReadLine());

                switch(op)
                {
                    case 0:
                        break;


                    case 1:
                        List<Pessoa> pessoas = new List<Pessoa>(); //receber o que tem armazenado no sistema
                        HttpClient client = new HttpClient(); //instancioando um objeto para fazer o acesso via http
                        client.BaseAddress = new Uri(BaseUrl); //definindo o endereço da API
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage resposta = await client.GetAsync("Pessoa/pessoas"); //acessando o endpoint da API

                        if (resposta.IsSuccessStatusCode)
                        {
                            var retorno = resposta.Content.ReadAsStringAsync().Result; //obtendo o retorno de uma consulta a API
                            pessoas = JsonConvert.DeserializeObject<List<Pessoa>>(retorno); 
                        }
                        else
                        {
                            Console.WriteLine("Erro :" + resposta.StatusCode);                            
                        }

                        foreach (Pessoa p in pessoas)
                        {
                            Console.WriteLine("\nID: " + p.id + "\nNome: " + p.nome + "\n");
                        }

                        break;


                    case 2:
                        Pessoa pessoa = new Pessoa();
                        Console.WriteLine("Digite o nome da pessoa: ");
                        pessoa.nome = Console.ReadLine();

                        HttpClient cliente = new HttpClient();
                        HttpResponseMessage respostaPost = await cliente.PostAsJsonAsync(BaseUrl + "Pessoa/pessoas" , pessoa);

                        Console.WriteLine("Retorno: " + respostaPost.StatusCode);

                        break;

                    case 3:
                        break;
                    case 4:
                        break;
                }
                



            }while (op != 0);
        }
    }
}