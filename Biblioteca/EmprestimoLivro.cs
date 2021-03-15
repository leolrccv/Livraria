using System;
using System.Globalization;
using System.IO;

namespace Biblioteca {
    public class EmprestimoLivro {
        private long IdCliente { get; set; }
        public long NumeroTombo { get; set; }
        private DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        private int StatusEmprestimo { get; set; }

        public static void Emprestimo() {
            var emprestimo = new EmprestimoLivro();

            Console.WriteLine("### Empréstimo ###");

            if (!SituacaoLivro(emprestimo)) return;
            if (!ClienteCadastrado(emprestimo)) return;
            emprestimo.DataEmprestimo = DateTime.Now;
            CadastrarDataDevolucao(emprestimo);
            emprestimo.StatusEmprestimo = 1;

            ArquivarEmprestimo(emprestimo);
            Console.WriteLine("\nLivro Emprestado com Sucesso!!");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
            Console.ReadKey();
            Console.Clear();
        }
        private static bool SituacaoLivro(EmprestimoLivro emprestimo) {
            var valido = false;

            Console.Write("Numero Tombo: ");
            if (long.TryParse(Console.ReadLine(), out var numTombo)) {
                emprestimo.NumeroTombo = numTombo;
            }

            if (!File.Exists("LIVRO.csv")) {
                Console.WriteLine("\nNenhum Livro cadastrado em nossa biblioteca!");
                return valido;
            }
            using (var streamReader = new StreamReader("LIVRO.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine().Split(';');
                    if (linha[0] == emprestimo.NumeroTombo.ToString()) {
                        valido = true;
                    }
                }
            }
            if (!valido) {
                Console.WriteLine("\nLivro não encontrado em nossa biblioteca!");
                Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
                Console.ReadKey();
                Console.Clear();
                return valido;
            }

            if (!File.Exists("EMPRESTIMO.csv")) {
                using (File.Create("EMPRESTIMO.csv")) { }
                return valido;
            }

            using (var streamReader = new StreamReader("EMPRESTIMO.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine().Split(';');
                    if (linha[1] == emprestimo.NumeroTombo.ToString() && linha[4] == "1") {
                        valido = false;
                        Console.WriteLine("\nLivro já está emprestado!");
                        Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            return valido;
        }
        private static bool ClienteCadastrado(EmprestimoLivro emprestimo) {
            Console.Write("CPF do Cliente: ");
            var cpf = Console.ReadLine().Replace(";", "").Trim();

            if (File.Exists("CLIENTE.csv")) {
                using (var streamReader = new StreamReader("CLIENTE.csv")) {
                    while (!streamReader.EndOfStream) {
                        var linha = streamReader.ReadLine().Split(';');
                        if (linha[1] == cpf) {
                            emprestimo.IdCliente = long.Parse(linha[0]);
                            return true;

                        }
                    }
                }
            }
            Console.WriteLine("\nCliente não cadastrado em nossa biblioteca!");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
        private static void CadastrarDataDevolucao(EmprestimoLivro emprestimo) {
            CultureInfo ptBR = new CultureInfo("pt-BR");

            Console.Write("Data para Devolução[dd/mm/AAAA]: ");
            while (true) {
                if (!DateTime.TryParseExact(Console.ReadLine(), "d", ptBR, DateTimeStyles.None, out DateTime dDevolucao)) {
                    Console.Write("Digite a data no formato correto [dd/mm/AAAA]: ");
                    continue;
                }

                if (dDevolucao < DateTime.Now.Date) {
                    Console.WriteLine("\nA Data para Devolução não pode ser menor que a data de Empréstimo!");
                    Console.Write("Insira uma nova data para devolução: ");
                    continue;
                }
                emprestimo.DataDevolucao = dDevolucao;
                break;
            }
        }
        private static void ArquivarEmprestimo(EmprestimoLivro emprestimo) {
            using (var streamWriter = new StreamWriter("EMPRESTIMO.csv", true)) {
                streamWriter.WriteLine(emprestimo);
            }
        }
        public override string ToString() {
            return $"{IdCliente};{NumeroTombo};{DataEmprestimo:d};{DataDevolucao:d};{StatusEmprestimo}";
        }
    }
}
