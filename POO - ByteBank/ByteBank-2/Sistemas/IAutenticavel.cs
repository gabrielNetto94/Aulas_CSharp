using ByteBank_2.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank_2.Sistemas
{
    public interface IAutenticavel
    {

        bool Autenticar(string senha);
        
    }
}
