using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_2.Funcionarios
{
    public abstract class Funcionario //abstract torna a classe "genérica" para que outras classes herdem dela para pegar atributos padrão
    {

        public static int TotalFuncionarios { get; private set; }
        public string Nome { get; set; }
        public string CPF { get; private set; }
        public double Salario { get; protected set; } //protected "protege" o set de outras classes mas permite o set de classes filhas
        
        public Funcionario(double salario, string cpf)
        {
            Salario = salario;
            CPF = cpf;
            TotalFuncionarios++;
        }

        //método que aceita ser sobrescrito com a palavra reservada "virtual"
        //método abstract necessita ser sobrescrito pela classe filha
        public abstract double GetBonificacao();

        public abstract void AumentarSalario();

    }
}
