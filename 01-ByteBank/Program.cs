using System;

namespace ByteBank
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             ContaCorrente contaGabriel = new ContaCorrente();
             ContaCorrente contaAmanda = new ContaCorrente();
             contaGabriel.Depositar(300);

             Console.WriteLine("Saldo Gabriel: " + contaGabriel.saldo);

             if (contaGabriel.Transferir(contaAmanda, 300))
             {
                 Console.WriteLine("Transferência realizada!");
             }
             else
             {
                 Console.WriteLine("Transferência falhou!");
             }

             Console.WriteLine("Saldo Gabriel: " +contaGabriel.saldo);
             Console.WriteLine("Saldo Amanda: " + contaAmanda.saldo);

             Console.ReadLine();
            */

            Cliente gabriel = new Cliente();
            ContaCorrente conta = new ContaCorrente();

            conta.titular = gabriel;

            


            
        }
    }
}
