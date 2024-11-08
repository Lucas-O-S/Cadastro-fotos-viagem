using System.Collections.Generic;

namespace FotosViagem.Models
{
    public class FotosViagemViewModel : PadraoViewModel
    {
        public string? nomeUsuario { get; set; }
        public string? loginUsuario { get; set; }

        public string localFoto { get; set; }
        public DateTime dataFoto { get; set; }
        public int usuario { get; set; }
        public IFormFile[] fotos { get; set; } = new IFormFile[3];

        public List<byte[]> fotosByte { get; set; } = new List<byte[]>();

        public List<string> Fotos64
        {
            get
            {

                var foto64 = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    // Se fotosByte tiver menos de 3 elementos, adicionar string vazia
                    if (fotosByte.Count > i && fotosByte[i] != null)
                    {
                        foto64.Add(Convert.ToBase64String(fotosByte[i]));
                    }
                    else
                    {
                        foto64.Add(string.Empty);
                    }
                }


                return foto64;


            }
        }
        public void ProcessarFotos()
        {
            if (fotos != null && fotos.Length > 0)
            {
                foreach (var foto in fotos)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        foto.CopyTo(memoryStream); // Converte o arquivo em byte[]
                        fotosByte.Add(memoryStream.ToArray());
                    }
                }
            }
        }
    }
}
