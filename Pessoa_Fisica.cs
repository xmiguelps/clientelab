using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClienteLab;

namespace ClienteLab
{
    public class Pessoa_Fisica : Cliente
    {
        public string Cpf {get; set;}

        public string Rg {get; set;}

        public Pessoa_Fisica(int id, string nome, string endereco, double valor_compra, double valor_imposto, double total)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            Valor_Compra = valor_compra;
            Valor_Imposto = valor_imposto;
            Total = total;
        }
    }
} 