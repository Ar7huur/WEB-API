namespace WebAPI.Models {
    public class Venda {

        public int VendaID { get; set; }
        public string VendaData { get; set; }
        public decimal VendaValor { get; set; }

        public Venda() {           
            VendaData = "";
            
        }

        public Venda(int vendaID, string vendaData, decimal vendaValor) {
            VendaID = vendaID;
            VendaData = vendaData;
            VendaValor = vendaValor;
        }
    }
}
