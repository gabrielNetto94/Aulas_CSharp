
namespace ByteBank_2
{
    class ContaCorrente
    {
        public ContaCorrente(int numero, int agencia)//construtor da classe
        {
            Numero = numero;
            Agencia = agencia;
            TotalContasCriadas++;
        }

        //Variável static compartilhada com toda a classe
        //private set, torna inacessivel alterar o valor da variável fora da classe
        public static int TotalContasCriadas { get; private set; }

        public Cliente Titular { get; set; } //get set direto se não precisar de nenhuma regra específica

        private int _agencia;
        public int Agencia
        {
            get
            {
                return _agencia;
            }
            set
            {
                if (value <= 0)
                {
                    return;
                }
                _agencia = value;
            }
        }
        public int Numero { get; set; }

        private double _saldo; //cria o atributo privado
        public double Saldo //cria outro campo público com get e set e a devida regra de negócio
        {
            get
            {
                return _saldo;
            }
            set //palavra reservada "value" para o set
            {
                if (value < 0)
                {
                    return;
                }

                this._saldo = value;//o "this é opcional neste caso
            }
        }


        public bool Sacar(double valor)
        {
            if (this._saldo < valor)
                return false;

            this._saldo -= valor;
            return true;

        }

        public void Depositar(double valor)
        {
            if (valor > 0)
                this._saldo += valor;

        }

        public bool Transferir(ContaCorrente contaDestino, double valor)
        {
            if (this._saldo < valor)
            {
                return false;
            }

            this._saldo -= valor;
            contaDestino.Depositar(valor);

            return true;

        }
    }
}