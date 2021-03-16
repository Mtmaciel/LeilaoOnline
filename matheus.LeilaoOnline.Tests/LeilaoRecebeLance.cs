using matheus.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace matheus.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(4, new double[] { 800, 900, 1000, 990 })]
        public void LeilaoRecebeLanceAposFinalizado(double esperado, double[] valores)
        {
            //Arrange
            var leilao = new Leilao("Van gogh");
            var joao = new Interessada("Joao", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < valores.Length; i++)
            {
                var valor = valores[i];
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(joao, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }


            //Act
            leilao.TerminaPregao();

            leilao.RecebeLance(maria, 2000);

            var obtido = leilao.Lances.Count();

            //Assert
            Assert.Equal(esperado, obtido);
        }

        [Theory]
        [InlineData(new double[] { 200, 300, 400, 500 })]
        [InlineData(new double[] { 200 })]
        [InlineData(new double[] { 200, 300, 400 })]
        [InlineData(new double[] { 200, 300, 400, 500, 600, 700 })]
        public void LeilaoRecebeNenhumLanceSemAbrirPregao(double[] ofertas)
        {
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano de Tal", leilao);

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            Assert.Empty(leilao.Lances);
        }
    }
}
