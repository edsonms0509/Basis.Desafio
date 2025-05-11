CREATE VIEW dbo.vw_LivrosComAutores
AS

SELECT 
	A.Nome Autor, L.Titulo, L.Editora, 
	L.Edicao, L.AnoPublicacao, S.Descricao Assunto 
FROM Livro L
	INNER JOIN Livro_Autor LA
	ON L.Codl = LA.Livro_Codl
	INNER JOIN Autor A
	ON LA.Autor_CodAu = A.CodAu
	INNER JOIN Livro_Assunto LS
	ON L.Codl = LS.Livro_Codl
	INNER JOIN Assunto S
	ON LS.Assunto_CodAs = S.CodAs;

GO


--select top 8 * from vw_LivrosComAutores order by autor 