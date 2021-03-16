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
            leilao.IniciaPregao();

            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Null(leilao.Ganhador);
        }

        [Fact]
        public void LancaExecaoAoFinalizarPregaoSemIniciar()
        {
            //Arrange
            var leilao = new Leilao("Van gogh");
            //assert
            Assert.Throws<System.InvalidOperationException>(
                //act
                () => leilao.TerminaPregao()
            );
        
            
            
        }

        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(1200, new double[] { 1200, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComLances(double esperado, double[] valores)
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

            var obtido = leilao.Ganhador.Valor;

            //Assert
            Assert.Equal(esperado, obtido);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void RetornaMaiorValorDadoLeilaoComLances1(double esperado, List<double> valores)
        {
            //Arrange
            var leilao = new Leilao("Van gogh");
            var joao = new Interessada("Joao", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < valores.Count; i++)
            {
                var valor = valores[i];
                if (i % 2 == 0) {
                    leilao.RecebeLance(joao, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            //Act
            leilao.TerminaPregao();

            var obtido = leilao.Ganhador.Valor;

            //Assert
            Assert.Equal(esperado, obtido);
        }


        public static IEnumerable<object[]> GetData()
        {
            return new List<object[]>
            { 
                new object[]{ 
                    1200,
                    new List<double>()
                    {
                        1200, 900, 1000, 990
                    }
                } 
            };
        }

    }
}
