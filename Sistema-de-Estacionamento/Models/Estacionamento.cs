namespace Sistema_de_Estacionamento.Models;

public class Estacionamento
{
    public Estacionamento(decimal preçoInicial, decimal preçoPorHora)
    {
        this.PreçoInicial = preçoInicial;
        this.PreçoPorHora = preçoPorHora;
    }

    public decimal PreçoInicial;
    public decimal PreçoPorHora;

    public List<string> Veiculos = new List<String>();

    public void AdicionarVeiculo()
    {
        Console.WriteLine($"Digite a placa do veículo");
        string placa = Console.ReadLine()!.ToUpper();
        //Validação quanto a existência do veículo no estacionamento
        if (Veiculos.Contains(placa))
        {
            Console.WriteLine($"{placa} já está no estacionamento");
        }
        //Validação da Placa
        else if(ValidacaoDePlaca(placa))
        {
            Veiculos.Add(placa);
            Console.WriteLine($"Veículo:{placa} adicionado com sucesso");
        }
        else
        {
            Console.WriteLine("Placa do veículo é inválida");
        }
        
    }
    
    public void RemoverVeiculo()
    {
        Console.WriteLine($"Digite a placa do veículo");
        string placa = Console.ReadLine()!.ToUpper();
        if (Veiculos.Contains(placa))
        {
            Console.WriteLine($"Digite o numero de horas que o veículo permaneceu no estacionamento(Hora Inteira, caso " +
                              $"tenha ficado menos de 1 hora digite 0)");
            decimal horas = decimal.Parse(Console.ReadLine()!);
            Console.WriteLine($"Digite o numero de minutos que o veículo permaneceu no estacionamento(Minutos inteiros, " +
                              $"caso não tenha minutos além de 1 hora digite 0)");
            decimal minutos = Decimal.Parse(Console.ReadLine()!);
            if (minutos != 0 || horas != 0)
            {
                Console.WriteLine($"O valor a ser pago pela guarda do veículo {placa} por {horas} horas e {minutos} " +
                                  $"minutos é R${CalcularPreço(horas, minutos)} ");
                Veiculos.Remove(placa);
            }
        }
        else
        {
            Console.WriteLine($"{placa} não está presente na lista de veículos");
        }
    }

    public void ListarVeiculos()
    {
        if (Veiculos.Any())
        {
            Console.WriteLine($"Os veículos estacionados são:");
            foreach (string placa in Veiculos)
            {
                Console.WriteLine($"Placa: {placa}");
            }
        }
        else
        {
            Console.WriteLine("Não há veículos estacionados");
        }
    }
    //Calcula o número de horas e minutos e assim retorna o valor total a ser pago. Caso o tempo seja igual ou inferior
    //a 30 minutos a cobrança será apenas o valor incial, caso seja superior irá cobrar o adicional por minuto.
    decimal CalcularPreço(decimal horas, decimal minutos)
    {
        if (horas == 0 && minutos <= 30)
        {
            return PreçoInicial;
        }
        return PreçoInicial + PreçoPorHora * ((minutos / 60) + horas);
        
    }
        

    //Validação da placa seguindo o padrão brasileiro
    bool ValidacaoDePlaca(string placa)
    {
        //Valida a quantidade de caracteres
        bool controle = true;
        if (placa.Length != 7)
        {
            controle = false;
        }
        //Valida o conteúdo dos caracteres
        else
        {
            for (int i = 0; i < 7; i++)
            {
                if (i > 0 && i < 3 || i == 4)
                {
                    if (placa[i] >= 'A' && placa[i] <= 'Z')
                    {
                        continue;
                    }

                    controle = false;
                    break;

                }

                if (i == 3 || i > 4)
                {
                    if (placa[i] >= '0' && placa[i] <= '9')
                    {
                        continue;
                    }
                    controle = false;
                    break;
                }
            }
        }

        return controle;
    }
}