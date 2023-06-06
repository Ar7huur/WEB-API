namespace WebAPI.Models {
    public class Departamento {
        public int ID { get; set; }
        public  string Nome { get; set; }

        public Departamento() {
            Nome = "";
        }

        public Departamento(int iD, string nome) :this() {
            ID = iD;
            Nome = nome;
        }
    }
}
