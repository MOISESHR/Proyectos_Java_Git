
namespace TGS.SGV.Comun.Validador
{
    public static class EntityValidatorFactory
    {
        #region Members

        static IEntityValidatorFactory _factory = null;

        #endregion

        #region  Metodos Publicos
        
        public static void SetCurrent(IEntityValidatorFactory factory)
        {
            _factory = factory;
        }
         
        public static IEntityValidator CrearValidacion()
        {
            return (_factory != null) ? _factory.Create() : null;
        }

        #endregion
    }
}
