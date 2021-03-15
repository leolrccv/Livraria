using System;
using System.IO;


namespace Biblioteca {
    class Cpf {
        public static bool CadastraCPF(Cliente cliente) {
            while (true) {
                Console.Write("CPF: ");
                var cpf = Console.ReadLine().Replace(";", "").Replace(".", "").Replace("-", "").Trim();
                var valido = VerificaCPF(cpf);
                if (valido) {
                    cliente.CPF = cpf;
                    break;
                }
                Console.WriteLine("Por favor, insira um CPF válido!\n");
            }
            //verifica se o cpf já está cadastrado
            if (!File.Exists("CLIENTE.csv")) {
                using (File.Create("CLIENTE.csv")) { }
                return true;
            }
            using (var streamReader = new StreamReader("CLIENTE.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine().Split(';');
                    if (linha[1] == cliente.CPF) {
                        Console.WriteLine("\nCliente já cadastrado!!");
                        Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
                        Console.ReadKey();
                        Console.Clear();
                        return false;
                    }
                }
            }
            return true;
        }
        private static bool VerificaCPF(string cpf) {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}

