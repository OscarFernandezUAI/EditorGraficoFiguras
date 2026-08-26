namespace EditorGraficoFiguras
{
    //CONCRETE PROTOTYPE: RECTANGULO
    public class Rectangulo : Figura
    {
        public int Ancho { get; set; }
        public int Alto { get; set; }

        public override IFigura Clonar()
        {
            return (IFigura)this.MemberwiseClone();
        }

        public override void MostrarInfo(int numero)
        {
            Console.WriteLine($"[{numero}] Rectangulo -> Color: {Color} | Posicion: ({PosX},{PosY}) | Ancho: {Ancho} | Alto: {Alto}");
        }
    }
}
