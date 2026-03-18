CREATE DATABASE clientlab;
USE clientlab;

CREATE TABLE tb_cliente_pf (
	id_cliente_pf INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
	endereco VARCHAR(150) NOT NULL,
    cpf VARCHAR(14),
    rg VARCHAR(12)
);

CREATE TABLE tb_cliente_pj (
	id_cliente_pj INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    endereco VARCHAR(150) NOT NULL,
    cnpj VARCHAR(18) NOT NULL,
    ie VARCHAR(20) NOT NULL
);

CREATE TABLE tb_vendas (
	id_vendas INT AUTO_INCREMENT PRIMARY KEY,
    fk_cliente_pf INT,
    fk_cliente_pj INT,
    FOREIGN KEY (fk_cliente_pf) REFERENCES tb_cliente_pf(id_cliente_pf),
    FOREIGN KEY (fk_cliente_pj) REFERENCES tb_cliente_pj(id_cliente_pj),
    valor_compra DOUBLE NOT NULL,
    valor_imposto DOUBLE NOT NULL,
    valor_total DOUBLE NOT NULL,
    data_hora_venda DATETIME DEFAULT CURRENT_TIMESTAMP
)