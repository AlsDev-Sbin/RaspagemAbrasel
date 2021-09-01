using System.Collections.Generic;

namespace RaspagemAbrasel.Models
{
	public class Regiao
	{
		public string Nome { get; set; }
		public string Link { get; set; }
		public List<string> LinkExt { get; set; }
		public bool? ExisteCidades { get; set; }
		public List<Cidade> Cidades { get; set; }
	}
}
