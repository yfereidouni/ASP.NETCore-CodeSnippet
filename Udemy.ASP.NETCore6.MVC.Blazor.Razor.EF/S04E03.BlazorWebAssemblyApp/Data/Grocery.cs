using System.ComponentModel.DataAnnotations;

namespace S04E03.BlazorWebAssemblyApp.Data;

public class Grocery
{
    [Required]
    [StringLength(15,ErrorMessage ="طول رشته باشید حداقل 15 کاراکتر باشد")]
    public string Name { get; set; }
    [Required]
    [Range(1,10000,ErrorMessage ="باید وردی بین (1-10000) باشد.")]
    public float Price { get; set; }
}
