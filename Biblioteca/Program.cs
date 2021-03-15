using System;

namespace Biblioteca {
    class Program {
        static void Main() {
            while (true) {
                Console.WriteLine("====== Menu da Biblioteca ======\n");
                Console.WriteLine("1. Cadastro de Cliente\n2. Cadastro de Livro\n3. Empréstimo de Livro\n" +
                    "4. Devolução do Livro\n5. Relatório de Empréstimos e Devoluções\n0. Sair");
                Console.Write("\nDigite o número da opção que deseja: ");
                var op = Console.ReadLine();
                Console.Clear();

                switch (op) { 
                    case "1":
                        Cliente.Cadastro();
                        continue;
                    case "2":
                        Livro.Cadastro();
                        continue;
                    case "3":
                        EmprestimoLivro.Emprestimo();
                        continue;
                    case "4":
                        DevolucaoLivro.Devolucao();
                        continue;
                    case "5":
                        Relatorio.ExibirRelatorio();
                        continue;
                    case "0":
                        Console.WriteLine("Saindo do menu");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("O número deve ser de 0 a 5!!\n");
                        Console.ResetColor();
                        continue;
                }
                break;
            }
            Console.WriteLine("\nPressione qualquer tecla para finalizar o programa . . .");
            Console.ReadKey();
        }
    }
}
