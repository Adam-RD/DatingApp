﻿using System.ComponentModel.DataAnnotations;

namespace API;

public class RegisterDTO
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    
    [StringLength(8, MinimumLength =4)]
    public  string password { get; set; } = string.Empty;
    

}
