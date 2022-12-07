using System.Globalization;
using Exercise.Entities;
namespace Exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // pesquisa e localiza e retorna caminho e nome
                Console.Write("Name of the type of the file to be searched: ");
                string searchedFile = Console.ReadLine();
                var foulders = Directory.EnumerateFiles(@"c:\tmp","*"+searchedFile+"*",SearchOption.AllDirectories);
                foreach (var item in foulders) {
                    Console.WriteLine(item);
                }

                Console.WriteLine();

                // criação da pasta csc
                Directory.CreateDirectory(@"c:\tmp\csc");

                // informações de produtos que foram vendidos
                List<string> list = new List<string>();
                string info;
                bool option;
                do {
                    Console.Write("Product information: ");
                    info = Console.ReadLine();
                    list.Add(info);
                    Console.Write("Continue? ");
                    option = bool.Parse(Console.ReadLine());
                }
                while (option == true);

                // criação do arquivo ItensVendidos
                // separação nome, preco,quantidade
                using (StreamWriter fileWriter = File.AppendText(@"c:\tmp\csc\ItensVendidos.csv"))
                {
                    foreach (var item in list)
                    {
                        string[] fields = item.Split(',');
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);
                        Products product = new Products(name, price, quantity);
                        // escreve no arquivo
                        fileWriter.WriteLine(product.Name + " , " + product.Price.ToString("f2", CultureInfo.InvariantCulture)+" , "+product.Quantity);
                    }
                }

                Console.WriteLine();
                // lê e mostra o conteúdo escrito
               StreamReader reader = new StreamReader(@"c:\tmp\csc\ItensVendidos.csv");
                string line = reader.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
                reader.Close();

                // criação de destino e resumo.csv
                Directory.CreateDirectory(@"c:\tmp\destino");
                using (StreamWriter fileWriter=File.AppendText(@"c:\tmp\destino\Resumo.csv")) 
                {
                    foreach (var item in list) 
                    { 
                        string[] fields = item.Split(',');
                        string name=fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);
                        Products product = new Products(name, price, quantity);
                        fileWriter.WriteLine(product.Name+" , "+product.Totalize().ToString("f2",CultureInfo.InvariantCulture));
                    } 
                }
            }
            catch (Exception e) { Console.WriteLine("Error!\r\n"+e.Message); }
        }
    }
}