namespace EditorGraficoFiguras
{
    //CONCRETE PROTOTYPE: CIRCULO
    public class Circulo : Figura
    {
        public int Radio { get; set; }

        public override IFigura Clonar()
        {            
            return (IFigura)this.MemberwiseClone();
        }

        public override void MostrarInfo(int numero)
        {
            Console.WriteLine($"[{numero}] Circulo -> Color: {Color} | Posicion: ({PosX},{PosY}) | Radio: {Radio}");
        }
    }
}
