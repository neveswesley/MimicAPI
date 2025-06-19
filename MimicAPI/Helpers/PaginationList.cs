using MimicAPI.V1.Models.DTO;

namespace MimicAPI.Helpers;

public class PaginationList<T>
{
    public List<T> Resultados { get; set; } = new List<T>();
    public Paginacao Paginacao { get; set; }
    public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
}