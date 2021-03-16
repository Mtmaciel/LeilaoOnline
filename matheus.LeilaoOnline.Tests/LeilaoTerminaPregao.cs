using matheus.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace matheus.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Fact]
        public void RetornaNuloDadoLeilaoSemLances()
        {
            //Arrange
            var leilao = new Leilao("Van gogh");
            var joao = new Interessada("Joao", leilao);

            //Act
            leilao.TerminaPregao();


            //Assert
            Assert.Null(leilao.Ganhador);
        }

        [Theory]
        [InlineData(1000, new double[]{ 800, 900, 1000, 990 })]
        [InlineData(1200, new double[]{ 1200, 900, 1000, 990 })]
        [InlineData(800,  new double[]{ 800})]
        public void RetornaMaiorValorDadoLeilaoComLances(double esperado, double[] valores)
        {
            //Arrange
            var leilao = new Leilao("Van gogh");
            var joao = new Interessada("Joao", leilao);
            var maria = new Interessada("Maria", leilao);

            foreach (var item in valores)
            {
                leilao.RecebeLance(joao, item);
            }

            //Act
            leilao.TerminaPregao();

            var obtido = leilao.Ganhador.Valor;

            //Assert
            Assert.Equal(esperado, obtido);
        }

    }
}
