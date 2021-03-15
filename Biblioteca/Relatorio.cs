using System;
using System.IO;

namespace Biblioteca {
    public class Relatorio {
        private string CPF { get; set; }
        private string Titulo { get; set; }
        private string Status { get; set; }
        private string DataEmprestimo { get; set; }
        private string DataDevolucao { get; set; }

        public static void ExibirRelatorio() {
            var relatorio = new Relatorio();
            var cont = 0;

            Console.WriteLine("### Relatório de Empréstimos e Devoluções ###\n");
            if (File.Exists("EMPRESTIMO.csv")) {
                using (var streamReader = new StreamReader("EMPRESTIMO.csv")) {
                    while (!streamReader.EndOfStream) {
                        var linha = streamReader.ReadLine().Split(';');
                        if (cont > 0) {
                            relatorio.CPF = PegarCpf(linha[0]);
                            relatorio.Titulo = PegarTitulo(linha[1]);
                            if (linha[4] == "1") relatorio.Status = "Emprestado";
                            else relatorio.Status = "Devolvido";
                            relatorio.DataEmprestimo = linha[2];
                            relatorio.DataDevolucao = linha[3];

                            Console.WriteLine("====================================");
                            Console.WriteLine($"CPF do Cliente = {relatorio.CPF}\nTítulo do Livro: {relatorio.Titulo}\nStatus do Emprestimo: {relatorio.Status}\n" +
                                $"Data de Empréstimo: {relatorio.DataEmprestimo}\nData de Devolução: {relatorio.DataDevolucao}");
                            Console.WriteLine("====================================");
                        }
                        cont++;
                    }
                }
            }
            if (cont == 1) Console.WriteLine("\nNão há Registros de Empréstimos e Devoluções!");
            else Console.WriteLine("\n\nRelatório Concluido!!");

            Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
            Console.ReadKey();
            Console.Clear();
        }
        private static string PegarCpf(string id) {
            using (var streamReader = new StreamReader("CLIENTE.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine().Split(';');
                    if (linha[0] == id) {
                        return linha[1];
                    }
                }
            }
            return null;
        }
        private static string PegarTitulo(string numTombo) {
            using (var streamReader = new StreamReader("LIVRO.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine().Split(';');
                    if (linha[0] == numTombo) {
                        return linha[2];
                    }
                }
            }
            return null;
        }
    }
}
