using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimicAPI.Database;
using MimicAPI.Helpers;
using MimicAPI.V1.Models;
using MimicAPI.V1.Models.DTO;
using MimicAPI.V1.Repositories.Interfaces;

namespace MimicAPI.V1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0", Deprecated = true)]
[ApiVersion("1.1")]
public class PalavrasController : ControllerBase
{
    private readonly IPalavraRepository _repository;
    private readonly IMapper _mapper;

    public PalavrasController(IPalavraRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [MapToApiVersion("1.0")]
    [MapToApiVersion("1.1")]
    [HttpGet("", Name = "ObterTodas")]
    public ActionResult<List<PalavraDTO>> GetAll([FromQuery] PalavraUrlQuery query)
    {
        var item = _repository.GetAll(query);

        if (item.Resultados.Count == 0)
            return NotFound();

        var lista = CriarLinksListPalavraDTO(query, item);

        return Ok(lista);
    }

    private PaginationList<PalavraDTO> CriarLinksListPalavraDTO(PalavraUrlQuery query, PaginationList<Palavra> item)
    {
        var lista = _mapper.Map<PaginationList<Palavra>, PaginationList<PalavraDTO>>(item);

        foreach (var palavra in lista.Resultados)
        {
            palavra.Links = new List<LinkDTO>();
            palavra.Links.Add(new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavra.Id }), "GET"));
        }

        lista.Links.Add(new LinkDTO("self", Url.Link("ObterTodas", query), "GET"));

        if (item.Paginacao != null)
        {
            Response.Headers.Add("X-Paginas", JsonSerializer.Serialize(item.Paginacao));

            if (query.Pagina < item.Paginacao.TotalPaginas)
            {
                var queryString = new PalavraUrlQuery()
                {
                    Pagina = query.Pagina + 1, Registros = query.Registros, Data = query.Data
                };
                lista.Links.Add(new LinkDTO("next", Url.Link("ObterTodas", queryString), "GET"));
            }

            if (query.Pagina - 1 > 0)
            {
                var queryString = new PalavraUrlQuery()
                {
                    Pagina = query.Pagina - 1, Registros = query.Registros, Data = query.Data
                };
                lista.Links.Add(new LinkDTO("prev", Url.Link("ObterTodas", queryString), "GET"));
            }
        }

        return lista;
    }

    [MapToApiVersion("1.0")]
    [MapToApiVersion("1.1")]
    [HttpGet("{id}", Name = "ObterPalavra")]
    public ActionResult GetById(int id)
    {
        var obj = _repository.GetById(id);
        if (obj == null)
            return NotFound();

        PalavraDTO palavraDto = _mapper.Map<Palavra, PalavraDTO>(obj);

        palavraDto.Links.Add(
            new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavraDto.Id }), "GET")
        );
        palavraDto.Links.Add(
            new LinkDTO("update", Url.Link("AtualizarPalavra", new { id = palavraDto.Id }), "UPDATE")
        );
        palavraDto.Links.Add(
            new LinkDTO("delete", Url.Link("ExcluirPalavra", new { id = palavraDto.Id }), "DELETE")
        );

        return Ok(palavraDto);
    }

    [MapToApiVersion("1.0")]
    [MapToApiVersion("1.1")]
    [HttpPost("")]
    public ActionResult Create([FromBody] Palavra palavra)
    {
        if (palavra == null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        palavra.Ativo = true;
        palavra.Criado = DateTime.Now;

        _repository.Add(palavra);

        var palavraDto = _mapper.Map<Palavra, PalavraDTO>(palavra);
        palavraDto.Links.Add(
            new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavraDto.Id }), "GET")
        );

        return Created($"/api/palavras/{palavra.Id}", palavraDto);
    }

    [MapToApiVersion("1.0")]
    [MapToApiVersion("1.1")]
    [HttpPut("{id}", Name = "AtualizarPalavra")]
    public ActionResult Update(int id, [FromBody] Palavra palavra)
    {
        var obj = _repository.GetById(id);
        if (obj == null)
            return NotFound();

        if (palavra == null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        palavra.Id = id;
        palavra.Ativo = true;
        palavra.Criado = obj.Criado;
        palavra.Atualizado = DateTime.Today;
        _repository.Update(palavra);

        var palavraDto = _mapper.Map<Palavra, PalavraDTO>(palavra);
        palavraDto.Links.Add(
            new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavraDto.Id }), "GET")
        );

        return Ok(palavraDto);
    }

    [MapToApiVersion("1.1")]
    [HttpDelete("{id}", Name = "ExcluirPalavra")]
    public ActionResult Delete(int id)
    {
        var palavra = _repository.GetById(id);
        if (palavra == null)
            return NotFound();

        palavra.Ativo = false;
        palavra.Atualizado = DateTime.Today;
        _repository.Update(palavra);

        return NoContent();
    }
}