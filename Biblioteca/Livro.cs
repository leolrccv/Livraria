using System;
using System.Globalization;
using System.IO;

namespace Biblioteca {
    public class Livro {
        private long NumeroTombo { get; set; }
        private string ISBN { get; set; }
        private string Titulo { get; set; }
        private string Genero { get; set; }
        private DateTime DataPublicacao { get; set; }
        private string Autor { get; set; }

        public static void Cadastro() {
            var livro = new Livro();

            Console.WriteLine("### Digite os dados do Livro ###");

            if (EstaCadastrado(livro)) return;

            GerarNumeroTombo(livro);

            Console.Write("Titulo: ");
            livro.Titulo = Console.ReadLine().Replace(";", "").Trim();
            Console.Write("Genero: ");
            livro.Genero = Console.ReadLine().Replace(";", "").Trim();

            CadastrarDataPublicacao(livro);

            Console.Write("Autor: ");
            livro.Autor = Console.ReadLine().Replace(";", "").Trim();

            ArquivarCadastro(livro);

            Console.WriteLine("\nLivro Cadastrado com Sucesso!!");
            Console.WriteLine($"Numero Tombo Gerado = {livro.NumeroTombo}");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
            Console.ReadKey();
            Console.Clear();
        }

        private static bool EstaCadastrado(Livro livro) {
            Console.Write("ISBN: ");
            var isbn = Console.ReadLine().Replace(";", "").Trim();

            if (!File.Exists("LIVRO.csv")) {
                using (File.Create("LIVRO.csv")) { }
                return false;
            }

            using (var streamReader = new StreamReader("LIVRO.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine().Split(';');
                    if (linha[1] == isbn) {
                        Console.WriteLine("\nLivro já cadastrado!!");
                        Console.WriteLine("Aperte qualquer tecla para voltar ao menu . . .");
                        Console.ReadKey();
                        Console.Clear();
                        return true;
                    }
                }
            }
            livro.ISBN = isbn;
            return false;
        }
        private static void GerarNumeroTombo(Livro livro) {
            var numeroTombo = "0";
            var cont = 0;

            using (var streamReader = new StreamReader("LIVRO.csv")) {
                while (!streamReader.EndOfStream) {
                    var linha = streamReader.ReadLine().Split(';');
                    if (cont > 0) {
                        numeroTombo = linha[0];
                    }
                    cont++;
                }
            }
            livro.NumeroTombo = long.Parse(numeroTombo) + 1;
        }
        private static void CadastrarDataPublicacao(Livro livro) {
            CultureInfo ptBR = new CultureInfo("pt-BR");

            Console.Write("Data de Publicação[dd/mm/AAAA]: ");
            while (true) {
                if (!DateTime.TryParseExact(Console.ReadLine(), "d", ptBR, DateTimeStyles.None, out DateTime dPublicacao)) {
                    Console.Write("Digite a data no formato correto [dd/mm/AAAA]: ");
                    continue;
                }
                livro.DataPublicacao = dPublicacao;
                break;
            }
        }
        private static void ArquivarCadastro(Livro livro) {
            using (var streamWriter = new StreamWriter("LIVRO.csv", true)) {
                streamWriter.WriteLine(livro);
            }
        }
        public override string ToString() {
            return $"{NumeroTombo};{ISBN};{Titulo};{Genero};{DataPublicacao:d};{Autor}";
        }
    }
}
