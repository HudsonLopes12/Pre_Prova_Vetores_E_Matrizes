using System;

class Program
{
	static double calcularMedia(int[] roubos, bool verbosidade){
		double soma = 0;
		double media = 0;
		int i = 0;
		int tamanhoVetor = roubos.Length;
		
		for (i=0; i<tamanhoVetor; i++){
			soma = soma + roubos[i];
		}
		
		media=soma/tamanhoVetor;
		
		if(verbosidade == true){
			Console.WriteLine("Media de roubos: " + media);
		}
		return media;
	}
	
	static void exibirTop3BairrosViolentos(string[] bairros, int[] roubos){
		int i = 0;
		int primeiro = 0;
		int primeiroi = 0;
		int segundo = 0;
		int segundoi = 0;
		int terceiro = 0;
		int terceiroi = 0;
		int tamanhoVetor = roubos.Length;
		
		for (i=0; i<tamanhoVetor; i++){
			if(roubos[i] > primeiro){
				primeiro = roubos[i];
				primeiroi = i;
			}
		}
		
		for (i=0; i<tamanhoVetor; i++){
			if(roubos[i] > segundo && roubos[i] < primeiro){
				segundo = roubos[i];
				segundoi = i;
			}
		}
				
		for (i=0; i<tamanhoVetor; i++){
			if(roubos[i] > terceiro && roubos[i] < primeiro && roubos[i] < segundo){
				terceiro = roubos[i];
				terceiroi = i;
			}
		}
		
		Console.WriteLine("1º: " + bairros[primeiroi] + "indice" + primeiroi + " - " + primeiro);
		Console.WriteLine("2º: " + bairros[segundoi] + "indice" + segundoi + " - " + segundo);
		Console.WriteLine("3º: " + bairros[terceiroi] + "indice" + terceiroi + " - " + terceiro);
			}
			
			static void Main()
			{
				string[] bairros = {"Centro", "Moema", "Pinheiros", "Itaquera", "Tatuapé",
				"Santo Amaro", "Vila Mariana", "Lapa", "Capão Redondo", "Santana"};
				
			int[] roubos = [350, 120, 210, 480, 190, 310, 150, 280, 520, 240];
			
			calcularMedia(roubos, true);
			exibirTop3BairrosViolentos(bairros, roubos);
			}
	}