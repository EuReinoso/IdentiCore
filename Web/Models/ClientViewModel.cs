using Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class ClientViewModel
{
    public Guid? Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public byte[]? Logo { get; set; }

    public IFormFile? LogoFile { get; set; }

    public async Task<UpdateClientDto> ToUpdateClientDtoAsync()
    {
        byte[]? finalLogo = Logo;

        if (LogoFile != null && LogoFile.Length > 0)
        {
            using var ms = new MemoryStream();
            await LogoFile.CopyToAsync(ms);
            finalLogo = ms.ToArray();
        }

        return new UpdateClientDto
        {
            Name = Name,
            Email = Email,
            Logo = finalLogo
        };
    }
}
