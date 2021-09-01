using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using HtmlAgilityPack;
using MaterialSkin;
using MaterialSkin.Controls;
using RaspagemAbrasel.Models;
using RaspagemAbrasel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace RaspagemAbrasel
{
	public partial class ViewMain : MaterialForm
	{
		public List<Regiao> ListaRegioes { get; set; }
		public CountItens CountItens { get; set; }
		public bool ConcluidoRaspagem { get; set; }
		public int IndexLista { get; set; }
		public ViewMain()
		{
			InitializeComponent();

			#region VISUAL
			var materialSkinManager = MaterialSkinManager.Instance;
			materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.Red600, Primary.Red900, Primary.Red400, Accent.Red700, TextShade.WHITE);
			#endregion
		}

		async Task ListarRegioes()
		{
			await Task.Run(() => {
				var htmlDoc = new HtmlWeb().Load("https://abrasel.com.br/");

				var selecRegiao = htmlDoc.DocumentNode.SelectNodes("//select[contains(@class, 'form-control inp')]").First();

				var ItensListaRegiao = selecRegiao.SelectNodes("//option");

				foreach (var item in ItensListaRegiao)
				{
					if (!item.GetAttributeValue("Value", "").Contains("http") ||
						item.GetAttributeValue("Value", "") is ("" or "http://abrasel.com.br"))
						continue;

					ListaRegioes.Add(new Regiao()
					{
						Nome = item.InnerText,
						Link = $"{item.GetAttributeValue("Value", "")}/associados/",
						LinkExt = new(),
					});

					CountItens.Regioes++;
				}
			});
		}

		async Task ListarCidades()
		{
			await Task.Run(() => {
				int i;
				var l = Parallel.For(i = 0, ListaRegioes.Count, async (i) => {
					try
					{
						ListaRegioes[i].Cidades = new();

						var regiao = ListaRegioes[i];

						var htmlDoc = await Task.Run(() => new HtmlWeb().Load(regiao.Link));
						if (htmlDoc.ParsedText.StartsWith("404"))
							htmlDoc = await Task.Run(() => new HtmlWeb().Load(regiao.Link = regiao.Link.Replace("associados", "associe-se")));

						var div = htmlDoc.DocumentNode.SelectNodes("//div");
						if (div is not null && div.Count > 0)
						{
							var endpoint = div.First().InnerText;
							if (endpoint.Contains('.'))
								endpoint = endpoint.Split('.').First();

							ListaRegioes[i].LinkExt.Add($"https://conexao.abrasel.com.br/cities/{endpoint}");
						}

						div = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'res')]");

						if (div is not null && div.Count > 0)
						{
							var scripts = div[0].SelectNodes("//script")[7];
							if (scripts != null)
							{
								string script = scripts.InnerText;
								int index = script.IndexOf(".load(") + 7;

								script = script.Substring(index);
								var link = script.Substring(0, script.IndexOf("\"")).Replace(@"""", "");

								ListaRegioes[i].LinkExt.Add(link);
								await ExtListarCidades(i);
							}
						}
					} catch (Exception e) { }
				});
			});
		}

		async Task ExtListarCidades(int index)
		{
			await Task.Run(() => {
				try
				{
					int indexRemove = 0;
					for (int i = 0; i < ListaRegioes[index].LinkExt.Count; i++)
					{
						var linkExt = ListaRegioes[index].LinkExt[i];
						try
						{
							var apiHtmlDoc = new HtmlWeb().Load(linkExt);
							var SelectCidades = apiHtmlDoc.GetElementbyId("get-city");
							if (SelectCidades != null)
							{
								var OptionsCidades = SelectCidades.SelectNodes("//option");
								if (OptionsCidades != null)
								{
									var Cidades = (
										from Ic in OptionsCidades
										where Ic.GetAttributeValue("value", "") != ""
										select new Cidade()
										{
											Id = Convert.ToInt32(Ic.GetAttributeValue("value", "")),
											Nome = Ic.InnerText
										}).ToList();

									ListaRegioes[index].Cidades = Cidades;

									CountItens.Cidades += Cidades.Count();
									ListaRegioes[index].ExisteCidades = Cidades.Count() > 0;
									indexRemove = i == 1 ? 0 : 1;
								}
							}
							break;
						} catch (Exception e)
						{
							indexRemove = i;
						}
					}
					ListaRegioes[index].LinkExt.RemoveAt(indexRemove);
				} catch (Exception e)
				{
					try
					{
						var apiHtmlDoc = new HtmlWeb().Load(ListaRegioes[index].Link);
						var Div = apiHtmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'internal-text')]");
						if (Div is not null)
						{
							var TagP = Div[0].SelectNodes("//p");
							if (TagP is not null)
							{
								var ListP = TagP[1].InnerHtml;
								var ListEstabelecimentoAnonimo = ListP.Split(new[] { "<br>", "<br/>" }, StringSplitOptions.RemoveEmptyEntries);
								if (ListP is not null)
								{
									var Cidades = (
										from p in ListEstabelecimentoAnonimo
										select new Cidade()
										{
											Id = -1,
											Nome = $"{p} | [Sem informação]"
										}).ToList();

									ListaRegioes[index].Cidades = Cidades;
									ListaRegioes[index].ExisteCidades = Cidades.Count() > 0;

									CountItens.Cidades += Cidades.Count();
								}
							}
						}
					} catch { }
				}
			});

			if (ListaRegioes[index].ExisteCidades is null)
				ListaRegioes[index].ExisteCidades = false;
		}

		async Task ListarEstabelecimentos()
		{
			int x;
			for (int i = 0; i < ListaRegioes.Count; i++)
			{
				var Regiao = ListaRegioes[i];

				while (Regiao.Cidades is null || Regiao.ExisteCidades is null)
					await Task.Delay(1000);

				Parallel.For(x = 0, Regiao.Cidades.Count, async (x) => {
					if (Regiao.Cidades[x] is not null || Regiao.ExisteCidades.Value)
					{
						var cidade = Regiao.Cidades[x];

						try
						{
							if (cidade.Id == -1 || Regiao.LinkExt is null || Regiao.LinkExt.Count == 0)
								"".Substring(1); //Forçar entrada no catch para dar um continue.

							var docHtml = new HtmlWeb().Load($"{Regiao.LinkExt.First()}/{cidade.Id}");

							var DivsEstabelecimentos = docHtml.DocumentNode.SelectNodes("//div[contains(@class, 'panel panel-default')]");
							if (DivsEstabelecimentos is not null)
							{
								ListaRegioes[i].Cidades[x].Estabelecimentos = new();
								foreach (var DivEstabelecimento in DivsEstabelecimentos)
								{
									var Estabelecimento = new Estabelecimento();
									var htmlContent = DivEstabelecimento.InnerHtml.Split(new[] { "\n", "<br>" }, StringSplitOptions.RemoveEmptyEntries);

									for (int h = 0; h < htmlContent.Length; h++)
									{
										var entidade = htmlContent[h];

										if (entidade.StartsWith("Endereço:"))
										{
											Estabelecimento.Endereco = entidade.Substring(9).Trim();
											h++;
											Estabelecimento.Localidade = h + 1 <= htmlContent.Length ? htmlContent[h] : "";
										} else if (entidade.StartsWith("Telefone:"))
											Estabelecimento.Telefone = entidade.Substring(9).Trim();
										else if (entidade.StartsWith("CEP:"))
											Estabelecimento.CEP = entidade.Substring(4, entidade.IndexOf("</div>") - 4).Trim();
										else if (entidade.Contains("><b>") & entidade.EndsWith("</b>"))
											Estabelecimento.Nome = entidade.Substring(entidade.IndexOf("><b>") + 4).TrimEnd("</b>".ToCharArray());
										if (entidade.Contains(@"<div class=""panel-footer"">"))
											Estabelecimento.TipoEstabelecimento = (Estabelecimento.TipoEstabelecimento =
													entidade.Substring(
													entidade.IndexOf(@"<div class=""panel-footer"">") + 26))
													.Substring(0, Estabelecimento.TipoEstabelecimento.LastIndexOf("</div>"));
									}

									if (ListaRegioes[i].Cidades[x].Estabelecimentos is null)
										ListaRegioes[i].Cidades[x].Estabelecimentos = new();
									ListaRegioes[i].Cidades[x].Estabelecimentos.Add(Estabelecimento);

									CountItens.Estabelecimentos++;
								}
								ListaRegioes[i].Cidades[x].ExisteEstabelecimento = (ListaRegioes[i].Cidades[x].Estabelecimentos.Count > 0);
							}
							if (ListaRegioes[i].Cidades[x].ExisteEstabelecimento is null)
								ListaRegioes[i].Cidades[x].ExisteEstabelecimento = false;
						} catch (Exception e)
						{
							if (ListaRegioes[i].Cidades is not null)
								ListaRegioes[i].Cidades = new();

							if (ListaRegioes[i].Cidades.Count >= x && ListaRegioes[i].Cidades[x].ExisteEstabelecimento is null)
							{
								if (ListaRegioes[i].Cidades[x].Estabelecimentos is null)
									ListaRegioes[i].Cidades[x].Estabelecimentos = new();

								ListaRegioes[i].Cidades[x].ExisteEstabelecimento = false;
							}
						}
					}
				});

				if (ListaRegioes[i].Cidades is null)
				{
					ListaRegioes[i].Cidades = new();
					ListaRegioes[i].ExisteCidades = false;
				}
			}
		}

		async Task SalvarPlanilha()
		{
			var workBook = new XLWorkbook();
			var workSheet = workBook.AddWorksheet("Estabelecimentos obtidos ✅");

			workSheet.Cell("B3").Value = "Nome";
			workSheet.Cell("C3").Value = "Telefone";
			workSheet.Cell("D3").Value = "Localidade";
			workSheet.Cell("E3").Value = "Endereço";
			workSheet.Cell("F3").Value = "CEP";

			int IndexCell = 1;
			for (int iRegiao = 0; iRegiao < ListaRegioes.Count; iRegiao++)
			{
				var regiao = ListaRegioes[iRegiao];
				workSheet.Cell($"B{IndexCell}").Value = regiao.Nome;
				workSheet.Range($"B{IndexCell}:F{IndexCell}").Merge().Style.Font.SetBold();

				while (regiao.ExisteCidades is null)
					await Task.Delay(250);

				if (!regiao.ExisteCidades.Value)
				{
					workSheet.Cell($"B{IndexCell}").Value = "[Sem informação]";
					workSheet.Cell($"C{IndexCell}").Value = regiao.Nome;
					workSheet.Cell($"C{IndexCell}").Value = regiao.Link;
					IndexCell++;
					continue;
				}

				for (int iCidade = 0; iCidade < regiao.Cidades.Count; iCidade++)
				{
					while (regiao.Cidades[iCidade] is null)
						await Task.Delay(250);

					if (regiao.Cidades[iCidade].Id == -1)
					{
						workSheet.Cell($"B{IndexCell}").Value = regiao.Cidades[iCidade].Nome;
						IndexCell++;
						continue;
					}

					var cidade = regiao.Cidades[iCidade];

					while (cidade.Estabelecimentos is null || cidade.ExisteEstabelecimento is null)
						await Task.Delay(250);

					if (!cidade.ExisteEstabelecimento.Value)
						continue;

					for (int iEstabelecimento = 0; iEstabelecimento < cidade.Estabelecimentos.Count(); iEstabelecimento++)
					{
						while (cidade.Estabelecimentos[iEstabelecimento] is null)
							await Task.Delay(250);

						var Estabelecimento = cidade.Estabelecimentos[iEstabelecimento];

						workSheet.Cell($"B{IndexCell}").Value = Estabelecimento.Nome;
						workSheet.Cell($"C{IndexCell}").Value = Estabelecimento.Telefone;
						workSheet.Cell($"D{IndexCell}").Value = Estabelecimento.Localidade;
						workSheet.Cell($"E{IndexCell}").Value = Estabelecimento.Endereco;
						workSheet.Cell($"F{IndexCell}").Value = Estabelecimento.CEP;

						IndexCell++;
						Progress.Invoke(() => Progress.PerformStep());
					}

				}
			}

			workBook.SaveAs(TxCaminho.Invoke(() => TxCaminho.Text).ToString());

			Progress.Value = Progress.Maximum;
			ConcluidoRaspagem = true;
		}

		async Task IniciarRaspagem()
		{
			await Task.Run(async () => {
				CountItens = new();
				ConcluidoRaspagem = false;
				ReloadDadosProgress();

				await ListarRegioes();

				await ListarCidades();

				SalvarPlanilha();

				await ListarEstabelecimentos();

				//ConcluidoRaspagem = true;
			});
		}

		async void ReloadDadosProgress()
		{
			while (!ConcluidoRaspagem)
			{

				Invoke(new Action(() => {
					LbRegioes.Text = string.Format(LbRegioes.Tag.ToString(), CountItens.Regioes);
					LbCidades.Text = string.Format(LbCidades.Tag.ToString(), CountItens.Cidades);
					LbEstabelecimento.Text = string.Format(LbEstabelecimento.Tag.ToString(), CountItens.Estabelecimentos);
				}));

				await Task.Delay(800);

				Progress.Invoke(() => {
					Progress.Maximum = CountItens.Regioes + CountItens.Estabelecimentos + CountItens.Cidades;
				});
			}
		}

		private async void BtIniciarRaspagem_Click(object sender, EventArgs e)
		{
			BtIniciarRaspagem.Enabled = false;
			ListaRegioes = new List<Regiao>();

			await IniciarRaspagem();


			BtIniciarRaspagem.Enabled = true;
		}

		private void ViewMain_Load(object sender, EventArgs e)
		{
			var date = DateTime.Now.ToString("dd-MM-yyyy");
			TxCaminho.Text = @$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\raspagem_de_dados[{date}].xlsx";
		}

		private void BtProcurar_Click(object sender, EventArgs e)
		{
			var openSave = new SaveFileDialog()
			{
				CheckPathExists = true,
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
				Filter = "Excel Xlsx|*.xlsx",
				DefaultExt = "Excel Xlsx|*.xlsx"
			};
			if (openSave.ShowDialog() == DialogResult.OK)
				TxCaminho.Text = openSave.FileName;

		}
	}
}
