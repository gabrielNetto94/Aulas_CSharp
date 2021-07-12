using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploLinq
{
    class Program
    {
        static void Main(string[] args)
        {

            //lista de gênero
            var generos = new List<Genero>
             {
                 new Genero {Id=1, Nome = "Rock"},
                 new Genero {Id=2, Nome = "Reggae"},
                 new Genero {Id=3, Nome = "Rock Progressivo"},
                 new Genero {Id=4, Nome = "Punk Rock"},
                 new Genero {Id=5, Nome = "Clássica"}
             };

            //lista de músicas
            var musicas = new List<Musica>
             {
                 new Musica {Id = 1,Nome = "Sweet child O'mine",GeneroId = 1 },
                 new Musica {Id = 2,Nome = "I shot the Sheriff",GeneroId = 2},
                 new Musica {Id = 3,Nome = "Thunderstruck",GeneroId = 1},
                 new Musica {Id = 4,Nome = "Danúbio Azul",GeneroId = 5}
             };

            ListarMusicaPorGenero(musicas, generos, "Rock");
            ListarMusicasGeneros(musicas, generos);

            Console.ReadKey();

        }
        public static void ListarMusicasGeneros(List<Musica> musicas, List<Genero> generos)
        {
            //listar nome das músicas e nome do gênero
            var musicaQuery = from m in musicas
                              join g in generos on m.GeneroId equals g.Id
                              select new
                              {
                                  NomeMusica = m.Nome,
                                  NomeGenero = g.Nome
                              };

            foreach (var musica in musicaQuery)
            {
                Console.WriteLine("Nome música: " + musica.NomeMusica + " Gênero: " + musica.NomeGenero);
            }
        }

        public static void ListarMusicaPorGenero(List<Musica> musicas, List<Genero> generos, string genero)
        {
            var musicaGeneroReggae = from m in musicas
                                     join g in generos on m.GeneroId equals g.Id
                                     where g.Nome.Equals(genero)
                                     select new { NomeMusica = m.Nome, NomeGenero = g.Nome };

            foreach (var m in musicaGeneroReggae)
            {
                Console.WriteLine("Nome música: " + m.NomeMusica + " Gênero: " + m.NomeGenero);
            }
        }
    }


}
class Genero
{
    public int Id { get; set; }
    public string Nome { get; set; }

}

class Musica
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int GeneroId { get; set; }

}