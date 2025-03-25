using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba3.Data
{

	[Table("Categoria")]
	public class Categoria
	{
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }

		public string? Descripcion { get; set; }	
	}
}
