using MimicAPI.Helpers;
using MimicAPI.V1.Models;

namespace MimicAPI.V1.Repositories.Interfaces;

public interface IPalavraRepository
{
    PaginationList<Palavra> GetAll(PalavraUrlQuery query);
    Palavra GetById(int id);
    void Add(Palavra palavra);
    void Update(Palavra palavra);
    void Delete(int id);
}