namespace TGS.SGV.Comun.Paginacion
{
    public interface IPaginacion
    {
        int Indice { get; set; }

        int Tamanio { get; set; }

        int Total { get; set; } 
         
    }
}
