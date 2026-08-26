namespace EditorGraficoFiguras
{    
    public class ProgramaMenu
    {
        private List<Figura> figuras = new List<Figura>();

        public void Ejecutar()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine();
                Console.WriteLine("========== EDITOR GRAFICO - PATRON PROTOTYPE ==========");
                Console.WriteLine("1. Crear figura");
                Console.WriteLine("2. Clonar figura existente");
                Console.WriteLine("3. Modificar atributos de una figura");
                Console.WriteLine("4. Listar figuras");
                Console.WriteLine("5. Salir");
                Console.WriteLine("========================================================");

                int opcion = LeerEntero("Seleccione una opcion (1-5): ", 1, 5);

                switch (opcion)
                {
                    case 1: CrearFigura(); break;
                    case 2: ClonarFigura(); break;
                    case 3: ModificarFigura(); break;
                    case 4: ListarFiguras(); break;
                    case 5: salir = true; Console.WriteLine("Saliendo del programa..."); break;
                }
            }
        }

        //Opcion 1: Crear
        private void CrearFigura()
        {
            Console.WriteLine("\n--- Crear figura ---");
            Console.WriteLine("1. Circulo");
            Console.WriteLine("2. Rectangulo");
            int tipo = LeerEntero("Tipo de figura (1-2): ", 1, 2);

            string color = LeerTexto("Ingrese el color: ");
            int posX = LeerEntero("Ingrese la posicion X: ", int.MinValue, int.MaxValue);
            int posY = LeerEntero("Ingrese la posicion Y: ", int.MinValue, int.MaxValue);

            Figura nuevaFigura;

            if (tipo == 1)
            {
                int radio = LeerEntero("Ingrese el radio (mayor a 0): ", 1, int.MaxValue);
                nuevaFigura = new Circulo { Color = color, PosX = posX, PosY = posY, Radio = radio };
            }
            else
            {
                int ancho = LeerEntero("Ingrese el ancho (mayor a 0): ", 1, int.MaxValue);
                int alto = LeerEntero("Ingrese el alto (mayor a 0): ", 1, int.MaxValue);
                nuevaFigura = new Rectangulo { Color = color, PosX = posX, PosY = posY, Ancho = ancho, Alto = alto };
            }

            figuras.Add(nuevaFigura);
            Console.WriteLine("Figura creada correctamente.");
        }

        //Opcion 2: Clonar
        private void ClonarFigura()
        {
            if (!HayFiguras()) return;

            ListarFiguras();
            int indice = LeerEntero($"Ingrese el numero de figura a clonar (1-{figuras.Count}): ", 1, figuras.Count);

            Figura original = figuras[indice - 1];
            Figura copia = (Figura)original.Clonar(); // <-- Clonacion via Prototype

            Console.WriteLine("Figura clonada. Puede modificar la copia (dejar igual = no aplica cambios).");

            string? nuevoColor = LeerTextoOpcional($"Nuevo color (Enter para mantener '{copia.Color}'): ");
            if (!string.IsNullOrEmpty(nuevoColor)) copia.Color = nuevoColor;

            int? nuevoX = LeerEnteroOpcional($"Nueva posicion X (Enter para mantener {copia.PosX}): ");
            if (nuevoX.HasValue) copia.PosX = nuevoX.Value;

            int? nuevoY = LeerEnteroOpcional($"Nueva posicion Y (Enter para mantener {copia.PosY}): ");
            if (nuevoY.HasValue) copia.PosY = nuevoY.Value;

            figuras.Add(copia);
            Console.WriteLine("Figura clonada y agregada a la lista.");
        }

        //Opcion 3: Modificar
        private void ModificarFigura()
        {
            if (!HayFiguras()) return;

            ListarFiguras();
            int indice = LeerEntero($"Ingrese el numero de figura a modificar (1-{figuras.Count}): ", 1, figuras.Count);
            Figura figura = figuras[indice - 1];

            Console.WriteLine("1. Color");
            Console.WriteLine("2. Posicion X");
            Console.WriteLine("3. Posicion Y");
            if (figura is Circulo) Console.WriteLine("4. Radio");
            if (figura is Rectangulo) Console.WriteLine("4. Ancho  /  5. Alto");

            int maxOpcion = figura is Rectangulo ? 5 : 4;
            int atributo = LeerEntero($"Que atributo desea modificar (1-{maxOpcion}): ", 1, maxOpcion);

            switch (atributo)
            {
                case 1:
                    figura.Color = LeerTexto("Nuevo color: ");
                    break;
                case 2:
                    figura.PosX = LeerEntero("Nueva posicion X: ", int.MinValue, int.MaxValue);
                    break;
                case 3:
                    figura.PosY = LeerEntero("Nueva posicion Y: ", int.MinValue, int.MaxValue);
                    break;
                case 4:
                    if (figura is Circulo c) c.Radio = LeerEntero("Nuevo radio (mayor a 0): ", 1, int.MaxValue);
                    else if (figura is Rectangulo r1) r1.Ancho = LeerEntero("Nuevo ancho (mayor a 0): ", 1, int.MaxValue);
                    break;
                case 5:
                    if (figura is Rectangulo r2) r2.Alto = LeerEntero("Nuevo alto (mayor a 0): ", 1, int.MaxValue);
                    break;
            }

            Console.WriteLine("Figura modificada correctamente.");
        }

        //Opcion 4: Listar
        private void ListarFiguras()
        {
            if (!HayFiguras()) return;

            Console.WriteLine("\n--- Listado de figuras ---");
            for (int i = 0; i < figuras.Count; i++)
            {
                figuras[i].MostrarInfo(i + 1);
            }
        }

        private bool HayFiguras()
        {
            if (figuras.Count == 0)
            {
                Console.WriteLine("No hay figuras cargadas todavia.");
                return false;
            }
            return true;
        }

        //VALIDACIONES
        private int LeerEntero(string mensaje, int min, int max)
        {
            int valor;
            bool esValido;

            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine() ?? string.Empty;

                esValido = int.TryParse(entrada, out valor);

                if (!esValido)
                {
                    Console.WriteLine("Error: debe ingresar un valor numerico entero.");
                }
                else if (valor < min || valor > max)
                {
                    Console.WriteLine($"Error: el valor debe estar entre {min} y {max}.");
                    esValido = false;
                }

            } while (!esValido);

            return valor;
        }
               
        private string LeerTexto(string mensaje)
        {
            string entrada;
            bool esValido;

            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Error: el campo no puede estar vacio.");
                    esValido = false;
                }
                else if (double.TryParse(entrada, out _))
                {
                    Console.WriteLine("Error: debe ingresar texto, no un numero.");
                    esValido = false;
                }
                else
                {
                    esValido = true;
                }

            } while (!esValido);

            return entrada;
        }
                
        private string? LeerTextoOpcional(string mensaje)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(entrada)) return null;

            if (double.TryParse(entrada, out _))
            {
                Console.WriteLine("Error: debe ingresar texto, no un numero. Se mantiene el valor anterior.");
                return null;
            }

            return entrada;
        }
                
        private int? LeerEnteroOpcional(string mensaje)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(entrada)) return null;

            if (int.TryParse(entrada, out int valor)) return valor;

            Console.WriteLine("Error: debe ingresar un numero entero. Se mantiene el valor anterior.");
            return null;
        }
    }
}
