using matheus.LeilaoOnline.Core;
using System;

namespace matheus.LeilaoOnline.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var leilao = new Leilao("Van gogh");
            var joao = new Interessada("Joao", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(joao, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(joao, 1000);
            leilao.RecebeLance(maria, 990);

            leilao.TerminaPregao();

            Console.WriteLine(leilao.Ganhador.Valor);
        }
    }
}
