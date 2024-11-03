namespace FotoViagem.Models
{
	public class FotosViagemViewModel : PadraoViewModel
	{
		public string localForo { get; set; }
		public DateTime dataFoto { get; set; }
		public int usuario { get; set; }
		public IFormFile[] fotos{get; set;}

		public List<byte[]> fotosByte {  get; set;}
		
		public List<string> Fotos64 { get {
				var foto64 = new List<string>();
				foreach (byte[] img in fotosByte)
				{
					if (img != null)
						foto64.Add(Convert.ToBase64String(img));
				}	
				return foto64;
			
			}
		}
	}
}
