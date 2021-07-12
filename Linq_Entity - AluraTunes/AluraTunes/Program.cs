using AluraTunes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AluraTunes
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var contexto = new AluraTunesEntities())
            {

                //ListarAlbunsMaisVendidos(contexto, "Led Zeppelin");
                //ListarQuantidadeAlbunsArtistas(contexto);
                //MaiorMenorMediaVendas(contexto);
                //CalcularMedianaVendas(contexto);
            }

            Console.ReadKey();
        }

        static void BuscarArtistaPorNome(AluraTunesEntities contexto, string nomeArtista)
        {

            //seleciona artistas e os albuns que contenham a palavra "Led"
            var query = from artista in contexto.Artistas
                        join alb in contexto.Albums on artista.ArtistaId equals alb.ArtistaId
                        where artista.Nome.Contains(nomeArtista)
                        select new
                        {
                            artista,
                            alb
                        };

            foreach (var item in query)
            {
                Console.WriteLine("{1} {2}", item.artista.ArtistaId, item.artista.Nome, item.alb.Titulo);
            }

        }
        static void BuscarArtistaPorNomeSemJoin(AluraTunesEntities contexto, string nome)
        {

            //query sem join apenas usando os realacionamentos dos objetos
            var query = from alb in contexto.Albums
                        where alb.Artista.Nome.Contains(nome)//acessa diretamente o objeto
                        select new
                        {
                            NomeArtista = alb.Artista.Nome,
                            NomeAlbum = alb.Titulo
                        };

            foreach (var item in query)
            {
                Console.WriteLine("{0} {1}", item.NomeArtista, item.NomeAlbum);
            }

        }

        static void ListarMusicasClienteComprou(AluraTunesEntities contexto, int id)
        {

            //listar nome do cliente, nome artista e música que cliente X comprou
            var query = from c in contexto.Clientes
                        join nf in contexto.NotaFiscals on c.ClienteId equals nf.ClienteId
                        join inf in contexto.ItemNotaFiscals on nf.NotaFiscalId equals inf.NotaFiscalId
                        join f in contexto.Faixas on inf.FaixaId equals f.FaixaId
                        join alb in contexto.Albums on f.AlbumId equals alb.AlbumId
                        join art in contexto.Artistas on alb.ArtistaId equals art.ArtistaId
                        where c.ClienteId.Equals(id)
                        orderby nf.DataNotaFiscal ascending
                        select new
                        {
                            Nome = c.PrimeiroNome + " " + c.Sobrenome,
                            Artista = art.Nome,
                            Faixa = f.Nome,
                            Data = nf.DataNotaFiscal
                        };

            Console.WriteLine("Compras do cliente: " + query.First().Nome);
            foreach (var item in query)
            {
                Console.WriteLine("{0} - {1} - {2}", item.Data, item.Artista, item.Faixa);
            }
        }

        static void ConsultaComLambda(AluraTunesEntities contexto, string nomeArtista)
        {
            //consulta Nome do artista com expressao lambda
            var query = contexto.Artistas.Where(a => a.Nome.Contains(nomeArtista));

            foreach (var artista in query)
            {
                Console.WriteLine("{0} {1}", artista.ArtistaId, artista.Nome);
            }
        }

        static void ExemploConsultaJoin(AluraTunesEntities contexto)
        {

            //consulsta com join
            var query = from faixa in contexto.Faixas
                        join genero in contexto.Generoes on faixa.GeneroId equals genero.GeneroId
                        select new
                        {
                            IdFaixa = faixa.FaixaId,
                            NomeFaixa = faixa.Nome,
                            NomeGenero = genero.Nome
                        };

            foreach (var item in query.Take(10))
            {
                Console.WriteLine("{0} - {1} - {2}", item.IdFaixa, item.NomeFaixa, item.NomeGenero);
            }
        }

        static void ConsultaJoinBuscando10Elementos(AluraTunesEntities contexto)
        {

            //consulta com join trazendo os 10 primeiros elementos
            var query = from g in contexto.Generoes
                        join f in contexto.Faixas
                        on g.GeneroId equals f.GeneroId
                        select new
                        {
                            Genero = g.Nome,
                            Faixa = f.Nome
                        };
            query = query.Take(10);// busca os primeiros 10 elementos

            contexto.Database.Log = Console.WriteLine;//printa no console o que foi executado no SQL SERVER
            Console.WriteLine(query); //outra maneira de vez o log da aplicação

            foreach (var fg in query)
            {
                Console.WriteLine("{0} {1}", fg.Genero, fg.Faixa);
            }
        }

        static void GetFaixas(AluraTunesEntities contexto, string artista, string album)
        {
            //busca faixas de determinado artista e teste se foi passado algo no parâmetro "album"
            var query = from f in contexto.Faixas
                        where f.Album.Artista.Nome.Contains(artista)
                        && (!string.IsNullOrEmpty(artista) ? f.Album.Titulo.Contains(album) : true) //se string artista não vier vazia, se vazia retorna true
                        orderby f.Album.Titulo, f.Nome
                        select f;

            foreach (var item in query)
            {
                Console.WriteLine("{0} {1}", item.Album.Titulo.PadRight(40), item.Nome);
            }

        }

        static void ContarFaixas(AluraTunesEntities contexto, string artista)
        {
            //conta as faixas de determinado artista com sintaxe do linq e lambda
            var query = from f in contexto.Faixas
                        where f.Album.Artista.Nome.Contains(artista)
                        select f;

            foreach (var faixa in query)
            {
                Console.WriteLine("{0} {1}", faixa.Album.Artista.Nome, faixa.Nome);
            }

            var quantidade = contexto.Faixas
                            .Count(a => a.Album.Artista.Nome.Equals(artista));

            Console.WriteLine("Quantidade de músicas do " + artista + ": " + quantidade);
        }

        static void CalcularTotalVendasArtista(AluraTunesEntities contexto, string artista)
        {
            var query = from inf in contexto.ItemNotaFiscals

                            /* 
                            //CONSULTA COM JOIN
                            join f in contexto.Faixas on iNF.FaixaId equals f.FaixaId
                            join alb in contexto.Albums on f.AlbumId equals alb.AlbumId
                            join art in contexto.Artistas on alb.ArtistaId equals art.ArtistaId
                            where art.Nome.Equals(artista)
                            */
                            //MESMA CONSULTA USANDO AS PROPRIEDADES
                        where inf.Faixa.Album.Artista.Nome == artista
                        select new
                        {
                            totalItem = inf.Quantidade * inf.PrecoUnitario, //retorna o total em valor da cada NF
                        };

            var valorTotalArtista = query.Sum(q => q.totalItem);//calcular o valor total de vendas do artista

            Console.WriteLine("Total de vendas do " + artista + " R$ {0}", valorTotalArtista);
        }

        static void ListarAlbunsMaisVendidos(AluraTunesEntities contexto, string artista)
        {
            //lista os albuns vendidos ordenado pelo preço
            var query = from inf in contexto.ItemNotaFiscals
                        where inf.Faixa.Album.Artista.Nome.Contains(artista)
                        group inf by inf.Faixa.Album into agrupado //agrupar o inf por Album e larga na variavel "agrupado"
                        let VendasPorAlbum = agrupado.Sum(a => a.Quantidade * a.PrecoUnitario)// cria variável para para consulta linq
                        orderby VendasPorAlbum
                        descending
                        select new
                        {
                            TituloAlbum = agrupado.Key.Titulo,
                            TotalPorAlbum = VendasPorAlbum
                        };

            foreach (var item in query)
            {
                Console.WriteLine("{0} - {1}",
                    item.TituloAlbum.PadRight(40),
                    item.TotalPorAlbum);
            }
        }

        static void ListarQuantidadeAlbunsArtistas(AluraTunesEntities contexto)
        {
            //Listar quantidade de albuns de cada banda
            var artistaEalbum = from alb in contexto.Albums
                                group alb by alb.Artista into agrupado
                                select new
                                {
                                    Artista = agrupado.Key.Nome,
                                    QuantidadeAlbuns = agrupado.Count()
                                };
            foreach (var item in artistaEalbum)
            {
                Console.WriteLine("{0}\t{1}", item.Artista.PadRight(40), item.QuantidadeAlbuns);
            }
        }

        static void MaiorMenorMediaVendas(AluraTunesEntities contexto)
        {
            //contexto.Database.Log = Console.WriteLine;//exibe no console o log do banco

            var maiorVenda = contexto.NotaFiscals.Max(nf => nf.Total);
            var menorVenda = contexto.NotaFiscals.Min(nf => nf.Total);
            var mediaVenda = contexto.NotaFiscals.Average(nf => nf.Total);

            Console.WriteLine("{0} - {1}- {2}", maiorVenda, mediaVenda, menorVenda);

            //FAZ A MESMA COISA DA QUERY ACIMA MAS MAIS REALIZA APENAS UM CONSULTA NO BANCO AO INVÉS DE 3 QUE O MÉTODO ACIMA FAZ
            var vendas = (from nf in contexto.NotaFiscals
                          group nf by 1 into agrupado
                          select new
                          {
                              maiorVenda = agrupado.Max(nf => nf.Total),
                              menorVenda = agrupado.Min(nf => nf.Total),
                              mediaVenda = agrupado.Average(nf => nf.Total)
                          }).Single(); //single faz toda a consulta retornar para a variável vendas

            Console.WriteLine("{0} - {1}- {2}", vendas.maiorVenda, vendas.mediaVenda, vendas.menorVenda);
        }

        static void CalcularMedianaVendas(AluraTunesEntities contexto)
        {

            var query = from nf in contexto.NotaFiscals
                        orderby nf.Total
                        select nf;

            var elementoCentral = query
                                 .Skip(query.Count() / 2)
                                 .First(); //pega o elemento central

            Console.WriteLine("Mediana = {0}", elementoCentral.Total);
        }
    }
}