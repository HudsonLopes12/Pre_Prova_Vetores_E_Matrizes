using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string caminhoArquivo = "dados_desmatamento.csv";

        CriarCSVExemploSeNaoExistir(caminhoArquivo);

        int[,] matrizAnterior = new int[6, 6];
        int[,] matrizAtual = new int[6, 6];

        CarregarDadosCSV(caminhoArquivo, matrizAnterior, matrizAtual);
        
        Console.WriteLine("=== ESTADO DA RESERVA (HÁ 6 MESES) ===");
        ExibirMatriz(matrizAnterior);

        Console.WriteLine("\n=== ESTADO DA RESERVA (SITUAÇÃO ATUAL) ===");
        ExibirMatriz(matrizAtual);

        Console.WriteLine("\n=== RELATÓRIO COMPARATIVO ===");
        AnalisarAumento(matrizAnterior, matrizAtual);
    }

    static void CarregarDadosCSV(string caminho, int[,] matrizAnterior, int[,] matrizAtual)
    {
        if (!File.Exists(caminho))
        {
            Console.WriteLine($"Erro: Arquivo {caminho} não encontrado.");
            return;
        }

        string[] linhas = File.ReadAllLines(caminho);
        
        for (int i = 0; i < 6; i++)
        {
            string[] valores = linhas[i].Split(',');
            for (int j = 0; j < 6; j++)
            {
                matrizAnterior[i, j] = int.Parse(valores[j].Trim());
            }
        }

        for (int i = 0; i < 6; i++)
        {
            string[] valores = linhas[i + 6].Split(',');
            for (int j = 0; j < 6; j++)
            {
                matrizAtual[i, j] = int.Parse(valores[j].Trim());
            }
        }
    }

    static void ExibirMatriz(int[,] matriz)
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.Write(matriz[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static double[] CalcularPercentualDesmatamento(int[,] matriz)
    {
        double qtdDesmatada = 0;   // Código 0
        double qtdParcial = 0;     // Código 1
        double qtdPreservada = 0;  // Código 2

        double totalCelulas = 36.0;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (matriz[i, j] == 0)
                    qtdDesmatada++;
                else if (matriz[i, j] == 1)
                    qtdParcial++;
                else if (matriz[i, j] == 2)
                    qtdPreservada++;
            }
        }

        double percentualDesmatada = (qtdDesmatada / totalCelulas) * 100.0;
        double percentualParcial = (qtdParcial / totalCelulas) * 100.0;
        double percentualPreservada = (qtdPreservada / totalCelulas) * 100.0;

        return new double[] { percentualDesmatada, percentualParcial, percentualPreservada };
    }

    static void AnalisarAumento(int[,] matrizAnterior, int[,] matrizAtual)
    {
        double[] percAnterior = CalcularPercentualDesmatamento(matrizAnterior);
        double[] percAtual = CalcularPercentualDesmatamento(matrizAtual);

        double desmatamentoAnterior = percAnterior[0]; // Índice 0 = Código 0 (Totalmente Desmatada)
        double desmatamentoAtual = percAtual[0];

        Console.WriteLine("Percentual de ocorrências na matriz 6 meses atrás:");
        Console.WriteLine($"Área Desmatada (Código 0): {desmatamentoAnterior:F2}%");

        Console.WriteLine("\nPercentual de ocorrências na matriz atual:");
        Console.WriteLine($"Área Desmatada (Código 0): {desmatamentoAtual:F2}%");

        Console.WriteLine();
        if (desmatamentoAtual > desmatamentoAnterior)
        {
            Console.WriteLine($"Houve Aumento no Desmatamento – Anterior {desmatamentoAnterior:F2}% -> Atual {desmatamentoAtual:F2}%");
        }
        else if (desmatamentoAtual < desmatamentoAnterior)
        {
            Console.WriteLine($"Houve Redução no Desmatamento – Anterior {desmatamentoAnterior:F2}% -> Atual {desmatamentoAtual:F2