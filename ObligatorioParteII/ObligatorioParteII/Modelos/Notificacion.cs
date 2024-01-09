namespace ObligatorioParteII.Modelos{
    public class Notificacion{


        private static Notificacion intancia;
        public String Mensaje {  get; set; }
        public Boolean Estado { get; set; }


        private Notificacion(){
            
        }
        public static Notificacion getInstance(){
            if (intancia == null){
                intancia = new Notificacion();
                return intancia;
            }else{
                return intancia;
            }
        }
    }
}
