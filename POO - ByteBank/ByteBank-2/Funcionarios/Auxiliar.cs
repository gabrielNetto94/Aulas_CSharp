using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_2.Funcionarios
{
    class Auxiliar : Funcionario
    {
        public Auxiliar(string cpf) : base(2000, cpf)
        {

        }

        public override double GetBonificacao()//sobrescrita do método da classe pai Funcionario com a palavra reservada "override"
        {
            return Salario * 0.2;            
        }

        public override void AumentarSalario()
        {
            Salario *= 1.1;
        }
    }
}
