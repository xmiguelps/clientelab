using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteLab
{
    public class Pessoa_Juridica : Cliente
    {
        public string Cnpj {get; set;}

        public string Ie {get; set;}

        public override double Pagar_Imposto(double valor)
        {
            valor = valor * 0.20;
            return valor;
        }

        public Pessoa_Juridica(string nome, string endereco, string cnpj, string ie)
        {
            Nome = nome;
            Endereco = endereco;
            Cnpj = cnpj;
            Ie = ie;
        }

        public Pessoa_Juridica()
        {
            Id = -1;
            Nome = "";
            Endereco = "";
            Valor_Compra = -1;
            Valor_Imposto = -1;
            Total = -1;
            Cnpj = "";
            Ie = "";
        }
    }
}