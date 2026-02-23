namespace skills_be.Models.Dto;

public class ActiveDirectoryUserDto
{
    public string? SamAccountName { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string? UserPrincipalName { get; set; }
}
