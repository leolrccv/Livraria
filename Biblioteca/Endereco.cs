using System;

namespace Biblioteca {
    public class Endereco {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public static Endereco CadastrarEndereco() {
            var endereco = new Endereco();

            Console.Write("Logradouro (rua ou avenida com numero): ");
            endereco.Logradouro = Console.ReadLine().Replace(";", "").Trim();
            Console.Write("Bairro: ");
            endereco.Bairro = Console.ReadLine().Replace(";", "").Trim();
            Console.Write("Cidade: ");
            endereco.Cidade = Console.ReadLine().Replace(";", "").Trim();
            Console.Write("Estado: ");
            endereco.Estado = Console.ReadLine().Replace(";", "").Trim();
            Console.Write("CEP: ");
            endereco.CEP = Console.ReadLine().Replace(";", "").Trim();

            return endereco;
        }
    }
}
