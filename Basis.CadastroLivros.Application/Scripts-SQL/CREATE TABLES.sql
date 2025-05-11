CREATE TABLE Livro (
	Codl INT IDENTITY(1,1) NOT NULL,
	Titulo VARCHAR(40) NOT NULL,
	Editora VARCHAR(40) NULL,
	Edicao INT NULL,
	AnoPublicacao VARCHAR(4) NULL,
	CONSTRAINT PK_Livro PRIMARY KEY CLUSTERED (Codl ASC)
);
GO



CREATE TABLE Autor (
	CodAu INT IDENTITY(1,1) NOT NULL,
	Nome VARCHAR(40) NOT NULL,
	CONSTRAINT PK_Autor PRIMARY KEY CLUSTERED (CodAu ASC)
);
GO



CREATE TABLE Livro_Autor (
	Livro_Codl INT NOT NULL,
	Autor_CodAu INT NOT NULL,
	CONSTRAINT PK_Livro_Autor PRIMARY KEY CLUSTERED (Livro_Codl, Autor_CodAu),
);
GO

ALTER TABLE Livro_Autor WITH CHECK ADD CONSTRAINT FK_Livro_Autor_Livro_Codl FOREIGN KEY(Livro_Codl)
REFERENCES Livro (Codl);
GO

ALTER TABLE Livro_Autor WITH CHECK ADD CONSTRAINT FK_Livro_Autor_Autor_CodAu FOREIGN KEY(Autor_CodAu)
REFERENCES Autor (CodAu);
GO



CREATE TABLE Assunto (
	CodAs INT IDENTITY(1,1) NOT NULL,
	Descricao VARCHAR(20) NOT NULL,
	CONSTRAINT PK_Assunto PRIMARY KEY CLUSTERED (CodAs ASC)
);
GO



CREATE TABLE Livro_Assunto (
	Livro_Codl INT NOT NULL,
	Assunto_CodAs INT NOT NULL,
	CONSTRAINT PK_Livro_Assunto PRIMARY KEY CLUSTERED (Livro_Codl, Assunto_CodAs),
);
GO

ALTER TABLE Livro_Assunto WITH CHECK ADD CONSTRAINT FK_Livro_Assunto_Livro_Codl FOREIGN KEY(Livro_Codl)
REFERENCES Livro (Codl);
GO

ALTER TABLE Livro_Assunto WITH CHECK ADD CONSTRAINT FK_Livro_Assunto_Autor_CodAu FOREIGN KEY(Assunto_CodAs)
REFERENCES Assunto (CodAs);
GO



CREATE TABLE Canal_De_Venda (
	Cod_Tpv INT IDENTITY(1,1) NOT NULL,
	Descricao VARCHAR(20) NOT NULL,
	CONSTRAINT PK_Canal_De_Venda PRIMARY KEY CLUSTERED (Cod_Tpv)
);
GO

INSERT INTO Canal_De_Venda (Descricao) VALUES ('Balção');
INSERT INTO Canal_De_Venda (Descricao) VALUES ('Self-Service');
INSERT INTO Canal_De_Venda (Descricao) VALUES ('Internet');
INSERT INTO Canal_De_Venda (Descricao) VALUES ('Evento');





CREATE TABLE Livro_Preco_Venda (
	Livro_Codl INT NOT NULL,
	Canal_De_Venda_Cod_Tpv INT NOT NULL,
	Preco_De_Venda DECIMAL(10, 2),
	CONSTRAINT PK_Livro_Preco_Venda PRIMARY KEY CLUSTERED (Livro_Codl, Canal_De_Venda_Cod_Tpv),
);
GO

ALTER TABLE Livro_Preco_Venda WITH CHECK ADD CONSTRAINT FK_Livro_Preco_Venda_Livro_Codl FOREIGN KEY(Livro_Codl)
REFERENCES Livro (Codl);
GO

ALTER TABLE Livro_Preco_Venda WITH CHECK ADD CONSTRAINT 
				FK_Livro_Preco_Venda_Canal_De_Venda_Cod_Tpv FOREIGN KEY(Canal_De_Venda_Cod_Tpv)
REFERENCES Canal_De_Venda (Cod_Tpv);
GO