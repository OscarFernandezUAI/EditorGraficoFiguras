namespace EditorGraficoFiguras
{
    //CLASE ABSTRACTA
    public abstract class Figura : IFigura
    {
        public string Color { get; set; } = string.Empty;
        public int PosX { get; set; }
        public int PosY { get; set; }

        public abstract IFigura Clonar();
        public abstract void MostrarInfo(int numero);
    }
}
