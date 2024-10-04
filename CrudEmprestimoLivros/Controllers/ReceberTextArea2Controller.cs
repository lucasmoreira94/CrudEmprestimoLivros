using CrudEmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace CrudEmprestimoLivros.Controllers
{
    public class ReceberTextArea2Controller:Controller
    {
        ConversaoJsonCsv c = new ConversaoJsonCsv();
        public IActionResult Index(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public IActionResult ReceberTextArea2(string Recebedor)
        {

            StringBuilder conteudoCSV = new StringBuilder();
            string? json = Recebedor;
            string caminhoCsv = @"c:\SAIDACSV.csv";

            try
            {
                if (json == null)
                {
                    throw new ArgumentException("Dados do campo JSON estão  vazios !");
                }
                
                if (json.TrimStart().StartsWith("["))
                {
                     
                    var registros = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);
                    ConverterJsonParaCsv(registros, caminhoCsv);

                }

                else if (json.TrimStart().StartsWith("{"))
                {
                    json = json.Trim('{', '}');  
                    json = "[" + json + "]";  

                    var registros = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);
                    ConverterJsonParaCsv(registros, caminhoCsv);
                }

                throw new ArgumentException("A string informada não é um JSON !");

            }
            catch (JsonException ex)
            {
                 return RedirectToAction("Index", new { id = "Erro ao deserializar o JSON ! Exemplo de Json Valido .: [{ \"tipo\": \"casa\", \"numero\": \"1234-5678\" },{ \"tipo\": \"celular\", \"numero\": \"9876-5432\" }]" });
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Index", new { id = ex.Message });
            }

            void ConverterJsonParaCsv(List<Dictionary<string, object>> registros, string caminhoCsv)
            {                
                if (registros == null || registros.Count == 0)
                {
                    return;
                }

                using (var writer = new StreamWriter(caminhoCsv))
                {                     
                    var colunas = new List<string>(registros[0].Keys);
                    writer.WriteLine(string.Join(",", colunas));
                                        
                    foreach (var registro in registros)
                    {
                        var valores = new List<string>();
                        foreach (var coluna in colunas)
                        {
                            valores.Add(registro[coluna]?.ToString());
                        }
                        writer.WriteLine(string.Join(",", valores));
                    }                                        
                }
                
                using (var reader = new StreamReader(caminhoCsv))
                {                   
                    while (!reader.EndOfStream)
                    {
                        string linha = reader.ReadLine();
                        if (linha != null)
                        {
                            conteudoCSV.AppendLine(linha);
                        }
                    }
                }

            }
            return RedirectToAction("Index", new { id = conteudoCSV.ToString() });
        }                       
    }
}


 