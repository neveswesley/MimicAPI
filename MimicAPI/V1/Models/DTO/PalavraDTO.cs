﻿namespace MimicAPI.V1.Models.DTO;

public class PalavraDTO : BaseDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Pontuacao { get; set; }
    public bool Ativo { get; set; }
    public DateTime Criado { get; set; } = DateTime.Today;
    public DateTime? Atualizado { get; set; }
}