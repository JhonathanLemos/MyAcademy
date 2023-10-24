using Abp.Domain.Entities;
using MyApi.Authorization.Users;
using MyApi.Enums;

namespace MyApi.Informacoes
{
    public class Informacao : Entity<long>
    {
        public EnumAnosInformacao AnosInformacao { get; set; }
        public EnumCondicionamento CondicionamentoInformacao { get; set; }
        public EnumDiasInformacao DiasInformacao { get; set; }
        public EnumMelhoresHorarios HorariosInformacao { get; set; }
        public EnumObjetivoAcademia ObjetivoInformacao { get; set; }

        public string RestricaoAlimentar { get; set; }
        public string OutraInformacao { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public bool Finalizado { get; set; }
    }
}
