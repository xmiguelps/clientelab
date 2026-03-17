using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace ClienteLab
{
    public class SistemaDAO
    {
        private readonly ConexaoBanco _conexaoBanco;

        public SistemaDAO()
        {
            _conexaoBanco = new ConexaoBanco();
        }

        public void CadastrarClientePF(Pessoa_Fisica cliente)
        {
            using (var conexao = _conexaoBanco.ObterConexao())
            {
                string query = "INSERT INTO tb_cliente_pf (nome, endereco, cpf, rg) VALUES (@nome, @endereco, @cpf, @rg)";
                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@endereco", cliente.Endereco);
                    comando.Parameters.AddWithValue("@cpf", cliente.Cpf);
                    comando.Parameters.AddWithValue("@rg", cliente.Rg);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
            Console.WriteLine("[!] (Pessoa Física) cadastrado com sucesso!");
        }

        public void CadastrarClientePJ(Pessoa_Juridica cliente)
        {
            using (var conexao = _conexaoBanco.ObterConexao())
            {
                string query = "INSERT INTO tb_cliente_pj (nome, endereco, cnpj, ie) VALUES (@nome, @endereco, @cnpj, @ie)";
                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@endereco", cliente.Endereco);
                    comando.Parameters.AddWithValue("@cpf", cliente.Cnpj);
                    comando.Parameters.AddWithValue("@rg", cliente.Ie);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
            Console.WriteLine("[!] (Pessoa Jurídica) cadastrado com sucesso!");
        }

        public void RegistrarVenda(int num, int id, double valor_compra)
        {
            using (var conexao = _conexaoBanco.ObterConexao())
            {
                Cliente cliente = null;
                string query_venda = "";
                string query_recibo = "";
                
                switch (num)
                {
                    case 1:
                        cliente = new Pessoa_Fisica();
                        query_venda = "INSERT INTO tb_vendas (fk_cliente_pf, valor_compra, valor_imposto, valor_total) VALUES (@id, @valor_compra, @valor_imposto, @valor_total)";
                        query_recibo = "SELECT (nome, endereco, cpf, rg) WHERE tb_cliente_pf id = @id";
                        break;
                    case 2:
                        cliente = new Pessoa_Juridica(); 
                        query_venda = "INSERT INTO tb_vendas (fk_cliente_pj, valor_compra, valor_imposto, valor_total) VALUES (@id, @valor_compra, @valor_imposto, @valor_total)";
                        query_recibo = "SELECT (nome, endereco, cnpj, ie) FROM tb_cliente_pj WHERE id = @id";
                        break;
                    
                    default:
                        Console.WriteLine("Resposta Invalida! Tente Novamente");
                        break;
                }
                using (var comandoCliente = new MySqlCommand(query_recibo, conexao))
                {
                    comandoCliente.Parameters.AddWithValue("@id", id);
                    conexao.Open();
                    using (var reader = comandoCliente.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente.Nome = reader["nome"].ToString();
                            cliente.Endereco = reader["endereco"].ToString();
                            if (cliente is Pessoa_Fisica pf1)
                            {
                                pf1.Cpf = reader["cpf"].ToString();
                                pf1.Rg = reader["rg"].ToString();
                            } else if (cliente is Pessoa_Juridica pj1)
                            {
                                pj1.Cnpj = reader["cnpj"].ToString();
                                pj1.Ie = reader["ie"].ToString();
                            }
                        }
                    }

                    
                }

                double imposto = cliente.Pagar_Imposto(valor_compra);
                double total = valor_compra + imposto; 

                using (var comando = new MySqlCommand(query_venda, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@valor_compra", valor_compra);
                    comando.Parameters.AddWithValue("@valor_imposto", cliente.Pagar_Imposto(valor_compra));
                    comando.Parameters.AddWithValue("@valor_total", valor_compra + cliente.Pagar_Imposto(valor_compra));
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                Console.WriteLine("-------- Recibo: Pessoa Física --------");
                Console.WriteLine($"Data/Hora ....: {DateTime.Now}");
                Console.WriteLine($"Nome..........: {cliente.Nome}");
                Console.WriteLine($"Endereço......: {cliente.Endereco}");
                if (cliente is Pessoa_Fisica pf2)
                {
                Console.WriteLine($"CPF ..........: {pf2.Cpf}");
                Console.WriteLine($"RG ...........: {pf2.Rg}");
                } else if (cliente is Pessoa_Juridica pj2)
                {
                Console.WriteLine($"CNPJ .........: {pj2.Cnpj}");
                Console.WriteLine($"IE ...........: {pj2.Ie}");
                }
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Valor de Compra: R$ {valor_compra.ToString("N2", new CultureInfo("pt-BR"))}");
                if (cliente is Pessoa_Fisica)
                {
                    Console.WriteLine($"Imposto (10%): R$ {valor_compra.ToString("N2", new CultureInfo("pt-BR"))}");
                } else if (cliente is Pessoa_Juridica)
                {
                    Console.WriteLine($"Imposto (20%).: R$ {valor_compra.ToString("N2", new CultureInfo("pt-BR"))}");
                }
                Console.WriteLine($"Total a Pagar.: R$ {valor_compra.ToString("N2", new CultureInfo("pt-BR"))}");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("[!] Venda registrada no banco de dados com sucesso!");
            }      
        }

        public void ConsultarRegistros()
        {
            using(var conexao = _conexaoBanco.ObterConexao())
            {
                string query = @"
                    SELECT 
                    v.id_vendas, 
                    v.data_hora_venda,
                    CASE
                        WHEN v.fk_client_pf IS NOT NULL THEN 'PF'
                        WHEN v.fk_cliente_pj IS NOT NULL THEN 'PJ'
                    END AS tipo,
                    COALESCE(f.nome, j.nome) AS cliente,
                    v.valor_compra,
                    v.valor_imposto,
                    v.valor_total

                    FROM tb_vendas v
                    LEFT JOIN tb_cliente_pf f ON v.fk_cliente_pf = f.id_cliente_pf
                    LEFT JOIN tb_cliente_pj j ON v.fk_cliente_pj = j.id_cliente_pj
                    ";
                using (var comando = new MySqlCommand(query, conexao))
                {
                    
                    conexao.Open();
                    
                    using (var reader = comando.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID Venda {reader["id_vendas"]} | Data e Hora {reader["data_hora_venda"]} | Tipo {reader["tipo"]} | Cliente {reader["cliente"]} | Valor Compra {reader["valor_compra"]} | Imposto {reader["valor_imposto"]} | Total a Pagar {reader["valor_total"]}");
                            count ++;
                        }
                        Console.WriteLine($"Total de registros: {count}");
                    }
                }
            }
        }

        public void GerarRelatorioCSV()
        {
            string path = "RelatorioGeral.csv";
            string conteudoCSV = "ID Venda;Data e Hora;Tipo;Cliente;Valor Compra;Valor Imposto;Total da Venda;\n";

            using (var conexao = _conexaoBanco.ObterConexao())
            {
                string query = @"
                    SELECT 
                    v.id_vendas, 
                    v.data_hora_venda,
                    CASE
                        WHEN v.fk_client_pf IS NOT NULL THEN 'PF'
                        WHEN v.fk_cliente_pj IS NOT NULL THEN 'PJ'
                    END AS tipo,
                    COALESCE(f.nome, j.nome) AS cliente,
                    v.valor_compra,
                    v.valor_imposto,
                    v.valor_total

                    FROM tb_vendas v
                    LEFT JOIN tb_cliente_pf f ON v.fk_cliente_pf = f.id_cliente_pf
                    LEFT JOIN tb_cliente_pj j ON v.fk_cliente_pj = j.id_cliente_pj
                    ORDER BY v.id_vendas ASC";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    conexao.Open();

                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            conteudoCSV += $"{reader["id_vendas"]};{reader["data_hora_venda"]};{reader["tipo"]};{reader["cliente"]};{reader["valor_compra"]};{reader["valor_imposto"]};{reader["valor_total"]}\n";
                        }
                    }
                }
            }

            File.WriteAllText(path, conteudoCSV);

            Console.WriteLine($"[!] Sucesso! Arquivo gerado: {Path.GetFullPath(path)}");
        }
    }
}