using System;

namespace DataEntity.Model
{
    class EpisodioEntity : BaseEntity
    {
        public int NumeroDoEpisodio { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Playlist { get; set; }

        public DateTime DataPublicacao { get; set; }
    }
}
