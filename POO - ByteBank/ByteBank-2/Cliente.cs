using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_2
{
    class Cliente
    {
        public string Nome { get; set; }
        public string Profissao { get; set; }
        private string _cpf;
        public string Cpf
        {
            get
            {
                return _cpf;
            }
            set
            {
                //validarCPF();
                _cpf = value;
            }
        }


    }
}
