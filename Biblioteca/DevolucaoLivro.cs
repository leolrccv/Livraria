using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Biblioteca {
    public class DevolucaoLivro : EmprestimoLivro {
        public static CultureInfo ptBR = new CultureInfo("pt-BR");

        private double ValorMulta { get; set; }
        public static void Devolucao() {
            var devolucao = new DevolucaoLivro();
            Console.WriteLine("### Devolução de Livro ###");

            if (!PegarDadosArquivo(devolucao)) return;
            Multa(devolucao);
            Devolver(devolucao);

            Console.WriteLine("\nLivro Devolvido com Sucesso!!");

            if (devolucao.ValorMulta > 0) Console.WriteLine($"Valor da multa: R${devolucao.ValorMulta:0.00}");

            Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
            Console.ReadKey();
            Console.Clear();
        }
        private static bool PegarDadosArquivo(DevolucaoLivro devolucao) {

            Console.Write("Numero Tombo: ");
            if (long.TryParse(Console.ReadLine(), out var numeroTombo)) {
                devolucao.NumeroTombo = numeroTombo;
            }

            if (File.Exists("EMPRESTIMO.csv")) {
                using (var streamReader = new StreamReader("EMPRESTIMO.csv")) {
                    while (!streamReader.EndOfStream) {
                        var linha = streamReader.ReadLine().Split(';');
                        if (linha[1] == devolucao.NumeroTombo.ToString() && linha[4] == "1") {
                            devolucao.DataDevolucao = DateTime.ParseExact(linha[3].ToString(), "d", ptBR);
                            return true;
                        }
                    }
                }
            }
            Console.WriteLine("Livro não encontrado para devolução");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
        private static void Multa(DevolucaoLivro devolucao) {
            const float valorDia = 0.1f;

            var diferencaDias = (DateTime.Now.Date - devolucao.DataDevolucao).Days;

            if (diferencaDias > 0) {
                devolucao.ValorMulta = valorDia * diferencaDias;
            }
        }
        private static void Devolver(DevolucaoLivro devolucao) {
            List<string> lista = new List<string>();
            using (var streamReader = new StreamReader("EMPRESTIMO.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine();
                    var linhaSeparada = linha.Split(';');
                    var linhaModificada = "";

                    if (linhaSeparada[1] == devolucao.NumeroTombo.ToString()) {
                        for (var i = 0; i < linhaSeparada.Length - 1; i++) {
                            linhaModificada += $"{linhaSeparada[i]};";
                        }
                        linha = linhaModificada + "2";
                    }
                    lista.Add(linha);
                }
            }
            using (var streamWriter = new StreamWriter("EMPRESTIMO.csv")) {
                foreach (var l in lista) {
                    streamWriter.WriteLine(l);
                }
            }
        }
    }
}

