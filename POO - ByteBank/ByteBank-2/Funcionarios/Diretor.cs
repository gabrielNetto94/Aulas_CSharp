using ByteBank_2.Sistemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_2.Funcionarios
{
    public class Diretor :  FuncionarioAutenticavel //Diretor herda da classe pai Funcionario
    {

        public Diretor(string cpf) : base(5000, cpf) //no momento que é executado o construtor da classe Diretor ele passa adiante o argumento para o construtor da classe pai Funcionario
        {
        }

        public override double GetBonificacao()//sobrescrita do método da classe pai Funcionario com a palavra reservada "override"
        {
            return Salario * 0.5;
            //base.GetBonificacao();//chama o método GetBonificacao da classe pai através do "base"
        }

        public override void AumentarSalario()
        {
            Salario *= 1.15;
        }

    }
}
