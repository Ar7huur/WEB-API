namespace WebAPI.Models {
    public class Vendedor {
        public int VendedorID { get; set; }
        public string VendedorNome { get; set; }
        public string VendedorEmail { get; set; }
        public decimal VendedorSalario { get; set; }

        public Vendedor() {
            VendedorNome = "";
            VendedorEmail = "";

        }

        public Vendedor(int vendedorID, string vendedorNome, string vendedorEmail, decimal vendedorSalario) {
            VendedorID = vendedorID;
            VendedorNome = vendedorNome;
            VendedorEmail = vendedorEmail;
            VendedorSalario = vendedorSalario;

        }
    }
}
