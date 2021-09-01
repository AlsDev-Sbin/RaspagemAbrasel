using System.Collections.Generic;

namespace RaspagemAbrasel.Models
{
	public class Cidade
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public bool? ExisteEstabelecimento { get; set; }
		public List<Estabelecimento> Estabelecimentos { get; set; }
	}
}
