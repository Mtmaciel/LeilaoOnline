using System;
using System.Collections.Generic;
using System.Linq;

namespace matheus.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoEmAndamento,
        LeilaoFechado,
        LeilaoAntesDoPregao
    }

    public class Leilao
    {
        private Interessada _ultimoCliente;

        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
        }

        private bool LanceValido (Interessada cliente, double valor)
        {
            return ((cliente != _ultimoCliente) &&
                Estado == EstadoLeilao.LeilaoEmAndamento);
        }
        public void RecebeLance(Interessada cliente, double valor)
        {
            if (LanceValido(cliente,valor))
            {
               _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if(Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException();
            }
            if (Lances.Any()){
                Ganhador = Lances.OrderBy(x => x.Valor).Last();
            }
            Estado = EstadoLeilao.LeilaoFechado;
        }
    }
}