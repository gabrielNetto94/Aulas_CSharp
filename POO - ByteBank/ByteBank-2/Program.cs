using ByteBank_2.Funcionarios;
using ByteBank_2.Sistemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_2
{
    class Program
    {
        static void Main(string[] args)
        {

            //CalcularBonificacao();

            UsarSistema();

            Console.ReadLine();

        }
        public static void CalcularBonificacao()
        {
            GerenciadorBonificacao gerenciadorBonificacao = new GerenciadorBonificacao();

            Diretor gabriel = new Diretor("123.123.123 - 53");
            gabriel.Nome = "Gabriel Netto";

            Designer amanda = new Designer("321.321.321-55");
            amanda.Nome = "Amanda Machado";

            GerenteDeConta pedro = new GerenteDeConta("123.123.123-44");
            pedro.Nome = "Pedro Costa";

            Auxiliar maria = new Auxiliar("123.123.444-99");

            gerenciadorBonificacao.Registrar(gabriel);
            gerenciadorBonificacao.Registrar(amanda);
            gerenciadorBonificacao.Registrar(pedro);
            gerenciadorBonificacao.Registrar(maria);

            Console.WriteLine("Total bonificação " + gerenciadorBonificacao.GetTotalBonificacao());
        }
        public static void UsarSistema()
        {
            SistemaInterno sistemaInterno = new SistemaInterno();
            

            Console.WriteLine("Gabriel Diretor  ");
            Diretor gabriel = new Diretor("123.123.123 - 53");
            gabriel.Nome = "Gabriel Netto";
            gabriel.Senha = "123";

            sistemaInterno.Logar(gabriel, gabriel.Senha);

            Console.WriteLine("Parceiro Comercial  ");
            ParceiroComercial parceiro = new ParceiroComercial();
            parceiro.Senha = "123";
            sistemaInterno.Logar(parceiro, "123");

            Console.WriteLine("Criando teste t");
            TesteAutenticar t = new TesteAutenticar();
            t.Senha = "a";

            sistemaInterno.Logar(t,"a");
            sistemaInterno.Logar(t, "b");

        }
    }
}
