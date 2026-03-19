using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClienteLab
{
    public class TestCalcImpostoPF
    {
        [Fact]
        public void Test()
        {
            Pessoa_Fisica pessoa = new Pessoa_Fisica();
            double valor = 2500;
            double imposto = pessoa.Pagar_Imposto(valor);
            double resultado = valor + imposto;
            Assert.Equal(2750, resultado);
        }
    }

    // public class TestCalcImpostoPJ
    // {
    //     [Fact]

    //     public void Test()
    //     {
    //         Pessoa_Juridica pessoa = new Pessoa_Juridica();
    //         double valor = 35000;
    //         double imposto = pessoa.Pagar_Imposto(valor);
    //         double resultado = valor + imposto;
    //         Assert.Equal(42000, resultado);
    //     }
    // }

    // public class TestCalcImpostoPF0
    // {
    //     [Fact]
    //     public void Test()
    //     {
    //         Pessoa_Fisica pessoa = new Pessoa_Fisica();
    //         double valor = 0;
    //         double imposto = pessoa.Pagar_Imposto(valor);
    //         double resultado = valor + imposto;
    //         Assert.Equal(0, resultado);
    //     }
    // }

    // public class TestCalcImpostoPJ0
    // {
    //     [Fact]

    //     public void Test()
    //     {
    //         Pessoa_Juridica pessoa = new Pessoa_Juridica();
    //         double valor = 0;
    //         double imposto = pessoa.Pagar_Imposto(valor);
    //         double resultado = valor + imposto;
    //         Assert.Equal(0, resultado);
    //     }
    // }

    // public class TestCalcImpostoPFDec
    // {
    //     [Fact]
    //     public void Test()
    //     {
    //         Pessoa_Fisica pessoa = new Pessoa_Fisica();
    //         double valor = 100.50;
    //         double imposto = pessoa.Pagar_Imposto(valor);
    //         double resultado = valor + imposto;
    //         Assert.Equal(111, resultado);
    //     }
    // }

    // public class TestCalcImpostoPJDec
    // {
    //     [Fact]

    //     public void Test()
    //     {
    //         Pessoa_Juridica pessoa = new Pessoa_Juridica();
    //         double valor = 100.50;
    //         double imposto = pessoa.Pagar_Imposto(valor);
    //         double resultado = valor + imposto;
    //         Assert.Equal(12, resultado);
    //     }
    // }
}