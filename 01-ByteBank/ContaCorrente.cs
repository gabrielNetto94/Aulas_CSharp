
namespace ByteBank
{
    class ContaCorrente
    {
        public Cliente titular; //ou public ByteBank.Cliente titular;
        public int numeroAgencia;
        public int numero;
        public double saldo;

        public bool Sacar(double valor)
        {
            if (this.saldo < valor)
                return false;

            this.saldo -= valor;
            return true;

        }

        public void Depositar(double valor)
        {
            if (valor > 0)
                this.saldo += valor;

        }

        public bool Transferir(ContaCorrente contaDestino, double valor)
        {
            if (this.saldo < valor)
            {
                return false;
            }

            this.saldo -= valor;
            contaDestino.Depositar(valor);

            return true;

        }
    }
}

