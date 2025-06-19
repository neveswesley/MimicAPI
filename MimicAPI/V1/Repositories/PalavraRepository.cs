using Microsoft.EntityFrameworkCore;
using MimicAPI.Database;
using MimicAPI.Helpers;
using MimicAPI.V1.Models;
using MimicAPI.V1.Repositories.Interfaces;

namespace MimicAPI.V1.Repositories;

public class PalavraRepository : IPalavraRepository
{
    private readonly MimicContext _db;

    public PalavraRepository(MimicContext db)
    {
        _db = db;
    }


    public PaginationList<Palavra> GetAll(PalavraUrlQuery query)
    {
        var lista = new PaginationList<Palavra>();
        var item = _db.Palavras.AsQueryable();
        
        if (query.Data.HasValue)
        {
            item = item.Where(a => a.Criado > query.Data.Value || a.Atualizado > query.Data.Value);
        }

        if (query.Pagina.HasValue)
        {
            var numeroTotalRegistros = item.Count();
            var totalPaginas = (int)Math.Ceiling((double)numeroTotalRegistros / query.Registros.Value);
            
            item = item.Skip((query.Pagina.Value - 1) * query.Registros.Value).Take(query.Registros.Value);
          
            var paginacao = new Paginacao();
            paginacao.NumeroPagina = query.Pagina.Value;
            paginacao.RegistrosPorPagina = query.Registros.Value;
            paginacao.TotalRegistros = numeroTotalRegistros;
            paginacao.TotalPaginas = totalPaginas;
            lista.Paginacao = paginacao;
        }
        
        lista.Resultados.AddRange(item.ToList());
        
        return lista;

    }

    public Palavra GetById(int id)
    {
        return _db.Palavras.AsNoTracking().FirstOrDefault(a => a.Id == id);
    }

    public void Add(Palavra palavra)
    {
        _db.Add(palavra);
        _db.SaveChanges();
    }

    public void Update(Palavra palavra)
    {
        _db.Palavras.Update(palavra);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var palavra = GetById(id);
        _db.Palavras.Remove(palavra);
        _db.SaveChanges();
    }
}