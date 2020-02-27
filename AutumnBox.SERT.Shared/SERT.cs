using AutumnBox.SERT.Impl;

namespace AutumnBox.SERT
{
    public static class SERT
    {
        public static ISERTManager NewSERTManager()
        {
            return new SERTManagerImpl();
        }
    }
}
