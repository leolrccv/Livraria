using System;
using System.IO;
using System.Globalization;


namespace Biblioteca {
    public class Cliente {
        private long IdCliente { get; set; }
        public string CPF { get; set; }
        private string Nome { get; set; }
        private DateTime DataNascimento { get; set; }
        private string Telefone { get; set; }
        private Endereco EnderecoCliente { get; set; }

        public static void Cadastro() {
            var cliente = new Cliente();

            Console.WriteLine("### Digite os dados do cliente ###");

            if (!Cpf.CadastraCPF(cliente)) return;

            GerarId(cliente);

            Console.Write("Nome: ");
            cliente.Nome = Console.ReadLine().Replace(";", "").Trim();

            CadastrarDataNascimento(cliente);

            Console.Write("Telefone: ");
            cliente.Telefone = Console.ReadLine().Replace(";", "").Trim();

            cliente.EnderecoCliente = Endereco.CadastrarEndereco();

            ArquivarCadastro(cliente);

            Console.WriteLine("\nCliente Cadastrado com Sucesso!!");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
            Console.ReadKey();
            Console.Clear();
        }
        private static void CadastrarDataNascimento(Cliente cliente) {
            CultureInfo ptBR = new CultureInfo("pt-BR");

            Console.Write("Data de Nascimento[dd/mm/AAAA]: ");
            while (true) {
                if (!DateTime.TryParseExact(Console.ReadLine(), "d", ptBR, DateTimeStyles.None, out DateTime dNascimento)) {
                    Console.Write("Digite a data no formato correto [dd/mm/AAAA]: ");
                    continue;
                }
                cliente.DataNascimento = dNascimento;
                break;
            }
        }
        private static void GerarId(Cliente cliente) {
            var id = "0";
            var cont = 0;

            using (var streamReader = new StreamReader("CLIENTE.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine().Split(';');
                    if (cont > 0) {
                        id = linha[0];
                    }
                    cont++;
                }
            }
            cliente.IdCliente = long.Parse(id) + 1;
        }
        private static void ArquivarCadastro(Cliente cliente) {
            using (var streamWriter = new StreamWriter("CLIENTE.csv", true)) {
                streamWriter.WriteLine(cliente);
            }
        }
        public override string ToString() {
            return $"{IdCliente};{CPF};{Nome};{DataNascimento:d};{Telefone};{EnderecoCliente.Logradouro};" +
                $"{EnderecoCliente.Bairro};{EnderecoCliente.Cidade};{EnderecoCliente.Estado};{EnderecoCliente.CEP}";
        }
    }
}
