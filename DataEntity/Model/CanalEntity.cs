namespace DataEntity.Model
{
    class CanalEntity : BaseEntity
    {
        public string Nome { get; set; }
        public string Criador { get; set; }
        public EpisodioEntity Episodio { get; set; }
    }
}
